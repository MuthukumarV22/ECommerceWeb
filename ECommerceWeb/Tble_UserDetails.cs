//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ECommerceWeb
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tble_UserDetails
    {
        public int DetailsId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> Mobile { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public Nullable<int> Pincode { get; set; }
    
        public virtual Tble_User Tble_User { get; set; }
    }
}
