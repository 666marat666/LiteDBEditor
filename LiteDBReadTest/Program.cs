using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteDBReadTest
{
    class Program
    {
        public class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string[] Phones { get; set; }
            public bool IsActive { get; set; }
        }
        static void Main(string[] args)
        {
            using (var db = new LiteDatabase(@"D:\Projects\C#\LiteDBEditor\test\MyData.db"))
            {
                // Get customer collection
                var col = db.GetCollection<Customer>("customers");

                while (true)
                {
                    // Create your new customer instance
                    Console.WriteLine("Entries count: " + col.Count().ToString());

                    // Insert new customer document (Id will be auto-incremented)
                    //Console.WriteLine("Entry was added: " + (int)col.Insert(customer).RawValue);
                }
            }
        }
    }
}
