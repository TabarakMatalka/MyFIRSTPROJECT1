using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace firstProjectTest.Models;

public partial class User
{
    public decimal Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    [NotMapped]
    public virtual IFormFile? ImageFile { get; set; }
    public string? ProfileImage { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public decimal? RoleId { get; set; }

    public virtual Role? Role { get; set; }

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public virtual ICollection<Testimonialpage> Testimonialpages { get; set; } = new List<Testimonialpage>();
}
