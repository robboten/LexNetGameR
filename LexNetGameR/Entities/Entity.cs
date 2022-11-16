using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexNetGameR.Entities
{
    internal class Entity
    {
        public char Symbol { get; set; }
        public ConsoleColor Color { get; set; } = ConsoleColor.Green;
        public Vector2Int Position { get; set; }
        public Vector2Int Acceleration { get; set; } 
        public bool IsActive { get; set; } = true; //not sure this is needed since if it's not in list it won't be active... but just in case..
        public bool IsStatic { get; set; } = false;
        public bool IsPlayer { get; set; } = false;


        public Entity(char symbol)
        {
            Position = new(1, 1);
            Symbol = symbol;
        }

        public Entity(char symbol, Vector2Int position)
        {
            Position = position;
            Symbol = symbol;
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
            Position = pos;
        }
        public void SetPos(Vector2Int Pos)
        {
            Position = Pos;
        }

        public void RenderEntity()
        {
            Position += Acceleration;

            UI.OutputSymbol(Color, Symbol.ToString(), Position);

        }
        public void RenderEntity(Vector2Int acceleration)
        {
            Acceleration = acceleration;
            Position += Acceleration;

            UI.OutputSymbol(Color, Symbol.ToString(), Position);
        }

        public void RenderEntityTrace(char symbol)
        {
            UI.OutputSymbol(UI.White, symbol.ToString(), Position);
        }

    }
}
