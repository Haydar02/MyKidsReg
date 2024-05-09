using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyKidsReg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyKidsReg.Models.Tests
{
    [TestClass()]
    public class UserTests
    {
        private User _userWithValidUsername = new User { User_Id = 1, User_Name = "validusername", Password = "password", Name = "John", Last_name = "Wilson", Address = "123 Main St", Zip_code = 12345, E_mail = "john@example.com", Mobil_nr = 1234567890, Usertype = User_type.Admin };
        private User _userWithNullUsername = new User { User_Id = 2, User_Name = null, Password = "password", Name = "John", Last_name = "Madsem", Address = "123 Main St", Zip_code = 12345, E_mail = "john@example.com", Mobil_nr = 1234567890, Usertype = User_type.Admin };
        private User _userWithShortUsername = new User { User_Id = 3, User_Name = "abc", Password = "password", Name = "John", Last_name = "Andersen", Address = "123 Main St", Zip_code = 12345, E_mail = "john@example.com", Mobil_nr = 1234567890, Usertype = User_type.Admin };
        private User _userWithLongUsername = new User { User_Id = 4, User_Name = "abcdefghijabcdefghijabcdefghij", Password = "password", Name = "Lo Siento", Last_name = "Doe", Address = "123 Main St", Zip_code = 12345, E_mail = "mike@example.com", Mobil_nr = 1234567890, Usertype = User_type.Admin };


        [TestMethod()]
        public void PhoneNrValidateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UsernameValidateTest_ValidUsername()
        {
            _userWithValidUsername.UsernameValidate();
        }

        [TestMethod()]
        public void UsernameValidateTest_NullUsername()
        {
            _userWithValidUsername.UsernameValidate();
            Assert.ThrowsException<ArgumentNullException>(() => _userWithNullUsername.UsernameValidate());
        }

        [TestMethod()]
        public void UsernameValidateTest_ShortUsername()
        {
            _userWithValidUsername.UsernameValidate();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _userWithShortUsername.UsernameValidate());
        }

        [TestMethod()]
        public void UsernameValidateTest_LongUsername()
        {
            _userWithValidUsername.UsernameValidate();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _userWithLongUsername.UsernameValidate());
        }

        [TestMethod()]
        public void ZipCodeValidateTest()
        {
            Assert.Fail();
        }
    }
}