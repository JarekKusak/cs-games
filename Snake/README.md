# ZÁPOČTOVÝ PROGRAM - SNAKE
## O hře
Tahle dokumentace popisuje počítačovou hru jménem Snake, ve které hráč ovládá pohyby hada na čtvercové ploše a sbírá jablíčka. Koncept hry sahá až do 70. let 20. století, přičemž hru zpopularizovala v 90. letech firma Nokia, která hru zahrnula mezi předinstalované hry na svých mobilních telefonech.
## Pravidla a průběh hry
Princip hry spočívá v nasbírání co nejvíce jablíček (a tedy co nejvyššího skóre) a při tom nenarazit do svého vlastního ocasu (nebo do volitelných překážek). Had může procházet skrze zdi (prochází z jedné strany na druhou).
## Nastavení a ovládání
Při prvním spuštění hry si hráč zakládá účet, ke kterému se ukládá nejvyšší dosažené skóre. Hráč si může navolit jméno účtu a také "skin" svého hada, může si vybrat, jakým znakem se bude vypisovat hlava a tělo, a taky jakou barvou. Je samozřejmě možné založit nový účet a účty průběžně měnit. Samotný pohyb hada se ovládá tlačítkami W (nahoru), S (dolů), A (doleva) a D (doprava). 
## Spuštění hry
Jak již bylo řečeno, při úplně prvním spuštění si hráč založí účet, vybere jméno a podobu hada. Poté se zobrazí startovací menu. Na výběr je hrát s aktuálním účtem, nebo účet změnit či vytvořit nový. Po výběru první volby (tedy Hrát) se zobrazí hrací menu, kde si hráč navoluje obtížnost (rychlost hada), kde si může vybrat až ze čtyř obtížností, a navolení překážek (pouze ano, nebo ne). Poté se pouze vykreslí hrací plocha s hadem a jablkem, a čeká se na hráčův vstup. Jakmile ho zadá, začne se hrát, dokud had nenarazí sám do sebe nebo do překážek. Po konci hry se program hráče zeptá, zda chce zkusit další kolo. Pokud ano, opět naskočí hrací menu, pokud ne, vrátí se rovnou do startovacího menu. Při navolení druhé volby ve startovacím menu (tedy Změnit či vytvořit účet) se zobrazí "player menu", kde má hráč na výběr mězi vytvořením účtu či přepnutí na nějaký z již vytvořených účtů.
## O programu (programátorský pohled)
Program byl napsán v jazyce C# v knihovnách .NET Framework, čili je spustitelný pouze na operačních systémech Windows. Jedná se ryze o konzolovou aplikaci.
## Programová dekompozice
Program je rozdělený na 6 tříd (+ třída Program.cs, zahajovací třída):
### Game.cs
Hlavní řídící třída, ve které probíhá veškerá logika a řízení hry. Stará se o výpis jednotlivých "menu", zakládání účtu hráče, změnu hráče, ale také o zahájení hry (čili zakládání nových objektů) a o její průběh.
### Filemanager.cs
Třída starající se o ukládání informací o hráčích (účtů) do souboru (který se standardně ukládá v adresáři %appdata%) a následné načítání dat z něj. Také uchovává list hráčů typu Player.cs
### Player.cs
Tato třída slouží k uchovávání informací o hráči, jako jsou jeho jméno, vzhled hlavy a těla hada, barvy a maximální dosažené skóre. Tyto informace mohou být dále využity v různých částech programu, který implementuje hru hada.
### Snake.cs
Tato třída reprezentuje hada. Třída obsahuje logiku pro pohyb hada, správu jeho těla, kontrolu kolizí s překážkami a sebou samým, a také výstup na obrazovku.
### Apple.cs
Tato třída reprezentuje jablko. Třída obsahuje logiku pro generování a správu jablka, kontrolu, zda bylo jablko snědeno, a výstup na obrazovku.
### Table.cs
Tato třída reprezentuje herní pole (tabulku). Třída obsahuje logiku pro vytvoření tabulky, včetně okrajů, případných překážek a výstupu na obrazovku.
## Nedostatky v programu (co je třeba dodělat či předělat)
Na vypisování jsem se rozhodl používat Console.SetCursorPosition, tedy neustálé nastavování souřadnic kurzoru uvnitř konzole. Tenhle způsob sice zajistil plynulejší vypisování (místo neustálého problikávání), ale za cenu přehlednosti. Co jsem také vyřešil špatně je nejednotné místo vypisování, každá třída si vypisování konkrétní položky řeší sama. Do programu by toho šlo přidat více, třeba více map (herních polí), mít možnost zabránit přecházení hada přes hranice pro zvýšení obtížnosti (tedy při srážce hada se zdí by nastal konec hry). Taky by bylo dobré mít možnost kompletně odstranit účet nebo si navolit vlastní klávesy pro ovládání.
## Závěr
Celková hra je principem jednoduchá, je to ale skvělý nástroj na zabíjení času při zdlouhavé a nudné jízdě městskou hromadnou dopravou, a také v ní existuje prostor na spoustu kreativních nápadů jak hru ozvláštnit a udělat ji ještě zábavnější.