﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Addressbook.Web.Tests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [SetUp]
        public void SetUp()
        {
            app.Contacts.OpenHomePageCheck();
            app.Contacts.ContactCreatedCheck();
        }

        [Test]
        public void ContactModificationTest()
        {
            ContactData newData = new ContactData();
            newData.FirstName = "Kate";
            newData.LastName = "Green";

            List<ContactData> oldContacts = app.Contacts.GetContactList();
            ContactData oldContactData = oldContacts[0];

            app.Contacts.Modify(0, 0, newData);

            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactsCount());

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts[1].FirstName = newData.FirstName;
            oldContacts[1].LastName = newData.LastName;
            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                if (contact.Id == oldContactData.Id)
                {
                    Assert.AreEqual(newData.FirstName, contact.FirstName);
                    Assert.AreEqual(newData.LastName, contact.LastName);
                }
            }
        }
    }
}
