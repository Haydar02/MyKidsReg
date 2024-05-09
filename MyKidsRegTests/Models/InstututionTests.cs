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
        private Instutution _NameEmptyInstitution = new Instutution { Id = 1, Name = null, Zip_Code = 4000, Address = "Lysalleen 31 " };
        private Instutution _NameUnder5Institution = new Instutution { Id = 1, Name = "hele", Zip_Code = 4000, Address = "Lysalleen 31 " };
        private Instutution _NameOver15Institution = new Instutution { Id = 1, Name = "hele4hele4hele15", Zip_Code = 4000, Address = "Hej 31 " };
        private Instutution _institutionWithLongZipCode = new Instutution { Id = 3, Name = "Raberparken", Zip_Code = 123456, Address = "Ukendt" };
        private Instutution _instututionLessThan4 = new Instutution { Id = 23, Name = "Mosehaven", Zip_Code = 1, Address = "" };
        private Instutution _instutution = new Instutution { Id = 1, Name = "Spirrebakken", Zip_Code = 4000, Address = "Lysalleen 31 " };


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
    }
}