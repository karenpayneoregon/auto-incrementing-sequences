﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace IncrementSequenceDemos.Models;

public partial class Customers
{
    public int CustomerIdentifier { get; set; }

    public string CompanyName { get; set; }

    public string ContactName { get; set; }

    public string ContactTitle { get; set; }

    public string City { get; set; }

    public string PostalCode { get; set; }

    public string Country { get; set; }

    public virtual ICollection<CustomerSequence> CustomerSequence { get; } = new List<CustomerSequence>();

    public virtual ICollection<Orders> Orders { get; } = new List<Orders>();
}