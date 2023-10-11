using System;
using System.Collections.Generic;

namespace firstProjectTest.Models;

public partial class Role
{
    public decimal Id { get; set; }

    public string? RoleName { get; set; }

    public string? RoleDescription { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
