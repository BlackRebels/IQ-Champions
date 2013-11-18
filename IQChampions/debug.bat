start AlkalmazasSzerver\bin\Debug\AlkalmazasSzerver.exe
FOR %%A IN (1 2 3 4) DO START Kliens\bin\Debug\iqchampion_design.exe

PAUSE

TASKKILL /F /IM iqchampion_design.exe
TASKKILL /F /IM AlkalmazasSzerver.exe
