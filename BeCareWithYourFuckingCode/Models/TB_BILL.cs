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
        public int ID { get; set; }
        public string USER_ACCOUNT_ID { get; set; }
        public string GAME_ACCOUNT_ID { get; set; }
        public Nullable<int> OFFER_PRICE { get; set; }
        public Nullable<int> SUCCESS_PRICE { get; set; }
        public Nullable<System.DateTime> BILL_DATE { get; set; }
        public Nullable<int> STATUS { get; set; }
    
        public virtual TB_GAME_ACCOUNT TB_GAME_ACCOUNT { get; set; }
        public virtual TB_USER TB_USER { get; set; }
    }
}
