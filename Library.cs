using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace cchtask
{
    class Library
    {
        // practical
        private DataTable resourceData;
        private DataTable availabilityData;

        public Library()
        {
            resourceData = new DataTable("resource");
            availabilityData = new DataTable("availability");
        }

        private void Load()
        {
            resourceData.ReadXmlSchema("..\\resource.xsd");
            resourceData.ReadXml("..\\resource.xml");

            availabilityData.ReadXmlSchema("..\\availability.xsd");
            availabilityData.ReadXml("..\\availability.xml");
        }

        private void Save()
        {
            availabilityData.WriteXml("..\\availability.xml");
        }

        public void List(string sortBy)
        {
            int id;
            bool availability;
            DataRow row;

            Load();
            resourceData.DefaultView.Sort = sortBy;

            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine("id | title           | author             | publisher       | date | available?");
            Console.WriteLine("------------------------------------------------------------------------------");
            foreach (DataRowView rowView in resourceData.DefaultView)
            {
                row = rowView.Row;
                id = row.Field<int>("id");
                availability = availabilityData.Rows[id].Field<bool>("available");
                Console.WriteLine("{0,2} | {1,-15} | {2,-18} | {3,-15} | {4:yyyy} | {5}", row["id"], row["title"], row["author"], row["publisher"], row["date"], availability);
            }
        }

        public void List()
        {
            List("id");
        }

        public void CheckIn(int id)
        {
            Load();
            availabilityData.Rows[id]["available"] = true;
            Save();
        }

        public void CheckOut(int id)
        {
            Load();
            availabilityData.Rows[id]["available"] = false;
            Save();
        }

    }
}