start AlkalmazasSzerver\bin\Debug\AlkalmazasSzerver.exe
REM FOR /L %%A IN (0,1,3) DO START Kliens\bin\Debug\iqchampion_design.exe %%A Play
FOR %%A IN ("Egyeske" "Ketteske" "Harmaska" "Negyeske") DO (
	TIMEOUT /T 1
	START Kliens\bin\Debug\iqchampion_design.exe %%A Play	
)
rem 
PAUSE

TASKKILL /F /IM iqchampion_design.exe
TASKKILL /F /IM AlkalmazasSzerver.exe
