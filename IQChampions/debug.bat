start AlkalmazasSzerver\bin\Debug\AlkalmazasSzerver.exe
FOR %%A IN ("Egyeske" "Ketteske" "Harmaska" "Negyeske" "a" "b" "c" "d") DO (
rem 
	TIMEOUT /T 1
	START Kliens\bin\Debug\iqchampion_design.exe %%A Play	
)

PAUSE

TASKKILL /F /IM iqchampion_design.exe
TASKKILL /F /IM AlkalmazasSzerver.exe
