﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MasterFloor.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MasterFloorsEntities : DbContext
    {
        public static MasterFloorsEntities _context;
        public static MasterFloorsEntities GetContext()
        {
            if (_context == null)
            {
                _context = new MasterFloorsEntities();
            }
            return _context;
        }
        public MasterFloorsEntities()
            : base("name=MasterFloorsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Adress> Adress { get; set; }
        public virtual DbSet<CountryIndex> CountryIndex { get; set; }
        public virtual DbSet<Countrys> Countrys { get; set; }
        public virtual DbSet<DirectorName> DirectorName { get; set; }
        public virtual DbSet<MaterialType> MaterialType { get; set; }
        public virtual DbSet<OrganizeName> OrganizeName { get; set; }
        public virtual DbSet<Partner> Partner { get; set; }
        public virtual DbSet<PartnerProducts> PartnerProducts { get; set; }
        public virtual DbSet<PartnetName> PartnetName { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Regions> Regions { get; set; }
        public virtual DbSet<Streets> Streets { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TypeProduct> TypeProduct { get; set; }
    }
}
