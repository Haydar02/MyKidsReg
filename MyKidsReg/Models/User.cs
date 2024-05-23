﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyKidsReg.Services.CommunicationsServices;

namespace MyKidsReg.Models;
public enum User_type
{
    Super_Admin,
    Admin,
    Padagogue,
    Parent
}

public partial class User
{
    [Key]
    public int User_Id { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string User_Name { get; set; }

    [Required]
    [StringLength(255)]
    [Unicode(false)]
    public string Password { get; set; }

    [Required]
    [StringLength(30)]
    [Unicode(false)]
    public string Name { get; set; }

    [Required]
    [StringLength(30)]
    [Unicode(false)]
    public string Last_name { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Address { get; set; }

    public int Zip_code { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string E_mail { get; set; }

    public long Mobil_nr { get; set; }

    [Required]
    // [StringLength(15)]
    [Unicode(false)]
    public User_type Usertype { get; set; }

    [JsonIgnore]
    [InverseProperty("User")]
    public virtual ICollection<AdminRelation> AdminRelations { get; set; } = new List<AdminRelation>();
    [JsonIgnore]
    [InverseProperty("UserNavigation")]
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
    [JsonIgnore]
    [InverseProperty("User")]
    public virtual ICollection<ParentsRelation> ParentsRelations { get; set; } = new List<ParentsRelation>();
    [JsonIgnore]
    [InverseProperty("User")]
    public virtual ICollection<TeacherRelation> TeacherRelations { get; set; } = new List<TeacherRelation>();

    [JsonIgnore]
    [NotMapped]
    public DateTime? TemporaryPasswordExpiration { get; set; }


    public bool IsTemporaryPasswordExpired()
    {
        // Kontroller om der er en udløbstid og om den er større end den aktuelle tid
        if (TemporaryPasswordExpiration.HasValue && TemporaryPasswordExpiration.Value > DateTime.Now)
        {
            // Midlertidig adgangskode er gyldig
            return false;
        }
        else
        {
            // Midlertidig adgangskode er udløbet
            return true;
        }
    }
    public void HashPassword(PasswordService passwordService)
    {
        Password = passwordService.HashPassword(Password);
    }

    // Metode til at verificere en indtastet adgangskode
    public bool VerifyPassword(PasswordService passwordService, string enteredPassword)
    {
        return passwordService.VerifyPassword(enteredPassword, Password);
    }

    public void UsernameValidate()
    {
        if (User_Name == null)
        {
            throw new ArgumentNullException("Angiv venligst et navn");
        }
        if (User_Name.Length <= 5 || User_Name.Length >= 15)
        {
            throw new ArgumentOutOfRangeException("Brugernavnet skal mindst være 5 tegn og maksimalt 15 tegn");
        }
    }

    public void ZipCodeValidate()
    {
        if (Zip_code == 0)
        {
            throw new ArgumentNullException("Udfyld postnummer tak. den må ikke være tom");
        }
        if (Zip_code < 1000 || Zip_code > 9999)
        {
            throw new ArgumentOutOfRangeException("Postnummeret skal være præcis 4 cifre langt");
        }
    }

    public void AddressValidate()
    {
        if (string.IsNullOrEmpty(Address))
        {
            throw new ArgumentNullException("Angiv venligst en addresse");
        }
    }

    public void EmailValidate()
    {
        if (string.IsNullOrEmpty(E_mail))
        {
            throw new ArgumentNullException("Angiv venligst en email");
        }
    }

    public void PhoneNrValidate()
    {
        if (Mobil_nr <= 0)
        {
            throw new ArgumentOutOfRangeException("Mobilnummeret må ikke være nul eller negativt");
        }
        if (Mobil_nr < 9999999 || Mobil_nr >= 999999999999)
        {
            throw new ArgumentOutOfRangeException("Mobilnummeret skal være mellem 8 og 12 cifre");
        }

        string mobilNrString = Mobil_nr.ToString();
        if ((mobilNrString.StartsWith("0") || mobilNrString.StartsWith("+")))
        {
            throw new ArgumentException("Mobilnummeret skal starte med '0' eller '+'");
        }
    }


    public void PasswordValidate()
    {
        if (string.IsNullOrEmpty(Password))
        {
            throw new ArgumentNullException("Adgangskode må ikke være tomt");
        }
        if (Password.Length < 4 || Password.Length > 9)
        {
            throw new ArgumentOutOfRangeException("Adgangskoden skal være mellem 4 og 9 tegn langt");
        }
        }
        //public void UserTypeValidate()
        //{
        //    if (!Enum.IsDefined(typeof(User_type), Usertype))
        //    {
        //        throw new ArgumentOutOfRangeException("Usertype is not a valid value.");
        //    }
        //}

        public void UserValidate()
        {
            UsernameValidate();
            ZipCodeValidate();
            PhoneNrValidate();
            // PasswordValidate();
            AddressValidate();
            EmailValidate();
            //UserTypeValidate();
        }
    }
public static class UserExtensions
{
    public static string ToText(this User_type userType)
    {
        switch (userType)
        {
            case User_type.Super_Admin:
                return "Super Admin";
            case User_type.Admin:
                return "Admin";
            case User_type.Padagogue:
                return "Padagogue";
            case User_type.Parent:
                return "Parent";
            default:
                throw new ArgumentOutOfRangeException(nameof(userType), userType, "Unsupported user type");
        }
    }
}