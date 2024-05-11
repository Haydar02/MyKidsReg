﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyKidsReg.Models;

[Table("Student")]
public partial class Student
{
    [Key]
    public int Id { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string Name { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string Last_name { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string Birthday { get; set; }

    public int? Department_id { get; set; }

    [ForeignKey("Department_id")]
    [InverseProperty("Students")]
    public virtual Department Department { get; set; }

    public void NameValidate()
    {
        if (Name == null)
        {
            throw new ArgumentNullException("Navn skal angives.");
        }
        else if (Name.Length < 2 || Name.Length > 15)
        {
            throw new ArgumentOutOfRangeException("Navn skal være mellem 2 og 15 tegn.");
        }
    }

    public void LastNameValidate()
    {
        if (Last_name == null)
        {
            throw new ArgumentNullException("Efternavn skal angives.");
        }
        else if (Last_name.Length < 2 || Last_name.Length > 15)
        {
            throw new ArgumentOutOfRangeException("Efternavn skal være mellem 2 og 15 tegn.");
        }
    }

    public void StudentValidate()
    {
        NameValidate();
        LastNameValidate();
    }
}