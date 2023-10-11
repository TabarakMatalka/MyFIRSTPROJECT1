using System;
using System.Collections.Generic;

namespace firstProjectTest.Models;

public partial class Subscription
{
    public decimal Id { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Status { get; set; }

    public string? Organization { get; set; }

    public decimal? UserId { get; set; }

    public decimal? PaymentId { get; set; }

    public virtual ICollection<Beneficiary> Beneficiaries { get; set; } = new List<Beneficiary>();

    public virtual Payment? Payment { get; set; }

    public virtual User? User { get; set; }
}
