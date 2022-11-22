using Microsoft.Extensions.Configuration;

namespace LexNetGameR
{
    internal static class ConsoleUIHelpers
    {

        public static string GetColor(this IConfiguration config, string value)
        {
            var section = config.GetSection("game:colors");
            return section[value];
        }


    }
}