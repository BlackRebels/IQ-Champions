cd AlkalmazasSzerver\bin\Debug
start AlkalmazasSzerver.exe
TIMEOUT /T 1
cd ..\..\..\Kliens\bin\Debug
FOR %%A IN ("Egyeske" "Ketteske" "Harmaska"   "Negyeske") DO (
rem "a" "b" "c" "d"	
	START iqchampion_design.exe %%A Play	
)

PAUSE

TASKKILL /F /IM iqchampion_design.exe
TASKKILL /F /IM AlkalmazasSzerver.exe
