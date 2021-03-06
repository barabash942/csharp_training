﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace Addressbook.Web.Tests
{
    public class GroupHelper: HelperBase
    {

    public GroupHelper(ApplicationManager manager) : base(manager)
    {
    }
        public void GroupCreatedCheck()
        {
            if (!IsAnyGroupCreated())
            {
                GroupData groupData = new GroupData();
                groupData.Name = "Qwe";
                groupData.Header = "rty";
                groupData.Footer = "ewyt";
                Create(groupData);
            }
        }

        
        private List<GroupData> groupCache = null;

        public List<GroupData> GetGroupList()
        {
            if (groupCache == null)
            {
                groupCache = new List<GroupData>();
                manager.Navigator.GoToGroupsPage();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("span.group"));

                foreach (IWebElement element in elements)
                {
                    groupCache.Add(new GroupData(null)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }

                string allGroupsNames = driver.FindElement(By.CssSelector("div#content form")).Text;
                string[] parts = allGroupsNames.Split('\n');
                int shift = groupCache.Count - parts.Length;
                for (int i = 0; i < groupCache.Count; i++)
                {
                    if (i < shift)
                    {
                        groupCache[i].Name = "";
                    }
                    else
                    {
                        groupCache[i].Name = parts[i - shift].Trim();
                    }
                }
            }

            return new List<GroupData>(groupCache);
        }

        public void GroupPageOpenCheck()
        {
            if (!IsGroupPageOpen())
            {
                manager.Navigator.GoToGroupsPage();
            }
        }

        public bool IsAnyGroupCreated()
        {
            return IsElementPresent(By.Name("selected[]"));
        }

        public bool IsGroupPageOpen()
        {
            return driver.Url == manager.Navigator.baseURL + "group.php";
        }

        public GroupHelper Create(GroupData groupdata)
        {
            InitGroupCreation();
            FillGroupForm(groupdata);
            SubmitGroupCreation();
            ReturnToGroupsPage();
            return this;
        }

        public int GetGroupsCount()
        {
            return driver.FindElements(By.CssSelector("span.group")).Count;
        }

        public GroupHelper Modify(int v, GroupData newData)
        {
            SelectGroup(v);
            InitGroupModification();
            FillGroupForm(newData);
            SubmitGroupModification();
            ReturnToGroupsPage();
            return this;
        }

        public void Modify(GroupData oldData, GroupData newData)
        {
            SelectGroup(oldData.Id);
            InitGroupModification();
            FillGroupForm(newData);
            SubmitGroupModification();
            ReturnToGroupsPage();
        }

        public GroupHelper Remove(int p)
        {
            SelectGroup(p);
            RemoveGroup();
            ReturnToGroupsPage();

            return this;
        }

        public void Remove(GroupData group)
        {
            SelectGroup(group.Id);
            RemoveGroup();
            ReturnToGroupsPage();
        }

    public GroupHelper InitGroupCreation()
       {
        driver.FindElement(By.Name("new")).Click();
        return this;
        }

    public GroupHelper FillGroupForm(GroupData groupdata)
        {
            Type(By.Name("group_name"), groupdata.Name);
            Type(By.Name("group_header"), groupdata.Header);
            Type(By.Name("group_footer"), groupdata.Footer);
            return this;
        }

        public GroupHelper SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            groupCache = null;
            return this;
        }

    public GroupHelper SelectGroup(int index)
    {
        driver.FindElement(By.XPath("(//input[@name= 'selected[]'])[" + (index+1) + "]")).Click();
        return this;
    }

        public GroupHelper SelectGroup(string id)
        {
            driver.FindElement(By.XPath("(//input[@name= 'selected[]' and @value='"+id+"'])")).Click();
            return this;
        }

        public GroupHelper ReturnToGroupsPage()
    {
        driver.FindElement(By.LinkText("groups")).Click();
        return this;
    }

    public GroupHelper RemoveGroup()
    {
        driver.FindElement(By.Name("delete")).Click();
        groupCache = null;
        return this;
    }
        public GroupHelper SubmitGroupModification()
        {
            driver.FindElement(By.Name("update")).Click();
            groupCache = null;
            return this;
        }

        public GroupHelper InitGroupModification()
        {
            driver.FindElement(By.Name("edit")).Click();
            return this;
        }
    }
}
