using System.Collections.Generic;
using System.Globalization;
using FutterlisteNg.Data.Model;

namespace FutterlisteNg.UnitTests.Builder
{
    public class PaymentBuilder
    {
        private string _payedBy;
        private string _spentOn;
        private List<PaymentLine> _paymentLines = new List<PaymentLine>();

        public PaymentBuilder WithPayedBy(string payedBy)
        {
            _payedBy = payedBy;
            return this;
        }

        public PaymentBuilder WithSpentOn(string spentOn)
        {
            _spentOn = spentOn;
            return this;
        }

        public PaymentBuilder WithPaymentLine(string payedFor, decimal amount)
        {
            _paymentLines.Add(new PaymentLine(payedFor, amount));
            return this;
        }
        
        public Payment Build()
        {
            var result = new Payment(_payedBy, _spentOn);
            result.PaymentLines.AddRange(_paymentLines);
            return result;
        }
    }
}