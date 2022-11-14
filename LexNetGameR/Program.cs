namespace LexNetGameR
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleColor OriginalBackgroundColor = Console.BackgroundColor;
            ConsoleColor OriginalForegroundColor = Console.ForegroundColor;

            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Yellow;
            //new MusicPlayer().Play();
            //Game, GameState, GameMode, Controller, Pawn (Hero,baddy), Map, Gameworld, Entities(hero, Creatures
            Game game = new();
            game.Run();
        }
    }
}