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
    
    public partial class TB_MONEY
    {
        public string USER_ACCOUNT_ID { get; set; }
        public Nullable<int> TOTAL_MONEY { get; set; }
        public Nullable<int> NEWBIE { get; set; }
        public Nullable<int> TIME_FOR_FREE { get; set; }
    
        public virtual TB_USER TB_USER { get; set; }
    }
}
