# Dokumentace
Vojtěch Gadurek 2023
# Úvod
Toto je prototyp celotáborové hry na těžení ropy.
Projekt je rozdělen na dvě části první, tato se věnuje implementaci uživatelského rozhraní a druhá [https://github.com/vojtechgadurek/CorpGameLibrary] je knihovnou.
Následujicí rozdělení jsem zvolil, aby se nemíchala logika hry a uživatelské rozhraní.
# Architektura 
Pro webovou část jsem použil Blazor Server, ostatní jsem napsal v normálním C#. Důvodem je moje malá znalost Javascriptu.
# Do budoucna
- Rád doplnil nějaký logging systém, případně upravil navratové hodnoty, tak aby šlo lépe rozpoznat, proč daná funkce nefungovala z binárního ano/ne. 
- V budoucnu bych rád upravil zamykání surovin/blokovaní kapacity, tak aby více omezovalo, kdo ji může provést
- Dále bych rád opustil třidu GameController, která velmi omezuje výhody, které plynou z Blazoru a services, (a je otázkou zda vyhody, které přináší za to stojí) a vychází z návrhu, který více odstinoval webovou část od samotné hry
- Dále bych rád přepsal Resources, tak aby byly více staticky kontrolovatelné při kompilaci - done :D

# Uživatelská příručka
Do hry je prvně potřeba se zaregistrovat a zvolit unikátní přezdívku a heslo.
Pak je možné si půjčit více pěnez
Dále je možné v záložce spotmarket vidět nabízené obchody
V záložce oilfields je možné si koupit nějaké nové ropné pole a na něj umístit těžbní jednotku (rig)
