using System;
using System.Collections.Generic;

namespace firstProjectTest.Models;

public partial class Contactuspage
{
    public decimal PageId { get; set; }

    public string? Message { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public decimal? HomepageId { get; set; }

    public virtual Homepage? Homepage { get; set; }
}
