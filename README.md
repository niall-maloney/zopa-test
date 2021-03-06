# Zopa Technical Test

[![Build status](https://ci.appveyor.com/api/projects/status/9p9p8lu9i4q4kijg?svg=true)](https://ci.appveyor.com/project/niall-maloney/zopa-test)
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/250d22aeeaec49678635e05055f9d834)](https://www.codacy.com/app/niall-maloney/zopa-test?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=niall-maloney/zopa-test&amp;utm_campaign=Badge_Grade)

There is a need for a rate calculation system allowing prospective borrowers to obtain a quote from our pool of lenders for 36 month loans. This system will take the form of a command-line application.

You will be provided with a file containing a list of all the offers being made by the lenders within the system in CSV format, see the example market.csv file provided alongside this specification.

You should strive to provide as low a rate to the borrower as is possible to ensure that Zopa's quotes are as competitive as they can be against our
competitors'. You should also provide the borrower with the details of the monthly repayment amount and the total repayment amount.

Repayment amounts should be displayed to 2 decimal places and the rate of the loan should be displayed to one decimal place.

Borrowers should be able to request a loan of any £100 increment between £1000 and £15000 inclusive. If the market does not have sufficient offers from lenders to satisfy the loan then the system should inform the borrower that it is not possible to provide a quote at that time.

**The application should take arguments in the form:**

```
cmd> [application] [market_file] [loan_amount]
```

**Example:**

```
cmd> quote.exe market.csv 1500
```

**The application should produce output in the form:**

```
cmd> [application] [market_file] [loan_amount]
Requested amount: £XXXX
Rate: X.X%
Monthly repayment: £XXXX.XX
Total repayment: £XXXX.XX
```

**Example:**

```
cmd> quote.exe market.csv 1000
Requested amount: £1000
Rate: 7.0%
Monthly repayment: £30.78
Total repayment: £1108.10
```

## Build

The application requires .Net Core 2.0 SDK in order to be built, and .Net Core 2.0 Runtime in order to run. [^1] The build targets Windows x64 platform.[^2]

`build.bat` will output build to `.\build-output\win-x64`.

`run-tests.bat` will run the solutions unit tests.

[^1]: A self contained application can be built using `dotnet publish .\ZopaTest.App\ZopaTest.App.csproj -f netcoreapp2.0 -c Release -r win-x64 -o ..\build-output\win-x64\ZopaTest`

[^2]: More on .Net Core target platforms can be found at https://docs.microsoft.com/en-us/dotnet/core/rid-catalog
