using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexNetGameR
{
    public interface IUI
    {
        void InitUI();
        public void ShowPoints(Vector2Int position, int Score);
        public void OutputSymbol(string color, string symbol, Vector2Int pos);
    }
}