# Dokumentace hry Svarta Jump

### Zadani
Cilem bylo vytvorit hru podobnou hre Doodle jump. Hra nese nazev Svarta Jump - Svarta je prosluly cesky mladik z mesta Horice, ktery se snazi skakat po blocich a ziskat
timto zpusobem co nejvice bodu. Hra tedy nema konec a neexistuji levely - cilem je jen ziskat co nejvice bodu.

Ve hre jsou dva typy objektu, se kterymi hrac interaguje - bloky a prisery.
 - Blok - obdelnik, na ktery svarta muze skocit a odrazit se od nej. Bloku je nekolik typu: 
   - existuji bezne bloky bez zadnych specialnich vlastnosti.
   - Dale jsou male zlute bloky, na ktere je zkratka jen tezsi se trefit hracem a odrazit se od nich.
   - Ve hre se pak nachazi dva typy trampolin:
     - Cerny blok je trampolina, ktera hrace vystreli vzhuru velikou silou. 
     - Slabsi verzi cerne trampoliny je trampolina seda, ktera se vyskytuje casteji a svarta diky ni muze vyskocit vyse nez pri odrazu od obycejneho bloku, nikoliv vsak tolik jako z cerne trampoliny.
 - Prisery - existuje jen jeden typ prisery - veliky zeleny ctverec s nepratelskym oblicejem. Pri jakemkoliv stretnuti s hrace s priserou dojde k umrti hrace.

Kolem prisery se nachazi vice bloku, aby Svarta mel vyssi moznost se pres priseru dostat.

Dale pro zajimavost byly zavedeny mody hry, ktere se v puvodni hre Doodle Jump vubec nevyskytuji. Existuji 3 mody hry:
 - Normal mode - vsechny zminene bloky se objevuji a prisery take, pozadi je ruzove. Bloky maji urcitou rychlost, se kterou se pohybuji, tato rychlost je determinovana na zaklade aktualniho skore.
 - Fast mode - pozadi se zmeni na cernou a vsechny bloky maji vyssi rychlost oproti Normal mode.
 - Little mode - pozadi je bile, bloky maji stejnou rychlost jako v Normal mode, ale jsou male.
 
Hra automaticky uklada 10 nejvyse dosazenych bodu.

## Uzivatelska cast
Pri spusteni hry jsou vyobrazeny nejvyssi dosahnuta skore a tlacitko Hraj. Po kliknuti na tlacitko se spusti hra.

Hrac se pohybuje sipkami doprava a doleva. Snahou je ziskat co nejvice bodu, ktere se vypocitava na zaklade urazene vzdalenosti smerem vzhuru.
Prohra nastava v okamziku, kdy hrac spadne dolu mimo obrazovku nebo kdyz se dotkne prisery.

Skore se zobrazuje v pravem dolnim rohu, v levem hornim rohu je pak tlacitko na stopnuti hry.

Pri prohre je zobrazeno skore, ktereho hrac dosahl. Pokud je toto skore v nejlepsich deseti dosud dosazenych highscore, je mezi ne ulozeno.

Tlacitkem Znovu hrac muze hrat dalsi hru, pripadne tlacitkem Exit hru ukonci.

## Programatorska cast

Hra je napsana v jazyce C# za pouziti Windows Forms.

### Rozdeleni na tridy

**Trida ```Game.cs```** - ridi prubeh hry a udrzuje vsechny prvky, ktere se objevuji na obrazovce. Taktez udrzuje aktualni mod hry - ```enum GameMode``` - (vyse zmineno) a aktualni stav hry. Jednotlive stavy jsou ulozeny v enum ```GameState``` a mohou nabyvat tri hodnot:
 - NotStarted - hra jeste nezacala
 - Started - hra bezi
 - End - hra skoncila, protoze hrac prohral
 
