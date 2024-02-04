using System;
using System.Collections.Generic;

namespace AppSync.Web.Models.Entity_Tables
{
    public partial class MenuItem
    {
        public MenuItem()
        {
            InverseParentMenuItem = new HashSet<MenuItem>();
        }

        public int MenuItemId { get; set; }
        public int? ParentMenuItemId { get; set; }
        public string Name { get; set; } = null!;
        public string? Url { get; set; }
        public int? Position { get; set; }
        public int? Level { get; set; }
        public bool? IsActive { get; set; }

        public virtual MenuItem? ParentMenuItem { get; set; }
        public virtual ICollection<MenuItem> InverseParentMenuItem { get; set; }
    }
}
