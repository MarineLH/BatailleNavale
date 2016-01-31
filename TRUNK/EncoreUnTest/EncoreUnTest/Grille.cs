using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoreUnTest
{

    public class Grille
    {
        public Case[,] grille { get; set; }
        public Grille()
        {
            grille = new Case[12, 12];

            int x = 0, y = 0;

            foreach (Case cellule in grille)
            {
                grille.SetValue(new Case(x, y), x, y);
                x += 1;
                if (x == 12)
                {
                    x = 0;
                    y += 1;
                }
            }
        }

        public void Draw()
        {
            int rowLength = grille.GetLength(0);
            int colLength = grille.GetLength(1);

            Console.WriteLine("    1 2 3 4 5 6 7 8 9 10");
            for (int i = 0; i < rowLength; i++)
            {
                if(i > 0 && i < 11)
                    Console.Write("{0} ", Program.Number2String(i, true));
                else
                    Console.Write("  ");
                
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write("{0} ", grille[i, j].Symbole);
                }
                Console.Write(Environment.NewLine);
            }
        }
    }
}
