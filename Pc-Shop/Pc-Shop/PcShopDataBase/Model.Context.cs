﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PcShopDataBase
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Database1Entities : DbContext
    {
        public Database1Entities()
            : base("name=Database1Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Felhasznalo> Felhasznaloes { get; set; }
        public virtual DbSet<PC> PCs { get; set; }
        public virtual DbSet<PcAlkatreszek> PcAlkatreszeks { get; set; }
        public virtual DbSet<PcRendelesre> PcRendelesres { get; set; }
        public virtual DbSet<Raktar> Raktars { get; set; }
        public virtual DbSet<Rendelesek> Rendeleseks { get; set; }
        public virtual DbSet<Uzenetek> Uzeneteks { get; set; }
    }
}