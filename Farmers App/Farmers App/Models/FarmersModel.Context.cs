﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Farmers_App.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Farmer_V2Entities1 : DbContext
    {
        public Farmer_V2Entities1()
            : base("name=Farmer_V2Entities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<FARM_TYPES> FARM_TYPES { get; set; }
        public virtual DbSet<FARMER> FARMERS { get; set; }
        public virtual DbSet<GENDER> GENDERS { get; set; }
        public virtual DbSet<PROVINCE> PROVINCES { get; set; }
    }
}
