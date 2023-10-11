using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace firstProjectTest.Models;

public partial class Testimonialpage
{
    public decimal PageId { get; set; }

    public string? Name { get; set; }

    public string? Status { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }


    [NotMapped]
    public virtual IFormFile? ImageFile { get; set; }
    public string? Image { get; set; }

    public string? Reviewmessage { get; set; }

    public decimal? UserId { get; set; }

    public decimal? HomepageId { get; set; }

    public virtual Homepage? Homepage { get; set; }

    public virtual User? User { get; set; }
}
