using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoreUnTest
{

    public class Grille
    {
        public NumJoueur IdJoueur { get; set; }
        public Case[,] grille { get; set; }

        public Grille(NumJoueur _num)
        {
            IdJoueur = _num;
            grille = new Case[12, 12]; // une grille est un tableau de cases

            int x = 0, y = 0;

            foreach (Case cellule in grille) // On initialise chaque case de la grille
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

        // Dessiner la grille pour afficher le terrain de jeu.
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
                    
                    if (grille[i, j].Symbole == "-")
                        Console.ForegroundColor = ConsoleColor.Blue;
                    else if (grille[i, j].Symbole == "X")
                        Console.ForegroundColor = ConsoleColor.Red;
                    else if (grille[i, j].Symbole == "#")
                        Console.ForegroundColor = ConsoleColor.DarkRed;

                    Console.Write("{0} ", grille[i, j].Symbole);
                    Console.ResetColor();

                }
                Console.Write(Environment.NewLine);
            }
        }
    }
}
