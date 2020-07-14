#!/bin/sh

rm -rf bin
rm -rf obj
dotnet build docx.fsproj
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
