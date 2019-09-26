using System.Collections.Generic;

namespace FutterlisteNg.Data.Model.Builder
{
    public class PaymentBuilder
    {
        private string _payedBy;
        private string _description;
        private List<PaymentLine> _lines;

        public PaymentBuilder(string payedBy)
        {
            _payedBy = payedBy;
            _lines = new List<PaymentLine>();
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
            payment.PaymentLines.AddRange(_lines);
            return payment;
        }
    }
}