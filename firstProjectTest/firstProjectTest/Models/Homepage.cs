using System;
using System.Collections.Generic;

namespace firstProjectTest.Models;

public partial class Homepage
{
    public decimal PageId { get; set; }

    public string? Title { get; set; }

    public string? Paragraph { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<Aboutuspage> Aboutuspages { get; set; } = new List<Aboutuspage>();

    public virtual ICollection<Contactuspage> Contactuspages { get; set; } = new List<Contactuspage>();

    public virtual ICollection<Testimonialpage> Testimonialpages { get; set; } = new List<Testimonialpage>();
}
