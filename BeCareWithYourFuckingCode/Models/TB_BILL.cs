//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class TB_BILL
    {
        public TB_BILL(string GAME_ACCOUNT_ID, string OFFER_PRICE, string SUCCESS_PRICE)
        {
            this.GAME_ACCOUNT_ID = GAME_ACCOUNT_ID;
            this.OFFER_PRICE = int.Parse(OFFER_PRICE);
            this.SUCCESS_PRICE = int.Parse(SUCCESS_PRICE);
        }
        public int ID { get; set; }
        public string USER_ACCOUNT_ID { get; set; }
        public string GAME_ACCOUNT_ID { get; set; }
        public Nullable<int> OFFER_PRICE { get; set; }
        public Nullable<int> SUCCESS_PRICE { get; set; }
        public Nullable<System.DateTime> BILL_DATE { get; set; }
    
        public virtual TB_GAME_ACCOUNT TB_GAME_ACCOUNT { get; set; }
        public virtual TB_USER TB_USER { get; set; }
    }
}
