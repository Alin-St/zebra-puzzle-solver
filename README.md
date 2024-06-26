# Zebra Puzzle Solver

This is a C# console application that helps you solve the [Zebra Problem](https://en.wikipedia.org/wiki/Zebra_Puzzle) by brute force.

The program is based on an adaptation of the problem prezented in [this video](https://youtu.be/cKRAH2maFis) by MindYourDecisions.

### How it works?

The program tries to generate all possible grids (solutions) using recursivity, cell by cell. At each step it checks the list of conditions given in the problem before continuing.

### Code

The relevant code can be found in [Program.cs](./ZebraPuzzleSolver/Program.cs).

The project can be opened with Visual Studio.

### Output

After running the program the solution will be displayed almost instantly:

```
Solution found:
House: 1, 2, 3, 4, 5
Color: yellow, blue, red, ivory, green
Country: Norway, Ukraine, England, Spain, Japan
Pet: fox, horse, snail, dog, zebra
Beverage: water, tea, milk, orange juice, coffe
Candy: Kit Kat, Candbury, M&Ms, Snickers, Reese's
The person from Norway drinks the water, and the person from Japan owns the zebra.
```
