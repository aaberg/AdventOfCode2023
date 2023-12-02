namespace TheLibrary.Day2;

public record Game(int Id, List<GameSet> Sets);

public record GameSet(int Green, int Blue, int Red);

public static class GameExtension
{
    public static Game ParseStringAsDay2Game(this string line)
    {
        var id = int.Parse(line.Split(" ")[1].Replace(":", ""));

        var game = new Game(id, new List<GameSet>());
        
        var setsStrings = line.Split(':')[1].Split(';');
        foreach (var setString in setsStrings)
        {
            var set = new GameSet(0,0,0);
            var blocks = setString.Split(',').Select(b => b.Trim());
            foreach (var block in blocks)
            {
                var count = block.Split(' ')[0];
                var color = block.Split(' ')[1];
                
                switch (color)
                {
                    case "green":
                        set = set with {Green = int.Parse(count)};
                        break;
                    case "blue":
                        set = set with {Blue = int.Parse(count)};
                        break;
                    case "red":
                        set = set with {Red = int.Parse(count)};
                        break;
                }
            }
            game.Sets.Add(set);
        }

        return game;
    }
}