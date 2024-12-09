﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementData.Entities
{
    public class Invoice : BaseEntity
    {
        public int InvoiceId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
