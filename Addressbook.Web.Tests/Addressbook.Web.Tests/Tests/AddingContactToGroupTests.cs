using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Addressbook.Web.Tests
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void TestAdiingContactToGroup()
        {
            //Arrange
            GroupData group = GroupData.GetAllFromDb()[0];
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = ContactData.GetAllFromDb().Except(oldList).First();

            //Act
            app.Contacts.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();

            //Assert
            Assert.AreEqual(oldList, newList);
        }
    }
}
