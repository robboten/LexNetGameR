using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace LexNetGameR
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json",optional:false,reloadOnChange:true)
                .Build();

            //var ui = config.GetSection("game:ui").Value;

            //var ent = config.GetSection("game:entities").Value;
            //string[][] maze = config.GetSection("game:maze").Get<List<string[]>>()!.ToArray();
            //var bg = config.GetSection("game:colors:backgroundcolor").Value;

            //Console.WriteLine($"{maze.GetLength(0)}-{maze[0].Length}");
            //Console.WriteLine($"{maze[5][0]}");
            //Console.WriteLine($"{string.Concat(maze[0]).GetType()}");
            //char[][] m2=maze.Select(x => string.Concat(x).ToCharArray()).ToArray();
            
            //Console.WriteLine($"{m2[0][0]}");
            //char[,] mm = m2;
            //IUI implementation;
            //switch(ui)
            //{ 
            //    case "console":
            //        implementation = new ConsoleUI();
            //        var game = new Game(implementation);
            //        game.Run();
            //        break;
            //    default:
            //        break;

            //}

            var game = new Game(new ConsoleUI(config), config);
            game.Run();


        }
    }
}