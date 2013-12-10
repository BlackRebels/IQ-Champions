cd AlkalmazasSzerver\bin\Debug
start IQbackend.exe
TIMEOUT /T 1
cd ..\..\..\Kliens\bin\Debug
FOR %%A IN ("Egyeske" "Ketteske" "Harmaska" "Negyeske") DO (
rem "a" "b" "c" "d"	 
	START IQChampions.exe %%A Play	
	TIMEOUT /T 1
)

PAUSE

TASKKILL /F /IM IQbackend.exe
TASKKILL /F /IM IQChampions.exe