Trida obsahuje metody:: 
 - ```move()``` - pohne se vsemi prvky na obrazovce. Sleduje, zda-li hrac nezemrel, pak zmeni stav hry na ```GameState.End```. Pokud jsou vyobrazeny vsechny objekty na obrazovce (bloky a prisery), pripravi dalsi, ktere se teprve zobrazi pomoci metod ```generate_new_block()``` a ```generate_monster()```, pripadne zmeni i mod hry. Mod se zmeni az pote, co pro nastavajici mod byl pouzity predem dany pocet bloku (```blocks_remaining_until_mode_change```). V pripade, ze nektere bloky a prisery hrac preskocil a jiz se nachazeji mimo obrazovku, pomoci metody ```delete_old_moveable_objects()``` tyto objekty smaze, aby nezabiraly zbytecnou pamet.
 - ```draw()``` - vykresli pozadi a prekresli vsechny prvky na obrazovce.
 
 **Abstraktni trida ```MoveableObject.cs```** - zarucuje potomkum metody ```move()```, pomoci ktere se budou menit svou polohu na obrazovce, ```draw()```, ktera bude slouzit k prekresleni objektu. Nakonec metoda ```energy_on_collision()``` vraci `int` hodnotu udavajici, kolik energie hrac dostane po odrazu s objektem. Tato energie je vyuzita k vyskoku smerem vzhuru. Trampolina tedy bude vracet vyssi hodnotu nez obycejny blok. Pokud stret s objektem je pro hrace smrtelny, bude objekt vracet hodnotu -1.
 
 **Trida `Block.cs`** - potomek tridy `MoveableObjects.cs`, implementuje tedy vsechny 3 abstraktni metody a delaji presne to, k cemu jsou predurcene. Vsechny promenne bloku jsou nastavitelne - collision_energy, sirka a vyska bloku, vzhled bloku, rychlost bloku atd. a proto pro jednotlive bloky nemam dalsi tridy, bylo by to zbytecne.
 
 **Trida `Monster.cs`** - opet potomek tridy `MoveableObjects.cs`, musi tedy implementovat vsechny jeji metody. Rozhodl jsem se, ze se monstrum nebude pohybovat, protoze by to pro hrace bylo moc narocne. Z toho duvodu je metoda `move()` prazdna. Metoda `energy_on_collision()` vraci hodnotu -1, protoze hrac pri stretu s priserou zemre.
 
 **Trida `Player.cs`** - trida reprezentujici hrace. Obsahuje metody `move()` a `draw()`. Metoda `move()` zajistuje simulaci gravitace pro pohyb hrace. Delka vyskoku je urcena hodnotou, kterou vrati blok metodou `energy_on_collision()`. Metoda tedy zkontroluje vsechny objekty na obrazovce a jejich kolize s hracem pomoci metod `collision_with_monster()` a `collision_with_block()`. Sleduje take, zda-li je hrac stale zivy - zda se nedotkl prisery nebo nespadl mimo obrazovku. Pokud pri vyskoku dosahne hrac vysky urcene v `MAX_HEIGHT_OF_PLAYER`, hrac se na obrazovce neposouva dale nahoru, naopak zustava stat a vsechny ostatni objekty na obrazovce se posouvaji dolu.
 
**Trida `HighscoreHandler`** - cte spravuje highscore ze souboru `highscore.txt`. Pokud soubor neexistuje, vytvori ho. Metoda `add_high_score prida` nove highscore a ulozi ho do souboru.

**Trida `Form1.cs`** - pri spusteni programu vytvori instanci tridy `Game.cs`. Zacatecni stav je `NotStarted` - v tomto stavu ziska aplikace pomoci tridy `HighscoreHandler.cs` highscore a vyobrazi je na obrazovce. Pri stisku tlacitka Hraj se hra prepne do stavu `Started`, spusti se timer a zacne samotna hra. Pri kazdem tiku timeru se vse na obrazovce posune a prekresli. K tomu slouzi jiz zminene dve metody `move()` a `draw()` tridy `Game.cs`. Pri prohre se zmeni stav hry na `End`, pokud je nynejsi skore v nejlepsich 10ti, ulozi se. Na obrazovce jsou dve tlacitka: Znovu pro opetovne hrani hry a Exit pro ukonceni hry.

Hlavni funkci teto tridy je spravovani tlacitek a vyobrazeni dat na obrazovce podle daneho stavu hry.
