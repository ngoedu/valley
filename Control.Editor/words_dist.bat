set WORD_PATH=%~dp0\Words
set DIST_PATH=%~dp0bin\Debug
set DIST_PATH2=..\XCodeRec\bin\Debug
set DIST_PATH3=..\App.TestForms\bin\Debug
set DIST_PATH4=D:\NGO\client\conf\editor

copy %WORD_PATH%\*.xml %DIST_PATH%\ /Y
copy %WORD_PATH%\*.xml %DIST_PATH2%\ /Y
copy %WORD_PATH%\*.xml %DIST_PATH3%\ /Y
copy %WORD_PATH%\*.xml %DIST_PATH4%\ /Y