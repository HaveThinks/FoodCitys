﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace FoodCityBackstage.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FoodDBEntities5 : DbContext
    {
        public FoodDBEntities5()
            : base("name=FoodDBEntities5")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Bannder> Bannder { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Evaluation> Evaluation { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<PostType> PostType { get; set; }
        public virtual DbSet<RePly> RePly { get; set; }
        public virtual DbSet<ShipAddress> ShipAddress { get; set; }
        public virtual DbSet<ShopCar> ShopCar { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<PostEvaluation> PostEvaluation { get; set; }
        public virtual DbSet<BannderType> BannderType { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<User> User { get; set; }
    }
}
