﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace MyKidsReg.Models;

[Table("Institution")]
public partial class Institution
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; }

    [Required]
    [StringLength(255)]
    [Unicode(false)]
    public string Address { get; set; }

    public int Zip_Code { get; set; }

    public long Tlf_Number { get; set; }

    [JsonIgnore]
    [InverseProperty("Institution")]
    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    [JsonIgnore]
    [InverseProperty("User")]
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public void NameValidate()
    {
        if (Name == null)
        {
            throw new ArgumentNullException("Angiv venligst et navn");
        }
        if (Name.Length <= 5 || Name.Length >= 15)
        {
            throw new ArgumentOutOfRangeException("Navnet skal mindst være 5 tegn og maksimalt 15 tegn");
        }
    }
    public void AddressValidate()
    {
        if (string.IsNullOrEmpty(Address))
        {
            throw new ArgumentNullException("Angiv venligst en addresse");
        }
    }

    public void ZipCodeValidate()
    {
        if (Zip_Code < 999)
        {
            throw new ArgumentOutOfRangeException("Postnummeret skal være 4 tegn langt");
        }
        if (Zip_Code > 11111)
        {
            throw new ArgumentOutOfRangeException("Postnummeret skal være 4 tegn langt");
        }
    }

    public void InstitutionValidate()
    {
        NameValidate();
        ZipCodeValidate();
        AddressValidate();
    }

}