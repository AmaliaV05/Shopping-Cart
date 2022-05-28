# Shopping-Cart console-app with unit tests

![GitHub top language](https://img.shields.io/github/languages/top/AmaliaV05/Shopping-Cart)

Shopping-Cart is a .NET 5.0 console application which computes the totals for a shopping cart of products shipped from different countries, 
applies various promotions and displays a detailed invoice in USD or discards the cart. The functionalities are tested using [NUnit framework](https://nunit.org/).

## Prerequisites
To run the application you need:
- [Visual Studio 2019](https://visualstudio.microsoft.com/vs/older-downloads/#visual-studio-2019-and-other-products) with the .NET Desktop Development workload
- [.Net Core SDK](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)
- [NUnit package installed](https://docs.microsoft.com/en-us/nuget/quickstart/install-and-use-a-package-in-visual-studio) using the NuGet Package Manager, the Package Manager Console, or the dotnet CLI
- [A Microsoft account](https://support.microsoft.com/en-us/account-billing/how-to-create-a-new-microsoft-account-a84675c3-3e9e-17cf-2911-3d56b15c0aaf)

## Features
The Shopping-Cart console-app can:
- Display the catalog of products
- Create a new shopping cart by adding a product
- Add other products to the shopping cart
- Create an invoice for the shopping cart containing the subtotal, shipping fees, VAT value, value of each discount and the total of the invoice
