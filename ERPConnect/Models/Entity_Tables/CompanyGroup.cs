using System;
using System.Collections.Generic;

namespace ERPConnect.Web.Models.Entity_Tables
{
    public partial class CompanyGroup
    {
        public int Id { get; set; }
        public string? GroupName { get; set; }
        public bool? IsActive { get; set; }
    }
}
