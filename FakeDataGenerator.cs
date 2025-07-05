using Bogus;

namespace _5_Amazing_Libraries
{
    

    public class FakeDataGenerator
    {
        private int _customerId = 1;
        private int _invoiceId = 1;
        private int _paymentId = 1;

        public List<Customer> GenerateCustomers(int count)
        {
            var customerFaker = new Faker<Customer>()
                .RuleFor(c => c.Id, f => _customerId++)
                .RuleFor(c => c.FullName, f => f.Name.FullName())
                .RuleFor(c => c.Email, f => f.Internet.Email())
                .RuleFor(c => c.Address, f => f.Address.FullAddress());

            return customerFaker.Generate(count);
        }

        public void GenerateInvoices(List<Customer> customers, int minInvoices = 1, int maxInvoices = 3)
        {
            var invoiceFaker = new Faker<Invoice>()
                .RuleFor(i => i.Id, f => _invoiceId++)
                .RuleFor(i => i.InvoiceDate, f => f.Date.Past(1))
                .RuleFor(i => i.Amount, f => Math.Round(f.Random.Decimal(1000, 10000), 2));

            foreach (var customer in customers)
            {
                var invoices = invoiceFaker.GenerateBetween(minInvoices, maxInvoices);
                foreach (var invoice in invoices)
                {
                    invoice.CustomerId = customer.Id;
                }
                customer.Invoices.AddRange(invoices);
            }
        }

        public void GeneratePayments(List<Customer> customers, int minPayments = 0, int maxPayments = 3)
        {
            var paymentFaker = new Faker<Payment>()
                .RuleFor(p => p.Id, f => _paymentId++)
                .RuleFor(p => p.PaymentDate, f => f.Date.Recent(60))
                .RuleFor(p => p.PaidAmount, f => Math.Round(f.Random.Decimal(500, 5000), 2));

            foreach (var customer in customers)
            {
                foreach (var invoice in customer.Invoices)
                {
                    var payments = paymentFaker.GenerateBetween(minPayments, maxPayments);
                    foreach (var payment in payments)
                    {
                        payment.InvoiceId = invoice.Id;
                    }
                    invoice.Payments.AddRange(payments);
                }
            }
        }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Address { get; set; } = "";
        public List<Invoice> Invoices { get; set; } = new();
    }

    public class Invoice
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal Amount { get; set; }
        public List<Payment> Payments { get; set; } = new();
    }

    public class Payment
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaidAmount { get; set; }
    }


}
