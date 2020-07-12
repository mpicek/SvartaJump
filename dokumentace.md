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


