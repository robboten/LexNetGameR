namespace LexNetGameR
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(new ConsoleUI());
            game.Run();
        }
    }
}