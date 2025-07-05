# 5 Amazing Libraries (.NET 9 Demo)

This project demonstrates the use of five powerful .NET libraries in a single .NET 9 console application. It showcases PDF generation, fake data creation, Excel import/export, benchmarking, and resilience patterns.

## Features

- **QuestPDF**: Generate professional PDF quotations.
- **Bogus**: Create realistic fake customer, invoice, and payment data.
- **ClosedXML**: Export and import data to/from Excel files.
- **BenchmarkDotNet**: Benchmark code performance.
- **Polly**: Implement retry, circuit breaker, and policy wrapping for resilience.

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Visual Studio 2022 or later

### Installation

1. Clone the repository.
2. Restore NuGet packages.
3. Build the solution



### Usage

Run the application:


The application will:

- Generate a quotation PDF (`Quotation.pdf`) using QuestPDF.
- Create fake customer, invoice, and payment data with Bogus.
- Export and import this data to/from an Excel file (`Data.xlsx`) using ClosedXML.
- Run performance benchmarks with BenchmarkDotNet (ensure Release mode).
- Demonstrate Polly's retry, circuit breaker, and policy wrap patterns.

### Output

- `Quotation.pdf`: Example PDF quotation.
- `Data.xlsx`: Excel file with customers, invoices, and payments.

## Libraries Used

- [QuestPDF](https://www.questpdf.com/)
- [Bogus](https://github.com/bchavez/Bogus)
- [ClosedXML](https://github.com/ClosedXML/ClosedXML)
- [BenchmarkDotNet](https://benchmarkdotnet.org/)
- [Polly](https://github.com/App-vNext/Polly)

## Project Structure

- `Program.cs`: Main entry point, orchestrates all features.
- `QuotationDocument.cs`: PDF generation logic.
- `FakeDataGenerator.cs`: Fake data creation.
- `ExcelService.cs`: Excel import/export logic.
- `BenchmarkTests.cs`: Performance benchmarks.
- `PollyService.cs`: Resilience patterns.

## License

This project is for educational/demo purposes.
