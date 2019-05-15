using NUnit.Framework;

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

            app.Contacts.Create(contact);
        }
    }
}
