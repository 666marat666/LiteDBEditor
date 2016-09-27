using LiteDBEditor.Base.DB;
using LiteDBEditor.Global;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiteDBEditor
{
    public partial class LiteDBEditor : Form
    {
        public LiteDBEditor()
        {
            InitializeComponent();
            DBContext.OnInit += InitControls;

            CollectionsList.DoubleClick += CollectionsList_DoubleClick;
        }

        private void CollectionsList_DoubleClick(object sender, EventArgs e)
        {
            DocumentsList.Nodes.Clear();
            var col = DBContext.db.GetCollection(CollectionsList.SelectedNode.Text);
            
            foreach (var doc in col.FindAll())
            {
                TreeNode node = null;
                foreach (var key in doc.Keys)
                {                    
                    if(key.ToLower() == "_id")
                    {
                        node = DocumentsList.Nodes.Add(doc.Get(key).AsString);                        
                    }
                    else if(key.ToLower() != "_id" && node != null)
                    {
                        var keyValue = doc.Get(key);
                        if (!keyValue.IsArray)
                        {
                            node.Nodes.Add(key + " : " + doc.Get(key).AsString);
                        }
                        else
                        {
                            var parentNode = node.Nodes.Add(key);
                            foreach(var item in keyValue.AsArray)
                            {
                                parentNode.Nodes.Add(item.AsString);
                            }
                        }
                    }
                }
                //node.Nodes.Add("Document: " + doc.ra);
            }
        }

        public void InitControls(LiteDB.LiteDatabase db)
        {
            foreach (var colName in db.GetCollectionNames())
            {
                CollectionsList.Nodes.Add(colName);
            }
            //CollectionsList
        }

        

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "db files (*.db)|*.db|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {                    
                    AppSettings.dbFilePath = openFileDialog1.FileName;
                    DBContext.Init(AppSettings.dbFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read database. Original error: " + ex.Message);
                }
            }
        }

        private void runCommandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var result = DBContext.db.Run(CommandText.Text);

                DocumentsList.Nodes.Clear();
                if(result.IsArray)
                {
                    foreach(var item in result.AsArray)
                    {
                        DocumentsList.Nodes.Add(item.AsDocument.ToString());
                    }
                }
                else
                {
                    DocumentsList.Nodes.Add(result.ToString());
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
