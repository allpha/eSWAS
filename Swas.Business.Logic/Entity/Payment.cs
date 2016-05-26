﻿namespace Swas.Business.Logic.Entity
{
    using System;
    using System.Collections.Generic;

    public class PaymentInfoItem
    {
        public int ActId { get; set; }
        public int ActDate { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerInfo { get; set; }

        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal PayedAmount { get; set; }
        public decimal DebtAmount { get; set; }

        public List<PaymentHistoryItem> HistoryItemSource { get; set; }
    }

    public class PaymentHistoryItem
    {
        public DateTime PayDate { get; set; }
        public decimal Amount { get; set; }
    }

}
