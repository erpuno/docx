# DOCX Template Processor

## Motivation

The goal was to build simple command line DOCX template processor that can be used from other than .NET platforms.

## Install .NET Core 3.1

Download `https://dot.net/v1/dotnet-install.sh` and then

```
$ ./dotnet-install.sh -c Current
```

Put into your `~/.profile` the following

```
export DOTNET_ROOT=$HOME/.dotnet
export PATH=$PATH:$HOME/.dotnet
```

With test

```
$ dotnet --list-runtimes
Microsoft.AspNetCore.App 3.1.5 [/home/maxim/.dotnet/shared/Microsoft.AspNetCore.App]
Microsoft.NETCore.App 3.1.5 [/home/maxim/.dotnet/shared/Microsoft.NETCore.App]
```

## Compile and run

Parameters are passed as command line arguments. Table fields are passed as CSV files.

```
$ cat firestarter.sh
#!/bin/sh

rm -rf bin
rm -rf obj
dotnet build docx.sln
cp Logo.jpg in.docx sample.csv bin/Debug/netcoreapp3.1
cd bin/Debug/netcoreapp3.1
./docx -vars vector "Table" "sample.csv" , \
             zimage "Logo" "Logo.jpg" , \
             scalar "ReportDate" "12-22-2222" , \
             scalar "Copyright" "SYNRC" , \
             scalar 'CompanyName' "SYNRC" \
      -in "in.docx" \
      -out "out.docx"
ls -l *.docx
```

```
$ cat sample.csv
Year,Brand,Model,Desc,Price
1997,Ford,E350,"ac, abs, moon",3000.00
1999,Chevy,"Venture ""Extended Edition""",,4900.00
1996,Jeep,Grand Cherokee,"MUST SELL! air, moon roof, loaded",4799.00
```

During run you should get out.docx with substituted template parameters.

```
$ dotnet build docx.sln
$ ./firestarter
Microsoft (R) Build Engine version 16.6.0+5ff7b0c9e for .NET Core
Copyright (C) Microsoft Corporation. All rights reserved.

  Determining projects to restore...
  Restored /home/maxim/depot/synrc/fsharp/docx/docx.fsproj (in 767 ms).
  TemplateEngine.Docx -> /home/maxim/depot/synrc/fsharp/docx/bin/Debug/netcoreapp3.1/TemplateEngine.Docx.dll
  Successfully created package '/home/maxim/depot/synrc/fsharp/docx/bin/Debug/TemplateEngine.Docx.1.0.0.nupkg'.
  docx -> /home/maxim/depot/synrc/fsharp/docx/bin/Debug/netcoreapp3.1/docx.dll

Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:04.71
Headers: ["Year"; "Brand"; "Model"; "Desc"; "Price"]
Rows: [["1996"; "Jeep"; "Grand Cherokee"; "MUST SELL! air, moon roof, loaded";
  "4799.00"]; ["1999"; "Chevy"; "Venture "Extended Edition""; ""; "4900.00"];
 ["1997"; "Ford"; "E350"; "ac, abs, moon"; "3000.00"]]
Vars: [TemplateEngine.Docx.TableContent; TemplateEngine.Docx.ImageContent;
 TemplateEngine.Docx.FieldContent; TemplateEngine.Docx.FieldContent;
 TemplateEngine.Docx.FieldContent]
Template: " in.docx "
Output: " out.docx"
Save Changes
-rw-r--r-- 1 maxim maxim 32979 Jul 14 10:45 in.docx
-rw-r--r-- 1 maxim maxim 39474 Jul 14 10:45 out.docx
```
