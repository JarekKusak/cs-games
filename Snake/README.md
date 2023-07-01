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
Hlavní řídící třída, ve které probíhá veškerá logika a řízení hry. 
