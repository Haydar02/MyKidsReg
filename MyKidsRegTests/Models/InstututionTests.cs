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
    public class InstututionTests
    {
        private Institution _institutionWithValidAddress = new Institution { Id = 1, Name = "Spirrebakken", Zip_Code = 4000, Address = "Lysalleen 31" };
        private Institution _institutionWithEmptyAddress = new Institution { Id = 2, Name = "Mosehaven", Zip_Code = 5000, Address = "" };
        private Institution _NameEmptyInstitution = new Institution { Id = 1, Name = null, Zip_Code = 4000, Address = "Lysalleen 31 " };
        private Institution _NameUnder5Institution = new Institution { Id = 1, Name = "hele", Zip_Code = 400, Address = "Lysalleen 31 " };
        private Institution _NameOver15Institution = new Institution { Id = 1, Name = "hele4hele4hele15", Zip_Code = 40000, Address = "Hej 31 " };
        private Institution _institutionWithLongZipCode = new Institution { Id = 363, Name = "Raberparken", Zip_Code = 123446, Address = "Ukendt" };
        private Institution _instututionLessThan4 = new Institution { Id = 23, Name = "Mosehaven", Zip_Code = 1, Address = "" };
        private Institution _instutution = new Institution { Id = 1, Name = "Spirrebakken", Zip_Code = 4000, Address = "Lysalleen 31 " };



        [TestMethod()]
        public void ZipCodeValidateTest_ZipCodeLessThan4() 
        {
            _instutution.ZipCodeValidate();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _instututionLessThan4.ZipCodeValidate());
        }

        [TestMethod()]
        public void ZipCodeValidateTest_LongZipCode() 
        {
           _instutution.ZipCodeValidate();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _institutionWithLongZipCode.ZipCodeValidate());
        }

        [TestMethod()]
        public void NameValidateTest_EmptyName()
        {
            _instutution.NameValidate();
            Assert.ThrowsException<ArgumentNullException>(() => _NameEmptyInstitution.NameValidate());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _NameUnder5Institution.NameValidate());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _NameOver15Institution.NameValidate());
        }
        [TestMethod()]
        public void AddressValidateTest_ValidAddress()
        {
            _institutionWithValidAddress.AddressValidate();
        }

        [TestMethod()]
        public void AddressValidateTest_EmptyAddress()
        {
            _institutionWithValidAddress.AddressValidate();
            Assert.ThrowsException<ArgumentNullException>(() => _institutionWithEmptyAddress.AddressValidate());
        }
    }
}