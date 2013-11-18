start AlkalmazasSzerver\bin\Debug\AlkalmazasSzerver.exe
TIMEOUT /T 1
FOR /L %%A IN (0,1,3) DO START Kliens\bin\Debug\iqchampion_design.exe %%A Play

PAUSE

TASKKILL /F /IM iqchampion_design.exe
TASKKILL /F /IM AlkalmazasSzerver.exe
