﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace MyKidsReg.Models;

public partial class ParentsRelation
{
    [Key]
    public int Id { get; set; }

    public int User_id { get; set; }

    public int Student_id { get; set; }

    [JsonIgnore]
    [ForeignKey("Student_id")]
    [InverseProperty("ParentsRelations")]
    public virtual Student Student { get; set; }
    [JsonIgnore]
    [ForeignKey("User_id")]
    [InverseProperty("ParentsRelations")]
    public virtual User User { get; set; }
}