# Dokumentace
Vojtěch Gadurek 2023
# Úvod
Toto je prototyp celotáborové hry na těžení ropy.
Projekt je rozdělen na dvě části první, tato se věnuje implementaci uživatelského rozhraní a druhá [https://github.com/vojtechgadurek/CorpGameLibrary] je knihovnou.
Následujicí rozdělení jsem zvolil, aby se nemíchala logika hry a uživatelské rozhraní. Hra je myšlena jako táborová, tedy předpokládá malý počet hráčů, kteří nemají v úmyslu jí rozbít, dále hesla jsou ukládána v plaintextu, uživatelé by měli být o tomto informováni. (PASSWORDS ARE STORED IN PLAINTEXT)
# Popis
## Služby
### EventAgregator
Poskytuje umožnuje komponentám se přihlásit k odběru změn a případně provést nějakou akci při jejich publikaci.
Dále umožnuje komponentám publikovat změnu.
```cs
@inject EventAgregator<Foo?> EventAgregator 

@code{
//Subcribes to the event agregator
protected override void OnInitialized()
    {
        EventAgregator.Subscribe(this,(Foo? foo) => { StateHasChanged(); }); 
    }
//Publishes eventual change, if this method is called
void Publish(Foo foo){EventAgregator.Publish(foo)
}
˙``
### UserStateMaintainer
Udržuje stav přihlášení hráče, ukládá posledního přidaného hráče do prohlížeče skrze protected storage. V případě odhlášení dané uživatelské jméno smaže.
#### PlayerBrowserData
Reprezentuje data uložená v prohlížeči nyní jenom uživatelské jméno.
### UserState
Je to více méně, EventAgregator, který umožňuje propagovat změnu přes všechna sessions.
Například Spotmarket toho využívý pro propagovaní změny při přidání nové SpotMarketTradeOffer.

# Do budoucna
- Rád doplnil nějaký logging systém, případně upravil navratové hodnoty, tak aby šlo lépe rozpoznat, proč daná funkce nefungovala z binárního ano/ne. 
- V budoucnu bych rád upravil zamykání surovin/blokovaní kapacity, tak aby více omezovalo, kdo ji může provést - čátečně hotovo
- Dále bych rád opustil třidu GameController, která velmi omezuje výhody, které plynou z Blazoru a services, (a je otázkou zda vyhody, které přináší za to stojí) a vychází z návrhu, který více odstinoval webovou část od samotné hry
- Dále bych rád přepsal Resources, tak aby byly více staticky kontrolovatelné při kompilaci - done :D

# Uživatelská příručka
Do hry je prvně potřeba se zaregistrovat a zvolit unikátní přezdívku a heslo.
Pak je možné si půjčit více pěnez
Dále je možné v záložce spotmarket vidět nabízené obchody
V záložce oilfields je možné si koupit nějaké nové ropné pole a na něj umístit těžbní jednotku (rig)
