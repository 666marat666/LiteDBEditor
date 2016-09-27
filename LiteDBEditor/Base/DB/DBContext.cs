using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteDBEditor.Base.DB
{
    public static class DBContext
    {

        public static OnInitEventHandler OnInit;
        public delegate void OnInitEventHandler(LiteDatabase db);

        public static LiteDatabase db;
        public static void Init(string dbConnString)
        {
            db = new LiteDB.LiteDatabase(dbConnString);
            OnInit(db);
        }
        

    }
}
