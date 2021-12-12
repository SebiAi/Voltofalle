# Voltofalle
## General
Voltofalle (*German*) or also called Voltobal Flip is a gambling game found in the gambling hall of Dukatia City and Orismania City in Pokemon Soul Silver and Pokemon Heart Gold.
The goal of this projekt is to make the best possible automatic solver for this puzzle. For more info about the game look [here](https://www.bisafans.de/spiele/editionen/heartgold-soulsilver/voltofalle.php) (*German*).

## Features
The following features are present (or planed):
+ [*DONE*] **Dead row detection**
+ [*WIP*] **Basic solving (one unknown value in one row)**
+ [*WIP*] **Advanced solving (solving as many fields as mathematically possible)**
+ [*WIP*] **When not fully mathematically solvable display the next best possible move**

## Methods used
To give a rough understanding of the concepts used, I will split them up into their respective topics.

### Dead row detection
A dead row is defined as follows:
+ **The Points + the Voltobals** add up to **5**
+ **The Points + the Voltobals** add up to the **same** amount when **summing up the values in the row** (*Voltobals and empty fields count as 1*)

With these criterias it is very simple to implement

### Basic solving
This is just plain old simple subtracting, nothing fancy. We take a look what number is still needed and insert it accordingly.

### Advanced solving
When we calculate on what field what number could be and cross reference this with the rows/columns we could potentially get further hints on what values can be on what field.
Futhermore, by then cross referencing the already discovered fields + the way the Points can be split accross the fields (*e.g.: 5 over 3 fields: {{1,1,3},{1,2,2}}*) it is possible to further shrink the possible numbers down.

### Next bets possible move
By calculating the ratio of the Points to the Voltobals we can determine the chance of explosion
