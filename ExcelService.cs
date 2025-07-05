using ClosedXML.Excel;


namespace _5_Amazing_Libraries
{
    public class ExcelService
    {
        public void ExportToExcel(string filePath,
            List<Customer> customers,
            List<Invoice> invoices,
            List<Payment> payments)
        {
            using var workbook = new XLWorkbook();

            // Customers
            var customerSheet = workbook.Worksheets.Add("Customers");
            customerSheet.Cell(1, 1).InsertTable(customers);

            // Invoices
            var invoiceSheet = workbook.Worksheets.Add("Invoices");
            invoiceSheet.Cell(1, 1).InsertTable(invoices);

            // Payments
            var paymentSheet = workbook.Worksheets.Add("Payments");
            paymentSheet.Cell(1, 1).InsertTable(payments);

            workbook.SaveAs(filePath);
            Console.WriteLine($"Exported to: {filePath}");
        }

        public void ImportFromExcel(string filePath,
            out List<Customer> customers,
            out List<Invoice> invoices,
            out List<Payment> payments)
        {
            customers = new();
            invoices = new();
            payments = new();

            using var workbook = new XLWorkbook(filePath);

            // Customers
            var customerSheet = workbook.Worksheet("Customers");
            foreach (var row in customerSheet.RangeUsed().RowsUsed().Skip(1))
            {
                customers.Add(new Customer
                {
                    Id = int.Parse(row.Cell(1).Value.ToString()),
                    FullName = row.Cell(2).GetString(),
                    Email = row.Cell(3).GetString(),
                    Address = row.Cell(4).GetString()
                });
            }

            // Invoices
            var invoiceSheet = workbook.Worksheet("Invoices");
            foreach (var row in invoiceSheet.RangeUsed().RowsUsed().Skip(1))
            {
                invoices.Add(new Invoice
                {
                    Id = int.Parse(row.Cell(1).Value.ToString()),
                    CustomerId = int.Parse(row.Cell(2).Value.ToString()),
                    InvoiceDate = DateTime.Parse(row.Cell(3).Value.ToString()),
                    Amount = decimal.Parse(row.Cell(4).Value.ToString())
                });
            }

            // Payments
            var paymentSheet = workbook.Worksheet("Payments");
            foreach (var row in paymentSheet.RangeUsed().RowsUsed().Skip(1))
            {
                payments.Add(new Payment
                {
                    Id = int.Parse(row.Cell(1).Value.ToString()),
                    InvoiceId = int.Parse(row.Cell(2).Value.ToString()),
                    PaymentDate = DateTime.Parse(row.Cell(3).Value.ToString()),
                    PaidAmount = decimal.Parse(row.Cell(4).Value.ToString())
                });
            }

            Console.WriteLine($"Imported from: {filePath}");
        }

        public void GenerateFakeData(
            out List<Customer> customers,
            out List<Invoice> invoices,
            out List<Payment> payments)
        {
            var generator = new FakeDataGenerator(); // Reuse from previous step

            customers = generator.GenerateCustomers(5);
            generator.GenerateInvoices(customers);
            generator.GeneratePayments(customers);

            invoices = customers.SelectMany(c => c.Invoices).ToList();
            payments = invoices.SelectMany(i => i.Payments).ToList();
        }
    }
}
    



