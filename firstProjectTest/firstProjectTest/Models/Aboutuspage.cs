using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace firstProjectTest.Models;

public partial class Aboutuspage
{
    public decimal PageId { get; set; }

    public string? Title { get; set; }

    public string? Paragraph { get; set; }

    public string? Location { get; set; }


    [NotMapped]
    public virtual IFormFile? ImageFile { get; set; }
    public string? Image { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public decimal? HomepageId { get; set; }

    public virtual Homepage? Homepage { get; set; }
}
