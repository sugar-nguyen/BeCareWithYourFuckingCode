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
    
    public partial class TB_SELL_DEAL_HISTORY
    {
        public string HIS_ID { get; set; }
        public Nullable<System.DateTime> HIS_DATE { get; set; }
        public string USER_ACCOUNT_ID { get; set; }
        public string GAME_ACCOUNT_ID { get; set; }
    
        public virtual TB_USER TB_USER { get; set; }
        public virtual TB_GAME_ACCOUNT TB_GAME_ACCOUNT { get; set; }
    }
}
