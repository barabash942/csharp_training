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

            app.Contacts.Modify(1, 1, newData);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts[1].FirstName = newData.FirstName;
            oldContacts[1].LastName = newData.LastName;
            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
