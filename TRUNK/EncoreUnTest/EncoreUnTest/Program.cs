using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace EncoreUnTest
{
    public class Program
    {
        // Constante du programme qui sert pour les vérifications de codes des cases.
        public static string[] IDCases = new string[100]
        {
                "A1", "A2", "A3", "A4", "A5", "A6", "A7", "A8", "A9", "A10", "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "B9", "B10",
                "C1", "C2", "C3", "C4", "C5", "C6", "C7", "C8", "C9", "C10", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "D10",
                "E1", "E2", "E3", "E4", "E5", "E6", "E7", "E8", "E9", "E10", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10",
                "G1", "G2", "G3", "G4", "G5", "G6", "G7", "G8", "G9", "G10", "H1", "H2", "H3", "H4", "H5", "H6", "H7", "H8", "H9", "H10",
                "I1", "I2", "I3", "I4", "I5", "I6", "I7", "I8", "I9", "I10", "J1", "J2", "J3", "J4", "J5", "J6", "J7", "J8", "J9", "J10"
        };

        // Méthode générale du programme qui sert à convertir un nombre en son équivalent dans l'alphabet ( 1 --> A, 2 --> B ...)
        public static String Number2String(int number, bool isCaps)
        {
            Char c = (Char)((isCaps ? 65 : 97) + (number - 1));
            return c.ToString();
        }

        // Méthode qui servira pour tirer (No shit Sherlock)
        public void Tirer(Joueur _joueur, Joueur _adversaire, string _code)
        {
            if (IDCases.Contains(_code)) // Vérifier que le code de la case est bien valide
            {
                int xTir = int.Parse(_code.Substring(1));
                int yTir = char.ToUpper(char.Parse(_code.Substring(0, 1))) - 64;
                EtatCase etat = _adversaire.MaGrille.grille[xTir, yTir].Tirer(); // Appeler la méthode Tirer() qui est dans Case.cs
                _joueur.NbTirs += 1; // Incrémenter le nombre de tirs du joueur pour les stats de fin de partie. (ex : Partie finie en X tirs)

                switch (etat)
                {
                    case EtatCase.TirRate:
                        _adversaire.MaGrille.grille[xTir, yTir].ChangerEtat(EtatCase.TirRate);  // MaJ Emplacement du tir sur la grille de l'adversaire
                        _joueur.SaGrille.grille[xTir, yTir].ChangerEtat(EtatCase.TirRate);      // MaJ Emplacement du tir sur la grille du joueur représentant celle de l'adv.
                        Console.WriteLine("Votre grille :");                                    // Actualisation des grilles :
                        _joueur.MaGrille.Draw();
                        Console.WriteLine("Grille de votre adversaire :");
                        _joueur.SaGrille.Draw();
                        Console.WriteLine("Vous avez tiré dans l'eau.");
                        break;

                    case EtatCase.TirRateAlready:                                               // Si on est un peu con et qu'on re-tire au même endroit
                        Console.WriteLine("Vous avez déjà tiré ici ! Réessayez.");              // Bah on peut réessayer. C'est gentil non ?
                        Tirer(_joueur, _adversaire, Console.ReadLine());    
                        break;

                    case EtatCase.BateauTouche:
                        _adversaire.MaGrille.grille[xTir, yTir].ChangerEtat(EtatCase.BateauTouche); // Si on réussit un tir, on update les grilles, et on tire à nouveau.
                        _joueur.SaGrille.grille[xTir, yTir].ChangerEtat(EtatCase.BateauTouche);
                        Console.WriteLine("Votre grille :");
                        _joueur.MaGrille.Draw();
                        Console.WriteLine("Grille de votre adversaire :");
                        _joueur.SaGrille.Draw();
                        Console.WriteLine("Vous avez touché un bateau ! Continuez !");
                        Tirer(_joueur, _adversaire, Console.ReadLine());
                        break;

                    case EtatCase.BateauToucheAlready:                                          // Là encore, c'est pour les teubés.
                        Console.WriteLine("Vous avez déjà touché un bateau ici ! Réessayez.");
                        Tirer(_joueur, _adversaire, Console.ReadLine());
                        break;
                }

            }
            else // Sinon demander de tirer à nouveau avec un nouveau code.
            {
                Console.WriteLine("Erreur. Vous avez mal écrit le numéro de la case. Réessayez.");
                Tirer(_joueur, _adversaire, Console.ReadLine());
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Création du joueur 1. Votre nom ?");
            Joueur joueur1 = new Joueur(Console.ReadLine(), NumJoueur.J1);

            joueur1.FaireGrille();

            Console.WriteLine("Vos bateaux ont bien été placés. Laissez la main au second joueur... Appuyez sur une touche pour continuer.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Joueur 2. Votre nom ?");
            Joueur joueur2 = new Joueur(Console.ReadLine(), NumJoueur.J2);

            joueur2.FaireGrille();

            Console.WriteLine("Vos bateaux ont bien été placés. La grille va être cachée... Appuyez sur une touche pour continuer.");
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Affichage des deux grilles...");
            joueur1.MaGrille.Draw();
            joueur1.SaGrille.Draw();

            joueur2.MaGrille.Draw();
            joueur2.SaGrille.Draw();

            Console.ReadKey();
            
            /* Tant qu'il reste des bateaux en jeu dans une des deux grilles, continuer la partie.
            Les joueurs tirent chacun leur tour, sauf s'ils ont touché un bateau, auquel cas, ils peuvent tirer à nouveau.
            */
        }
    }
}
