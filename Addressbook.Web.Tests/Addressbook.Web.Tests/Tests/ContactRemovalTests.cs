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
    public class ContactRemovalTests : AuthTestBase
    {
        [SetUp]
        public void SetUp()
        {
            app.Contacts.OpenHomePageCheck();
            app.Contacts.ContactCreatedCheck();
        }

        [Test]
        public void ContactRemovalTest()
        {
            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.Remove(1);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.RemoveAt(1);

            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
