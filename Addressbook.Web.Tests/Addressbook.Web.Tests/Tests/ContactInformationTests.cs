﻿using NUnit.Framework;
using System.Collections.Generic;

namespace Addressbook.Web.Tests
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {
        [SetUp]
        public void SetUp()
        {
            app.Contacts.OpenHomePageCheck();
        }

        [Test]
        public void ContactInformationTest()
        {
            ContactData fromTable = app.Contacts.GetContactInfoFromTable(0);
            ContactData fromForm = app.Contacts.GetContactInfoFromEditForm(0);

            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);

        }

        [Test]
        public void ContactInformationDetailsTest()
        {
            ContactData fromDetails = app.Contacts.GetContactInfoFromDetails(0);
            ContactData fromTable = app.Contacts.GetContactInfoFromTable(0);

            Assert.AreEqual(fromTable.AllContactDetails, fromDetails.AllContactDetails);
        }
    }
}
