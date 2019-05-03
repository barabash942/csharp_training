using NUnit.Framework;

namespace Addressbook.Web.Tests
{
    [TestFixture]
    public class ContactCreationTest : TestBase
    {
        [Test]
        public void ContactCreationTestCase()
        {
            OpenHomePage();
            Login(new AccountData("admin", "secret"));
            InitContactCreation();
            ContactData contact = new ContactData();
            contact.FirstName = "Ann";
            contact.LastName = "Brown";
            FillContactForm(contact);
            SubmitContactCreation();
            Logout();
        }
    }
}
