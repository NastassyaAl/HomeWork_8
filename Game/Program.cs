Console.Clear();
string[,] menu = {{"@","Начать игру"," "},
                    {" ","Выбор уровня"," "},
                    {" ","Выход"," "},};
string[,] levelMenu = {{"@","уровень 1"," "},
                       {" ","уровень 2"," "},
                       {" ","уровень 3"," "},
                       {" ","уровень 4"," "},
                       {" ","уровень 5"," "},};
string[,] matrix = {{"@"," "," "," "," "," "},
    {" "," "," "," "," "," "},
    {" "," "," "," "," "," "},
    {" "," "," "," "," "," "},
    {" ","$"," "," "," "," "},
    {" "," "," "," "," "," "}};
Dictionary<int, string[,]> Lvls = new Dictionary<int, string[,]>{{1,
new string[,]{{"@"," "," "," "," "},
              {" ","|"," "," "," "},
              {" "," "," ","|"," "},
              {" "," "," "," "," "},
              {" "," "," ","$"," "},
              {" "," "," "," "," "}}},
              {2,
new string[,]{{"@"," "," "," "," "},
              {" ","|"," ","|"," "},
              {"|"," "," ","|"," "},
              {" "," "," "," "," "},
              {" "," "," ","$"," "},
              {" ","|"," "," "," "}}},
              {3,
new string[,]{{"@"," "," "," "," "," "},
              {" ","|"," ","|"," "," "},
              {" "," "," ","|"," "," "},
              {" "," "," "," "," "," "},
              {" "," "," ","$","|"," "},
              {" ","|"," "," "," "," "},
              {" ","|"," "," "," "," "}}},
              {4,
new string[,]{{"@","|"," "," "," "," "},
              {" ","|"," ","|"," "," "},
              {" "," "," ","|"," "," "},
              {" "," ","|"," "," "," "},
              {" "," ","|","$","|"," "},
              {" ","|"," "," ","|"," "},
              {" ","|"," "," "," "," "}}},
              {5,
new string[,]{{"@","|"," "," "," ","|"," "},
              {" "," "," ","|"," ","|"," "},
              {"|"," "," ","|"," "," "," "},
              {" ","|"," ","|"," "," ","|"},
              {" "," "," ","$","|"," "," "},
              {"|"," ","|"," ","|"," "," "},
              {" "," "," "," "," "," "," "},
              {" ","|"," ","|"," "," ","|"}}}};
int coins = 0;
int MenuX = 0;
int MenuY = 0;

int SelectMenuPlauer(string[,] array, int index)
{
    MatrixWrite(array);
    ConsoleKeyInfo User_keyTab = Console.ReadKey();
    while (User_keyTab.Key != ConsoleKey.Enter)
    {
        Console.Clear();
        array[MenuY, MenuX] = " ";
        if (User_keyTab.Key == ConsoleKey.W && index > 0)
        {
            index--; MenuY--;
        }
        if (User_keyTab.Key == ConsoleKey.S && index < array.GetLength(0) - 1)
        {
            index++; MenuY++;
        }
        array[MenuY, MenuX] = "@";
        MatrixWrite(array);
        User_keyTab = Console.ReadKey();
    }
    return index;
}

void MatrixWrite(string[,] array)
{
    for (int i = 0; i < array.GetLength(0); i++)
    {
        for (int j = 0; j < array.GetLength(1); j++)
        {
            Console.Write(array[i, j] + " ");
        }
        Console.WriteLine();
    }
    Console.WriteLine("Количество очков " + coins);
}
string[,] ItemFoodMatrix(int x, int y, string[,] array)
{
    while (matrix[y, x] == "$")
    {
        matrix[y, x] = " ";
        int matX = new Random().Next(0, array.GetLength(0));
        int matY = new Random().Next(0, array.GetLength(1));
        while (matrix[matY, matX] == "|")
        {
            matX = new Random().Next(0, array.GetLength(0));
            matY = new Random().Next(0, array.GetLength(1));
        }
        array[matY, matX] = "$";
        coins++;
    }
    return array;
}

bool Barrier(int x, int y)
{
    if (matrix[y, x] == "|") return false;
    return true;
}
int x = 0;
int y = 0;
while (true)
{
    switch (SelectMenuPlauer(menu, 0))
    {
        case 0:
            Console.Clear();
            Game();
            break;
        case 1:
            Console.Clear();
            Level();
            break;
        case 2:
            Console.Clear();
            break;
        default:
            break;
    }
}
void Game()
{
    while (true)
    {
        Console.Clear();
        MatrixWrite(matrix);
        matrix[y, x] = " ";
        ConsoleKeyInfo User_keyTab = Console.ReadKey();
        if (User_keyTab.Key == ConsoleKey.W) if (Barrier(x, y - 1)) y--;
        if (User_keyTab.Key == ConsoleKey.S) if (Barrier(x, y + 1)) y++;
        if (User_keyTab.Key == ConsoleKey.A) if (Barrier(x - 1, y)) x--;
        if (User_keyTab.Key == ConsoleKey.D) if (Barrier(x + 1, y)) x++;

        if (y > matrix.GetLength(0)) y = 0;
        if (y < 0) y = matrix.GetLength(0)-1;
        if (x > matrix.GetLength(1)) x = 0;
        if (x < 0) x = matrix.GetLength(1)-1;
        matrix = ItemFoodMatrix(x, y, matrix);
        matrix[y, x] = "@";
    }
}

void Level()
{
    while (true)
    {
        switch (SelectMenuPlauer(levelMenu, 1))
        {
            case 0:
                Console.Clear();
                matrix = Lvls[1];
                Game();
                break;
            case 1:
                Console.Clear();
                matrix = Lvls[2];
                Game();
                break;
            case 2:
                Console.Clear();
                matrix = Lvls[3];
                Game();
                break;
            case 3:
                Console.Clear();
                matrix = Lvls[4];
                Game();
                break;
            case 4:
            Console.Clear();
            matrix = Lvls[5];
            Game();
            break;    
            default:
                break;
        }
    }
}
