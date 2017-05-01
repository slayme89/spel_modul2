# Spelkonstruktions projekt
## Hur man lägger till sina ändringar

Efter du har lagt till dina commits till ditt lokala repo så gör följande:
1. Gå in under "Synchronization" och tryck på "Fetch".
2. Finns det inkommande commits så måste du göra en rebase, annars gå till steg 6.
3. Gå in under "Branches" och välj "Rebase".
4. Välj att rebasa från master till origin/master.
5. Lös eventuella konflikter för att göra klart rebasen.
6. Nu kan du gå in under "Synchronization" och välja "Push" för att lägga till dina ändringar på github.

Om du inte följer den här guiden så blir dina commits baserade på två olika grenar i git historien och det ser ut som du har gjort
ändringar som egentligen gjordes av någon annan. Baseras då dina ändringar på en flera dagar gammal branch med mycket ändringar blir det
rörigt och svårt att se vem som gjort vad.
