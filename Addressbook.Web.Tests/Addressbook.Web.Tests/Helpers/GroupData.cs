﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addressbook.Web.Tests
{
    class GroupData
    {
        private string name;
        private string header = "";
        private string footer = "";

        public GroupData()
        {
           
        }

        public GroupData(string name, string header, string footer)
        {
            this.name = name;
            this.header = header;
            this.footer = footer;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Header
        {
            get { return header; }
            set { header = value; }
        }

        public string Footer
        {
            get { return footer; }
            set { footer = value; }
        }
    }
}
