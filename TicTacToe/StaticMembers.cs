namespace TicTacToe;

public static class StaticMembers
{
    private const string Board = """
                                  {0} | {1} | {2}
                                 -----------
                                  {3} | {4} | {5}
                                 -----------
                                  {6} | {7} | {8}
                                 """;

    private static readonly Dictionary<int, Player> UsedFields = new();
    private static Player? _player1 = new();


    internal static void OnWin(Player player)
    {
        Console.Clear();
        DisplayBoard();
        Console.WriteLine($"Game over! {player.Nick} with symbol {player.Symbol} won! Congratulations");
    }

    internal static void OnDraw()
    {
        Console.Clear();
        DisplayBoard();
        Console.WriteLine("Game over! It's a draw!");
    }

    internal static bool IsBoardFull()
    {
        return UsedFields.Count == 9;
    }

    internal static bool HasWon(Player player)
    {
        var playerFields = UsedFields.Where(x => x.Value == player).Select(x => x.Key).ToList();
        var wonAny = HasWinningFields(0, 1, 2) || HasWinningFields(3, 4, 5) || HasWinningFields(6, 7, 8) ||
                     HasWinningFields(0, 3, 6) || HasWinningFields(1, 4, 7) || HasWinningFields(2, 5, 8) ||
                     HasWinningFields(0, 4, 8) || HasWinningFields(2, 4, 6);

        return wonAny;


        bool HasWinningFields(int pos1, int pos2, int pos3)
        {
            var has1 = playerFields.Contains(pos1);
            var has2 = playerFields.Contains(pos2);
            var has3 = playerFields.Contains(pos3);

            return has1 && has2 && has3;
        }
    }

    internal static void DisplayBoard()
    {
        Console.Clear();

        object?[] strParams = Enumerable.Range(0, 9).Select(_ => (object)" ").ToArray();
        for (var i = 0; i <= 8; i++)
            strParams[i] = UsedFields.TryGetValue(i, out var value) ? value.Symbol.ToString() : " ";
        Console.WriteLine(Board, strParams);
    }

    internal static void GetPositionAndSeed(Player player)
    {
        var cords = GetCords();
        var translated = TranslatePos(cords);
        while (UsedFields.ContainsKey(translated))
        {
            Console.Clear();
            DisplayBoard();
            Console.WriteLine("This field is already taken. Please choose another one.");
            cords = GetCords();
            translated = TranslatePos(cords);
        }

        UsedFields.Add(translated, player);
    }

    private static (int, int) GetCords()
    {
        var x = GetCord();
        var y = GetCord();

        return (x, y);
    }

    private static int GetCord()
    {
        int cord;
        var input = string.Empty;
        while (!int.TryParse(input, out cord) || cord < 1 || cord > 3)
        {
            Console.WriteLine("Please enter a valid number between 1 and 3:");
            input = Console.ReadLine();
        }

        return cord;
    }

    internal static Player GetPlayer()
    {
        var name = GetName();
        var sym = GetSymbol();

        _player1 ??= new Player { Nick = name, Symbol = sym };

        while (_player1.Symbol == sym)
        {
            Console.WriteLine("This symbol is already taken. Please choose another one.");
            sym = GetSymbol();
        }

        return new Player { Nick = name, Symbol = sym };
    }

    private static string GetName()
    {
        Console.Write("Please enter your name: ");
        var name = Console.ReadLine();
        while (string.IsNullOrEmpty(name))
        {
            Console.WriteLine("Please enter a valid name.");
            name = Console.ReadLine();
        }

        Console.Clear();
        return name;
    }

    private static char GetSymbol()
    {
        var symbol = string.Empty;
        char sym;
        while (!char.TryParse(symbol, out sym) || sym.ToString().Length != 1)
        {
            Console.Write("Please choose your symbol (1 character): ");
            symbol = Console.ReadLine();
        }

        Console.Clear();
        return sym;
    }

    private static int TranslatePos((int, int) pos)
    {
        return pos switch
        {
            (1, 1) => 0,
            (1, 2) => 1,
            (1, 3) => 2,
            (2, 1) => 3,
            (2, 2) => 4,
            (2, 3) => 5,
            (3, 1) => 6,
            (3, 2) => 7,
            (3, 3) => 8,
            _ => throw new ArgumentOutOfRangeException(nameof(pos))
        };
    }
}