using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingForAddressBook
{
    public  class AddressBookData
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public Int64 zip { get; set; }
        public Int64 phoneNumber { get; set; }
        public string Email { get; set; }
    }
}
