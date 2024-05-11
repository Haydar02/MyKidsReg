﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyKidsReg.Models;

[Keyless]
public partial class ParentsRelation
{
    public int User_id { get; set; }

    public int Student_id { get; set; }

    [ForeignKey("Student_id")]
    public virtual Student Student { get; set; }

    [ForeignKey("User_id")]
    public virtual User User { get; set; }
}