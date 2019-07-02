using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Addressbook.Web.Tests
{
    public class RemovingContactFromGroupTest : AuthTestBase
    {
        [Test]
        public void TestRemovingContactFromGroup()
        {
            //Arrange
            app.Contacts.ContactCreatedCheck();
            app.Groups.GroupCreatedCheck();

            GroupData group = GroupData.GetAllFromDb()[0];
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = ContactData.GetAllFromDb().First();

            //Act
            app.Contacts.RemoveContactFromGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Remove(contact);
            newList.Sort();
            oldList.Sort();

            //Assert
            Assert.AreEqual(oldList, newList);
        }
    }
}
