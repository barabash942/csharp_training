using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Addressbook.Web.Tests;

namespace Addressbook.TestData.Generators
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataType = args[0];
            int count = Convert.ToInt32(args[1]);
            StreamWriter writer = new StreamWriter(args[2]);
            string format = args[3];

            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < count; i++)
            {
                groups.Add(new GroupData(TestBase.GenerateRandomString(10),
                    TestBase.GenerateRandomString(20),
                    TestBase.GenerateRandomString(25)));
            }

            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < count; i++)
            {
                contacts.Add(new ContactData(TestBase.GenerateRandomString(10),
                    TestBase.GenerateRandomString(15)));
            }

            if (dataType == "groups" && format == "csv")
            {
                writeGroupsToCsvFile(groups, writer);
            }
            else if (dataType == "groups" && format == "xml")
            {
                writeGroupsToXmlFile(groups, writer);
            }
            if (dataType == "contacts" && format == "xml")
            {
                writeContactsToXmlFile(contacts, writer);
            }
            else
            {
                System.Console.Out.Write("Unrecognized format: " + format);
            }
            writer.Close();
        }

        static void writeGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
            {
                writer.WriteLine(String.Format("${0},${1},${2}",
                    group.Name, group.Header, group.Footer));
            }
        }

        static void writeGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }

        static void writeContactsToXmlFile(List<ContactData> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);
        }
    }
}
