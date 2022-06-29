#!/bin/sh

./docx -vars vector "Table" "sample.csv" , \
             zimage "Logo" "Logo.jpg" , \
             scalar "ReportDate" "12-22-2222" , \
             scalar "Copyright" "SYNRC" , \
             base64 'CompanyName' "0JTQviDQvtC/0YDQsNGG0Y7QstCw0L3QvdGPINC00L7RgNGD0YfQtdC90L3RjyDQnNGW0L3RltGB0YLRgNCwcXdlcXdl" \
      -in "in.docx" \
      -out "out.docx"
