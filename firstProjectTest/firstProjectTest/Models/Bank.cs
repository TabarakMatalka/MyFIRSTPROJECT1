using System;
using System.Collections.Generic;

namespace firstProjectTest.Models;

public partial class Bank
{
    public decimal Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Cardholder { get; set; }

    public string? Cardnumber { get; set; }

    public decimal? Balance { get; set; }

    public string? Cvv { get; set; }

    public decimal? PaymentId { get; set; }

    public virtual Payment? Payment { get; set; }
}
