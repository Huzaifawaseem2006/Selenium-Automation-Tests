@ECHO OFF
ECHO Demo Automation Executed Started.

set testcategory=Login
set dllpath=D:\FSQA_01\FSQA_01\bin\Debug\FSQA_01.dll
set trxerpath=D:\FSQA_01\FSQA_01\bin\Debug\
set testsummaryreportPath=D:\FSQA_01\Report\

FOR /f %%a IN ('WMIC OS GET LocalDateTime ^| FIND "."') DO SET DTS=%%a
SET filename=%testcategory%_%DTS:~0,4%%DTS:~4,2%%DTS:~6,2%%DTS:~8,2%%DTS:~10,2%%DTS:~12,2%
echo %filename%

call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"
cd %testsummaryreportPath%


VSTest.Console.exe  %dllpath% /TestCaseFilter:"TestCategory=%testcategory%" /Logger:"trx;LogFileName=%testsummaryreportPath%\%filename%\%filename%.trx"

cd %trxerpath%
TrxToHTML.exe %testsummaryreportPath%%filename%\

PAUSE