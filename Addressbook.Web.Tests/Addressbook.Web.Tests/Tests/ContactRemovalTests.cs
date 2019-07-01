using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace Addressbook.Web.Tests
{
    [TestFixture]
    public class ContactRemovalTests : ContactTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            if (ContactData.GetAllFromDb().Count == 0)
            {
                string firstname = "UserNameForTest";
                string lastname = "UserFamilyForTest";
                ContactData contactForRemoving = new ContactData(lastname, firstname);
                app.Contacts.Create(contactForRemoving);
            }

            List<ContactData> oldContacts = ContactData.GetAllFromDb();
            
            app.Contacts.Remove(0);

            List<ContactData> newContacts = ContactData.GetAllFromDb();

            Assert.AreEqual(oldContacts.Count - 1, newContacts.Count);

            ContactData toBeRemoved = oldContacts[0];
            oldContacts.RemoveAt(0);

            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
            }
        }
    }
}
