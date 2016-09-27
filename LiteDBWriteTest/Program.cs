using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteDBWriteTest
{

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string[] Phones { get; set; }
        public Dictionary<string,string> Items { get; set; }
        public bool IsActive { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new LiteDatabase(@"D:\Projects\C#\LiteDBEditor\test\MyData.db"))
            {
                // Get customer collection
                var col = db.GetCollection<Customer>("customers");

                while (true)
                {
                    // Create your new customer instance
                    var customer = new Customer
                    {
                        Name = "John Doe " + new Random(DateTime.Now.Millisecond).Next().ToString(),
                        Phones = new string[] { "8000-0000", "9000-0000" },
                        IsActive = true
                    };

                    customer.Items = new Dictionary<string, string>();
                    customer.Items.Add("test", "test");

                    // Insert new customer document (Id will be auto-incremented)
                    Console.WriteLine("Entry was added: " + (int)col.Insert(customer).RawValue);
                }                
            }
        }
    }
}
