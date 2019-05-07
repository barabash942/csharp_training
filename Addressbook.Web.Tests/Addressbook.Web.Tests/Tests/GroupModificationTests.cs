using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Addressbook.Web.Tests
{
    [TestFixture]
    public class GroupModificationTests : TestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            GroupData newData = new GroupData();
            newData.Name = "UpdatedText";
            newData.Header = "UpdatedHeader";
            newData.Footer = "updated footer";

            app.Groups.Modify(1, newData);
        }
    }
}
