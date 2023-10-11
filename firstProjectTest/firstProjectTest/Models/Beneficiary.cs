using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace firstProjectTest.Models;

public partial class Beneficiary
{
    public decimal Id { get; set; }

    public string? Name { get; set; }

    public string? Relationship { get; set; }

    public decimal? Age { get; set; }

    public string? Status { get; set; }

    [NotMapped]
    public virtual IFormFile? ImageFile { get; set; }
    public string? ProofImage { get; set; }

    public decimal? SubscriptionId { get; set; }

    public virtual Subscription? Subscription { get; set; }
}
