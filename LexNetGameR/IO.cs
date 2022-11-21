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

            //string entityData = @"[{""Name"":""Hero"",""Symbol"":""H"",""Color"":""Green"",""IsActive"":true,""IsStatic"":false,""IsPlayer"":true},{""Name"":""Ghost"",""Symbol"":""G"",""Color"":""DarkGray"",""IsActive"":true,""IsStatic"":false,""IsPlayer"":false,""Position"":{""X"":0,""Y"":0},""Points"":0}]";

            var entityDataList = JsonSerializer.Deserialize<List<Entity>>(entityData);
            return entityDataList;
        }
    }
}