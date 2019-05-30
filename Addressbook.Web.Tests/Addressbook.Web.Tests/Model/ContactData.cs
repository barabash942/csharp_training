using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Addressbook.Web.Tests
{
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

        public string FirstName { get; set; }

        public string LastName { get; set; }

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
                if (allContactDetails != null)
                {
                    return allContactDetails;
                }
                else
                {
                    return MakeContactDetailsToTest(allContactDetails).Trim();
                }
            }
            set
            {
                allContactDetails = value;
            }
        }

        public string MakeContactDetailsToTest(string allContactDetails)
        {
            if (FirstName == null || FirstName == "")
            {
                return "";
            }
            return FirstName + " ";

            if (LastName == null || LastName == "")
            {
                return "";
            }
            return LastName + "\r\n";

            if (Address == null || Address == "")
            {
                return "";
            }
            return Address + "\r\n" + "\r\n";

            if (HomePhone == null || HomePhone == "")
            {
                return "";
            }
            return "H: " + HomePhone + "\r\n";

            if (MobilePhone == null || MobilePhone == "")
            {
                return "";
            }
            return "M: " + MobilePhone + "\r\n";

            if (WorkPhone == null || WorkPhone == "")
            {
                return "";
            }
            return "W: " + WorkPhone + "\r\n";

            if (Email == null || Email == "")
            {
                return "";
            }
            return Email + "\r\n";

            if (Email2 == null || Email2 == "")
            {
                return "";
            }
            return Email2 + "\r\n";

            if (Email3 == null || Email3 == "")
            {
                return "";
            }
            return Email3;
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
            return "LastName= " + LastName + "\nFirstName = " + FirstName;
        }
    }
}
