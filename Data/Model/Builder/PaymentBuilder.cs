using System;
using System.Collections.Generic;

namespace FutterlisteNg.Data.Model.Builder
{
    public class PaymentBuilder
    {
        public static PaymentBuilder Valid => new PaymentBuilder("Eric")
            .WithDescription("McDonald's 27.09.2019")
            .WithPaymentLine("Stan", 5)
            .WithPaymentLine("Eric", 5);

        private string _payedBy;
        private Guid _id;
        private string _description;
        private List<PaymentLine> _lines;

        public PaymentBuilder(string payedBy)
        {
            _payedBy = payedBy;
            _lines = new List<PaymentLine>();
        }

        public PaymentBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public PaymentBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public PaymentBuilder WithPaymentLine(string payedFor, decimal amount)
        {
            _lines.Add(new PaymentLine(payedFor, amount));
            return this;
        }

        public Payment Build()
        {
            var payment = new Payment(_payedBy, _description);
            payment.Id = _id;
            payment.PaymentLines.AddRange(_lines);
            return payment;
        }
    }
}