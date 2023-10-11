using System;
using System.Collections.Generic;

namespace firstProjectTest.Models;

public partial class Payment
{
    public decimal Id { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string? PaymentMethod { get; set; }

    public virtual ICollection<Bank> Banks { get; set; } = new List<Bank>();

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}
