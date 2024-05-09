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
        private User _user = new User { User_Id = 1, User_Name = "validusername", Password = "password", Name = "John", Last_name = "Wilson", Address = "123 Main St", Zip_code = 1234, E_mail = "john@example.com", Mobil_nr = 1234567890, Usertype = User_type.Admin };
        private User _userWithNullUsername = new User { User_Id = 2, User_Name = null, Password = "password", Name = "John", Last_name = "Madsem", Address = "123 Main St", Zip_code = 12345, E_mail = "john@example.com", Mobil_nr = 1234567890, Usertype = User_type.Admin };
        private User _userWithShortUsername = new User { User_Id = 3, User_Name = "abc", Password = "password", Name = "John", Last_name = "Andersen", Address = "123 Main St", Zip_code = 12345, E_mail = "john@example.com", Mobil_nr = 1234567890, Usertype = User_type.Admin };
        private User _userWithLongUsername = new User { User_Id = 4, User_Name = "abcdefghijabcdefghijabcdefghij", Password = "password", Name = "Lo Siento", Last_name = "Doe", Address = "123 Main St", Zip_code = 12345, E_mail = "mike@example.com", Mobil_nr = 1234567890, Usertype = User_type.Admin };
        private User _phoneNumberLess = new User { User_Id = 4, User_Name = "abcdefghijabcdefghijabcdefghij", Password = "password", Name = "Lo Siento", Last_name = "Doe", Address = "123 Main St", Zip_code = 12345, E_mail = "mike@example.com", Mobil_nr = +123456, Usertype = User_type.Admin };
        private User _phoneEmpty = new User { User_Id = 4, User_Name = "abcdefghijabcdefghijabcdefghij", Password = "password", Name = "Lo Siento", Last_name = "Doe", Address = "123 Main St", Zip_code = 12345, E_mail = "mike@example.com", Mobil_nr = 0, Usertype = User_type.Admin };

        private User _Zip_CodeEmpty = new User { User_Id = 2, User_Name = "abcdefghijabcdefghijabcdefghij", Password = "password", Name = "Lo Siento", Last_name = "Doe", Address = "123 Main St", Zip_code = 0, E_mail = "mike@example.com", Mobil_nr = 0, Usertype = User_type.Admin };
        private User _Zip_CodeLessThan4Number = new User { User_Id = 3, User_Name = "abcdefghijabcdefghijabcdefghij", Password = "password", Name = "Lo Siento", Last_name = "Doe", Address = "123 Main St", Zip_code = 999, E_mail = "mike@example.com", Mobil_nr = 0, Usertype = User_type.Admin };
        private User _Zip_CodeMoreThan4Number = new User { User_Id = 4, User_Name = "abcdefghijabcdefghijabcdefghij", Password = "password", Name = "Lo Siento", Last_name = "Doe", Address = "123 Main St", Zip_code = 10000, E_mail = "mike@example.com", Mobil_nr = 0, Usertype = User_type.Admin };
        private User userWithValidZipCode = new User { User_Id = 5, User_Name = "abcdefghijabcdefghijabcdefghij", Password = "password", Name = "Lo Siento", Last_name = "Doe", Address = "123 Main St", Zip_code = 1234, E_mail = "mike@example.com", Mobil_nr = 0, Usertype = User_type.Admin };
        [TestMethod()]
        public void PhoneNrValidateTest()
        {
            _user.PhoneNrValidate();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _phoneNumberLess.PhoneNrValidate());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _phoneEmpty.PhoneNrValidate());

        }

        [TestMethod()]
        public void UsernameValidateTest_ValidUsername()
        {
            _user.UsernameValidate();
        }

        [TestMethod()]
        public void UsernameValidateTest_NullUsername()
        {
            _user.UsernameValidate();
            Assert.ThrowsException<ArgumentNullException>(() => _userWithNullUsername.UsernameValidate());
        }

        [TestMethod()]
        public void UsernameValidateTest_ShortUsername()
        {
            _user.UsernameValidate();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _userWithShortUsername.UsernameValidate());
        }

        [TestMethod()]
        public void UsernameValidateTest_LongUsername()
        {
            _user.UsernameValidate();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _userWithLongUsername.UsernameValidate());
        }

        [TestMethod()]
        public void ZipCodeValidateTest()
        {
            _user.ZipCodeValidate();
            Assert.AreEqual(4, _user.Zip_code.ToString().Length);
            Assert.IsTrue(_user.Zip_code == userWithValidZipCode.Zip_code);

            Assert.ThrowsException<ArgumentNullException>(() => _Zip_CodeEmpty.ZipCodeValidate());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _Zip_CodeLessThan4Number.ZipCodeValidate());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _Zip_CodeMoreThan4Number.ZipCodeValidate());
        }
    }
}