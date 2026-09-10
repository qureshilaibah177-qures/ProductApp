using System;
using System.Collections.Generic;

namespace ProductApp.Models;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public int? CustomerId { get; set; }

    public DateTime? InvoiceDate { get; set; }

    public decimal TotalAmount { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
}
