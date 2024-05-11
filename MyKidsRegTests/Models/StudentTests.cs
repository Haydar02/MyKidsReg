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
    public class StudentTests
    {
        private Student _ValidStudent1 = new Student { Name = "John", Last_name = "Doet" };
        private Student _NameEmptyStudent = new Student { Name = "", Last_name = "" };
        private Student _NameUnder2Student = new Student { Name = "A", Last_name = "Doe" };
        private Student _NameOver15Student = new Student { Name = "JohnathonMichaelSmith", Last_name = "Doe" };
        private Student _LastNameEmptyStudent = new Student { Name = "John", Last_name = "" };
        private Student _LastNameUnder2Student = new Student { Name = "John", Last_name = "D" };
        private Student _LastNameOver15Student = new Student { Name = "John", Last_name = "JohnsonJohnsonson" };


        [TestMethod()]
        public void StudentValidateTest_ValidStudent()
        {
            _ValidStudent1.StudentValidate();
        }

        [TestMethod()]
        public void NameValidateTest_EmptyName()
        {
            _ValidStudent1.StudentValidate();
            Assert.ThrowsException<ArgumentNullException>(() => _NameEmptyStudent.NameValidate());
        }

        [TestMethod()]
        public void NameValidateTest_NameUnder2()
        {
            _ValidStudent1.StudentValidate();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _NameUnder2Student.NameValidate());
        }

        [TestMethod()]
        public void NameValidateTest_NameOver15()
        {
            _ValidStudent1.StudentValidate();
            _NameOver15Student.NameValidate();
        }

        [TestMethod()]
        public void LastNameValidateTest_EmptyLastName()
        {
            _ValidStudent1.StudentValidate();
            Assert.ThrowsException<ArgumentNullException>(() => _LastNameEmptyStudent.LastNameValidate());
        }

        [TestMethod()]
        public void LastNameValidateTest_LastNameUnder2()
        {
            _ValidStudent1.StudentValidate();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _LastNameUnder2Student.LastNameValidate());
        }

        [TestMethod()]
        public void LastNameValidateTest_LastNameOver15()
        {
            _ValidStudent1.StudentValidate();
            _LastNameOver15Student.LastNameValidate();
        }
    }
}