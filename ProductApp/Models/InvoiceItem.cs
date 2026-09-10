using System;
using System.Collections.Generic;

namespace ProductApp.Models;

public partial class InvoiceItem
{
    public int InvoiceItemId { get; set; }

    public int? InvoiceId { get; set; }

    public int? ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public virtual Invoice? Invoice { get; set; }

    public virtual Product? Product { get; set; }
}
