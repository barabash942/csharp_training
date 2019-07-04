using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Addressbook.Tests.Autoit
{
    public class TestBase
    {
        public ApplicationManager app;


        [OneTimeSetUp]
        public void initApplication()
        {
            app = new ApplicationManager();
        }

        [OneTimeTearDown]
        public void stopApplication()
        {
            app.Stop();
        }
    }
}
