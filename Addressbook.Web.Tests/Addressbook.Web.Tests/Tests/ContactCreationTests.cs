using NUnit.Framework;

namespace Addressbook.Web.Tests
{
    [TestFixture]
    public class ContactCreationTest : AuthTestBase
    {
        [Test]
        public void ContactCreationTestCase()
        {
            ContactData contact = new ContactData();
            contact.FirstName = "Ann";
            contact.LastName = "Brown";

            app.Contacts.Create(contact);
        }
    }
}
