using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;

namespace Addressbook.Web.Tests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;
        private string allContactDetails;

        public ContactData(string lastName, string firstName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public ContactData()
        {

        }

        public ContactData(string allContactDetails)
        {
            AllContactDetails = allContactDetails;
        }

        [Column("firstname")]
        public string FirstName { get; set; }

        [Column("lastname")]
        public string LastName { get; set; }

        [Column("id"), PrimaryKey]
        public string Id { get; set; }

        public string Address { get; set; }

        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanupPhones(HomePhone) + CleanupPhones(MobilePhone) + CleanupPhones(WorkPhone)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (MakeEmailToConcat(Email) + MakeEmailToConcat(Email2) + MakeEmailToConcat(Email3)).Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }

        public string AllContactDetails
        {
            get
            {
                if (allContactDetails != null || allContactDetails != "")
                {
                    return allContactDetails;
                }
                else
                {
                    allContactDetails = "";
                    if (FirstName == null || FirstName == "")
                    {
                        allContactDetails += FirstName + " ";
                    }

                    if (LastName == null || LastName == "")
                    {
                        allContactDetails += LastName + "\r\n";
                    }

                    if (Address == null || Address == "")
                    {
                        allContactDetails += Address + "\r\n\r\n";
                    }

                    if (HomePhone == null || HomePhone == "")
                    {
                        allContactDetails += "H: " + HomePhone + "\r\n";
                    }

                    if (MobilePhone == null || MobilePhone == "")
                    {
                        allContactDetails += "M: " + MobilePhone + "\r\n";
                    }

                    if (WorkPhone == null || WorkPhone == "")
                    {
                        allContactDetails += "W: " + WorkPhone + "\r\n";
                    }

                    if (Email == null || Email == "")
                    {
                        allContactDetails += Email + "\r\n";
                    }

                    if (Email2 == null || Email2 == "")
                    {
                        allContactDetails += Email2 + "\r\n";
                    }

                    if (Email3 == null || Email3 == "")
                    {
                        allContactDetails += Email3;
                    }

                    return allContactDetails.Trim();
                }
            }
            set
            {
                allContactDetails = value;
            }
        }

        private string CleanupPhones(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[ -()]", "") + "\r\n";
        }

        private string MakeEmailToConcat(string email)
        {
            if (email == null || email == "")
            {
                return "";
            }
            return email + "\r\n";
        }

        public string HomePhone { get; set; }

        public string MobilePhone { get; set; }

        public string WorkPhone { get; set; }

        public string Email { get; set; }

        public string Email2 { get; set; }

        public string Email3 { get; set; }

        private string DisplayName
        {
            get { return $"{FirstName} {LastName}"; }
        }

        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }

            return
                LastName.CompareTo(other.LastName) != 0
                    ? LastName.CompareTo(other.LastName)
                    : FirstName.CompareTo(other.FirstName);
        }

        public static List<ContactData> GetAllFromDb()
        {
            using (AddressbookDB db = new AddressbookDB())
            {
                return (from c in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:00") select c).ToList();
            }
        }

        public List<GroupData> GetGroups()
        {
            using (AddressbookDB db = new AddressbookDB())
            {
                List<GroupData> groups =
                    (from g in db.Groups
                     from gcr in db.GCR.Where(p => p.ContactId == Id && p.GroupId == g.Id)
                     select g).Distinct().ToList();
                return groups;
            }
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            return LastName == other.LastName && FirstName == other.FirstName;
        }

        public override int GetHashCode()
        {
            return DisplayName.GetHashCode();
        }

        public override string ToString()
        {
            return "FirstName= " + FirstName + "LastName= " + LastName 
                + "\rAddress = " + Address
                + "\rHomePhone = " + HomePhone + "\rMobilePhone = " 
                + MobilePhone + "\rWorkPhone = " + WorkPhone
                + "\rEmail = " + Email + "\rEmail2 = " 
                + Email2 + "\rEmail3 = " + Email3;
        }
    }
}