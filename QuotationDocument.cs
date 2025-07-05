using QuestPDF.Drawing;
using QuestPDF.Elements;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.ComponentModel;



namespace _5_Amazing_Libraries
{
    /// <summary>
    /// This is an example of generating a professional Quotation PDF using the QuestPDF library in .NET 9.
    /// The PDF includes a styled header, company and customer details, a line items table with prices,
    /// and a total calculation. The design aims to provide a clean and printable quotation format.
    /// </summary>
    public class QuotationDocument : IDocument
    {
        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Size(PageSizes.A4);
                page.DefaultTextStyle(x => x.FontSize(12));
                page.Content().Element(ComposeContent);
            });
        }

        void ComposeContent(QuestPDF.Infrastructure.IContainer container)
        {
            container.Column(column =>
            {
                column.Spacing(20);

                column.Item().Element(ComposeHeader);
                column.Item().Element(ComposeCompanyCustomerDetails);
                column.Item().Element(ComposeQuotationTable);
                column.Item().AlignRight().Text("Thank you for your business!")
                      .Italic().FontSize(10).FontColor(Colors.Grey.Darken2);
            });
        }

        void ComposeHeader(QuestPDF.Infrastructure.IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeColumn().Column(col =>
                {
                    col.Item().Text("QUOTATION").FontSize(20).Bold().FontColor(Colors.Blue.Medium);
                    col.Item().Text("Date: " + DateTime.Now.ToShortDateString());
                    col.Item().Text("Quote No: Q-1023");
                });

                row.ConstantColumn(150).Height(60).Background(Colors.Grey.Lighten2).AlignCenter().AlignMiddle().Text("Your Logo").SemiBold();
            });
        }

        void ComposeCompanyCustomerDetails(QuestPDF.Infrastructure.IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeColumn().Column(col =>
                {
                    col.Item().Text("From:").Bold();
                    col.Item().Text("Tech Solutions Pvt. Ltd.\n123 Business Road\nAhmedabad, India\n+91-9999999999");
                });

                row.RelativeColumn().Column(col =>
                {
                    col.Item().Text("To:").Bold();
                    col.Item().Text("Client Company\n456 Client Lane\nMumbai, India\n+91-8888888888");
                });
            });
        }

        void ComposeQuotationTable(QuestPDF.Infrastructure.IContainer container)
        {
            string[] headers = { "Item Description", "Quantity", "Unit Price", "Amount" };
            var items = new[]
            {
            new { Description = "Web Design Services", Quantity = 1, Price = 25000 },
            new { Description = "API Development", Quantity = 2, Price = 15000 },
            new { Description = "Hosting (1 year)", Quantity = 1, Price = 5000 }
        };

            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(4);
                    columns.RelativeColumn(1);
                    columns.RelativeColumn(2);
                    columns.RelativeColumn(2);
                });

                // Header
                table.Header(header =>
                {
                    foreach (var title in headers)
                    {
                        header.Cell().Background(Colors.Blue.Lighten2).Padding(5).Text(title).Bold();
                    }
                });

                // Items
                foreach (var item in items)
                {
                    table.Cell().Padding(5).Text(item.Description);
                    table.Cell().Padding(5).Text(item.Quantity.ToString());
                    table.Cell().Padding(5).Text($"₹{item.Price:N0}");
                    table.Cell().Padding(5).Text($"₹{(item.Price * item.Quantity):N0}");
                }

                // Total
                var total = items.Sum(i => i.Price * i.Quantity);

                table.Cell().ColumnSpan(3).Padding(5).AlignRight().Text("Total:").Bold();
                table.Cell().Padding(5).Text($"₹{total:N0}").Bold().FontColor(Colors.Green.Darken2);
            });
        }
    }
}






