// 1. There are 5 homes in a row numbered 1 to 5. Each house is painted a different color and has
// individuals of different countries, each owning different pets, drinking different beverages,
// and enjoying different brands of candy.
// 2. The Englishman lives in the red house.
// 3. The Spaniard owns the dog.
// 4. Coffee is drunk in the green house.
// 5. The Ukrainian drinks tea.
// 6. The green house is numbered one more than the ivory house.
// 7. The person who eats M&Ms owns snails.
// 8. Kit Kats are eaten in the yellow house.
// 9. Milk is drunk in home 3.
// 10. The Norwegian lives in home 1.
// 11. The man who eats Candbury's lives in the house next to the man with the fox.
// 12. Kit Kats are eaten in the house next to the house where the horse is kept.
// 13. The Snickers eater also drinks orange juice.
// 14. The Japanese enjoys Reese's candy.
// 15. The Norwegian lives next to the blue house.
// Now who drinks water? And who owns the zebra?

// House: 1, 2, 3, 4, 5
// Color: red, green, ivory, yellow, blue
// Country: England, Spain, Ukraine, Norway, Japan
// Pet: dog, snail, fox, horse, zebra
// Beverage: coffe, tea, milk, orange juice, water
// Candy: M&Ms, Kit Kat, Candbury, Snickers, Reese's

using Grid = System.Collections.Generic.List<System.Collections.Generic.List<int>>;

int Color = 0, Country = 1, Pet = 2, Beverage = 3, Candy = 4;
int red = 0, green = 1, ivory = 2, yellow = 3, blue = 4;
int England = 0, Spain = 1, Ukraine = 2, Norway = 3, Japan = 4;
int dog = 0, snail = 1, fox = 2, horse = 3, zebra = 4;
int coffe = 0, tea = 1, milk = 2, orangeJuice = 3, water = 4;
int MMs = 0, KitKat = 1, Candbury = 2, Snickers = 3, Reeses = 4;

Grid grid =
[
    [ -1, -1, -1, -1, -1 ], // House 1 (Color, Country, Pet, Beverage, Candy)
    [ -1, -1, -1, -1, -1 ], // House 2
    [ -1, -1, -1, -1, -1 ], // House 3
    [ -1, -1, -1, -1, -1 ], // House 4
    [ -1, -1, -1, -1, -1 ], // House 5
];

List<Func<bool>> conditions =
[
    // 2. The Englishman lives in the red house.
    () => CheckHouseCondition(Country, England, Color, red),

    // 3. The Spaniard owns the dog.
    () => CheckHouseCondition(Country, Spain, Pet, dog),
    
    // 4. Coffee is drunk in the green house.
    () => CheckHouseCondition(Beverage, coffe, Color, green),

    // 5. The Ukrainian drinks tea.
    () => CheckHouseCondition(Country, Ukraine, Beverage, tea),

    // 6. The green house is numbered one more than the ivory house.
    () => CheckNeighborCondition(Color, ivory, Color, green, ordered: true),

    // 7. The person who eats M&Ms owns snails.
    () => CheckHouseCondition(Candy, MMs, Pet, snail),

    // 8. Kit Kats are eaten in the yellow house.
    () => CheckHouseCondition(Candy, KitKat, Color, yellow),

    // 9. Milk is drunk in home 3.
    () => grid[2][Beverage] == milk || !grid.Any(h => h[Beverage] == milk),

    // 10. The Norwegian lives in home 1.
    () => grid[0][Country] == Norway || !grid.Any(h => h[Country] == Norway),

    // 11. The man who eats Candbury's lives in the house next to the man with the fox.
    () => CheckNeighborCondition(Candy, Candbury, Pet, fox),

    // 12. Kit Kats are eaten in the house next to the house where the horse is kept.
    () => CheckNeighborCondition(Candy, KitKat, Pet, horse),

    // 13. The Snickers eater also drinks orange juice.
    () => CheckHouseCondition(Candy, Snickers, Beverage, orangeJuice),

    // 14. The Japanese enjoys Reese's candy.
    () => CheckHouseCondition(Country, Japan, Candy, Reeses),

    // 15. The Norwegian lives next to the blue house.
    () => CheckNeighborCondition(Country, Norway, Color, blue),
];

bool CheckHouseCondition(int prop1, int value1, int prop2, int value2)
{
    int house1 = grid.FindIndex(h => h[prop1] == value1);
    int house2 = grid.FindIndex(h => h[prop2] == value2);

    if (house1 >= 0 && house2 >= 0)
        return house1 == house2;

    if (house1 >= 0)
        return grid[house1][prop2] == -1;

    if (house2 >= 0)
        return grid[house2][prop1] == -1;

    return true;
}

bool CheckNeighborCondition(int prop1, int value1, int prop2, int value2, bool ordered = false)
{
    if (!ordered)
        return CheckNeighborCondition(prop1, value1, prop2, value2, ordered: true) ||
               CheckNeighborCondition(prop2, value2, prop1, value1, ordered: true);

    int house1 = grid.FindIndex(h => h[prop1] == value1);
    int house2 = grid.FindIndex(h => h[prop2] == value2);

    if (house1 >= 0 && house2 >= 0)
        return house2 - house1 == 1;

    if (house1 >= 0)
        return house1 + 1 < 5 && grid[house1 + 1][prop2] == -1;

    if (house2 >= 0)
        return house2 > 0 && grid[house2 - 1][prop1] == -1;

    return true;
}

void GenerateGridsRecursively(int gridCell)
{
    int house = gridCell / 5;
    int prop = gridCell % 5;

    if (house == 5)
    {
        PrintGrid();
        return;
    }

    for (int value = 0; value < 5; value++)
    {
        // Check rule 1.
        if (grid.Any(h => h[prop] == value))
            continue;

        grid[house][prop] = value;

        if (conditions.All(c => c()))
            GenerateGridsRecursively(gridCell + 1);

        grid[house][prop] = -1;
    }
}

void PrintGrid()
{
    List<(string prop, List<string> values)> textTable =
    [
        ("Color", ["red", "green", "ivory", "yellow", "blue"]),
        ("Country", ["England", "Spain", "Ukraine", "Norway", "Japan"]),
        ("Pet", ["dog", "snail", "fox", "horse", "zebra"]),
        ("Beverage", ["coffe", "tea", "milk", "orange juice", "water"]),
        ("Candy", ["M&Ms", "Kit Kat", "Candbury", "Snickers", "Reese's"])
    ];


    Console.WriteLine("Solution found:");
    Console.WriteLine("House: 1, 2, 3, 4, 5");

    for (int i = 0; i < 5; i++)
    {
        var (prop, values) = textTable[i];

        Console.Write(prop + ": ");
        Console.Write(string.Join(", ", grid.Select(h => values[h[i]])));
        Console.WriteLine();
    }

    int waterDrinkerHouse = grid.FindIndex(h => h[Beverage] == water);
    string waterDrinkerCountry = textTable[Country].values[grid[waterDrinkerHouse][Country]];

    int zebraOwnerHouse = grid.FindIndex(h => h[Pet] == zebra);
    string zebraOwnerCountry = textTable[Country].values[grid[zebraOwnerHouse][Country]];

    Console.WriteLine($"The person from {waterDrinkerCountry} drinks the water," +
        $" and the person from {zebraOwnerCountry} owns the zebra.");
    Console.WriteLine();
}

GenerateGridsRecursively(0);

Console.WriteLine("Press any key to exit...");
Console.ReadKey();
