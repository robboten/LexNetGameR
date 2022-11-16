using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexNetGameR
{
    internal class Entity
    {
        public char Symbol { get; set; }
        public string Name { get; set; }    
        public ConsoleColor Color { get; set; } = ConsoleColor.Green;
        public Vector2Int Position {get;set;}
        public Vector2Int Acceleration { get; set; } //keep or not?
        public bool IsActive {get;set;}
        public bool IsStatic {get;set;}
        public bool IsPlayer {get;set;}

        public Entity(string name,char symbol)
        {
            Position = new(1,1);
            Symbol = symbol;
            Name = name;
            IsActive = true;
        }

        public Entity(string name, char symbol, Vector2Int position)
        {
            Position = position;
            Symbol = symbol;
            Name = name;
            IsActive = true;
        }
        public Vector2Int GetPos()
        {
            return Position;
        }
        public void SetPosAcc(Vector2Int Acc)
        {
            Position += Acc;
            Vector2Int pos = Position;
            pos.Clamp(0, 9); //why doesn't this work directly on Position?
            Position=pos;
        }
        public void SetPos(Vector2Int Pos)
        {
            Position = Pos;
        }

    }
}
