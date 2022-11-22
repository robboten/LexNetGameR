using LexNetGameR.Entities;
using System.Text.Json;

namespace LexNetGameR
{
    internal class IO
    {
        public static List<Entity>? ReadConfig()
        {
            //move to io
            string path = @"D:\entities.json";

            string entityData = "";
            if (File.Exists(path))
            {
                entityData = File.ReadAllText(path);
            }

            var entityDataList = JsonSerializer.Deserialize<List<Entity>>(entityData);
            return entityDataList;
        }
    }
}