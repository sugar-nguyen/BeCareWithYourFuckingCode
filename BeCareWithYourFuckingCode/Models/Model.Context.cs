﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BeCareWithYourFuckingCode.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WEBACCOUNTEntities : DbContext
    {
        public WEBACCOUNTEntities()
            : base("name=WEBACCOUNTEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TB_ADMIN> TB_ADMIN { get; set; }
        public virtual DbSet<TB_GAME_ACCOUNT> TB_GAME_ACCOUNT { get; set; }
        public virtual DbSet<TB_GAME_IMAGE> TB_GAME_IMAGE { get; set; }
        public virtual DbSet<TB_GAME_PRICE_OFFER> TB_GAME_PRICE_OFFER { get; set; }
        public virtual DbSet<TB_RANK_NAME> TB_RANK_NAME { get; set; }
        public virtual DbSet<TB_SELL_DEAL_HISTORY> TB_SELL_DEAL_HISTORY { get; set; }
        public virtual DbSet<TB_USER> TB_USER { get; set; }
        public virtual DbSet<TB_MONEY> TB_MONEY { get; set; }
        public virtual DbSet<TB_GAME_ACCOUNT_DETAIL> TB_GAME_ACCOUNT_DETAIL { get; set; }
        public virtual DbSet<TB_BILL> TB_BILL { get; set; }
        public virtual DbSet<TB_CARD_DEAL_HISTORY> TB_CARD_DEAL_HISTORY { get; set; }
    }
}
