// See https://aka.ms/new-console-template for more information
using _5_Amazing_Libraries;
using BenchmarkDotNet.Running;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;



Console.WriteLine("Hello, World!");

//#1 QuestPDF

Console.WriteLine("Generating Quotation PDF using QuestPDF...");
QuestPDF.Settings.License = LicenseType.Community;
var document = new QuotationDocument();
document.GeneratePdf("Quotation.pdf");

//#2 Bogus 

Console.WriteLine("Generating Fake Data of customers using Bogus...");
var generator = new FakeDataGenerator();

var customers = generator.GenerateCustomers(10);
generator.GenerateInvoices(customers);
generator.GeneratePayments(customers);

// Display Output
foreach (var customer in customers)
{
    Console.WriteLine($"\nCustomer: {customer.FullName} ({customer.Email})");
    Console.WriteLine($"Address: {customer.Address}");
    foreach (var invoice in customer.Invoices)
    {
        Console.WriteLine($"\n  Invoice #{invoice.Id} - {invoice.InvoiceDate.ToShortDateString()} - ₹{invoice.Amount}");
        foreach (var payment in invoice.Payments)
        {
            Console.WriteLine($"    Payment #{payment.Id} - {payment.PaymentDate.ToShortDateString()} - ₹{payment.PaidAmount}");
        }
    }
}


//#3 ClosedXML
Console.WriteLine("Export/Import using ClosedXML...");
var excel = new ExcelService();

// 1. Generate fake data
excel.GenerateFakeData(out var cust, out var invoices, out var payments);

// 2. Export to Excel
var filePath = "Data.xlsx";
excel.ExportToExcel(filePath, cust, invoices, payments);

// 3. Import it back
excel.ImportFromExcel(filePath, out var c2, out var i2, out var p2);

Console.WriteLine($"\nLoaded {c2.Count} customers, {i2.Count} invoices, {p2.Count} payments");

// 4. BenchmarkDotNet

Console.WriteLine("BenchmarkDotNet runing test (it requires RELEASE mode to run correctly...");

BenchmarkRunner.Run<BenchmarkTests>();

// 5. Polly
Console.WriteLine("Polly...");

var polly = new PollyService();

polly.RetryPolicyExample();
polly.CircuitBreakerExample();
polly.WrapPoliciesExample();

Console.WriteLine("Press any key to exit...");
Console.ReadLine();
