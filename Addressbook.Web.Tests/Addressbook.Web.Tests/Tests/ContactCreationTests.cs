using NUnit.Framework;
using System.Collections.Generic;

namespace Addressbook.Web.Tests
{
    [TestFixture]
    public class ContactCreationTest : AuthTestBase
    {
        [SetUp]
        public void SetUp()
        {
            app.Contacts.OpenHomePageCheck();
        }

        [Test]
        public void ContactCreationTestCase()
        {
            ContactData contact = new ContactData();
            contact.FirstName = "Ann";
            contact.LastName = "Brown";

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.Create(contact);
        }
    }
}
