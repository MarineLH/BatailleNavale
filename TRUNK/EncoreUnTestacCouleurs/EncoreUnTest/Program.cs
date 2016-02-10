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
        public static NumJoueur DernierTir;
        public static string CaseTir;
        // Constante du programme qui sert pour les vérifications de codes des cases.
        public static string[] IdCases = new string[100]
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

        // Méthode pour tirer (No shit Sherlock)
        public static EtatPartie Tirer(Joueur _joueur, Joueur _adversaire, string _code)
        {
            EtatPartie etatPartie = EtatPartie.JRetire;
            Console.Clear();
            if (IdCases.Contains(_code)) // Vérifier que le code de la case est bien valide
            {
                int xTir = int.Parse(_code.Substring(1));
                int yTir = char.ToUpper(char.Parse(_code.Substring(0, 1))) - 64;
                EtatCase etat = _adversaire.MaGrille.grille[yTir, xTir].Tirer(); // Appeler la méthode Tirer() qui est dans Case.cs
                _joueur.NbTirs += 1; // Incrémenter le nombre de tirs du joueur pour les stats de fin de partie. (ex : Partie finie en X tirs)

                switch (etat)
                {
                    case EtatCase.TirRate:
                        _adversaire.MaGrille.grille[yTir, xTir].ChangerEtat(EtatCase.TirRate);  // MaJ Emplacement du tir sur la grille de l'adversaire
                        _joueur.SaGrille.grille[yTir, xTir].ChangerEtat(EtatCase.TirRate);
                        etatPartie = EtatPartie.JSuivant;                                       // MaJ Emplacement du tir sur la grille du joueur représentant celle de l'adv.
                        break;

                    case EtatCase.TirRateAlready:
                        // Si on est un peu con et qu'on re-tire au même endroit
                        Console.WriteLine("Vous avez déjà tiré ici ! Réessayez.\r\n");              // Bah on peut réessayer. C'est gentil non ?
                        etatPartie = EtatPartie.JRetire; 
                        break;

                    case EtatCase.BateauTouche:
                        _adversaire.MaGrille.grille[yTir, xTir].ChangerEtat(EtatCase.BateauTouche); // Si on réussit un tir, on update les grilles, et on tire à nouveau.
                        _joueur.SaGrille.grille[yTir, xTir].ChangerEtat(EtatCase.BateauTouche);
                        break;

                    case EtatCase.BateauToucheAlready:                                          // Là encore, c'est pour les teubés.
                        Console.WriteLine("Vous avez déjà touché un bateau ici ! Réessayez.\r\n");
                        etatPartie = EtatPartie.JRetire;
                        break;
                }
            }
            else // Sinon demander de tirer à nouveau avec un nouveau code.
            {
                Console.WriteLine("Erreur. Vous avez mal écrit le numéro de la case. Réessayez.\r\n");
                etatPartie = EtatPartie.JRetire;
            }

            // Faire le listing des bateaux qu'il reste à l'adversaire.
            int i = 1; // Compteurs pour les bateaux existant deux fois (torpilleur et sous marin)
            int j = 1;
            foreach (var bateau in _adversaire.ListBateaux)
            {
                if (bateau.EstCoule(_adversaire.MaGrille, _joueur.SaGrille))
                {
                    if (!_adversaire.ResteBateau())
                    {
                        etatPartie = EtatPartie.PTerminée;
                        break;
                    }
                    Console.WriteLine("Un {0} de l'adversaire est coulé !", bateau.Nom);
                    _adversaire.MaFlotte.PlacerBateau(bateau.Nom); // On réutilise cette fonction pour décrémenter le total de bateaux dans la flotte
                    
                }
                else
                {
                    switch (bateau.Nom)
                    {
                        case NomsBateau.PorteAvions:
                            Console.WriteLine("Il vous reste {0} {1}(s) à couler.", _adversaire.MaFlotte.QuantitePA, bateau.Nom);
                            break;
                        case NomsBateau.Cuirassé:
                            Console.WriteLine("Il vous reste {0} {1}(s) à couler.", _adversaire.MaFlotte.QuantiteCuir, bateau.Nom);
                            break;
                        case NomsBateau.Croiseur:
                            Console.WriteLine("Il vous reste {0} {1}(s) à couler.", _adversaire.MaFlotte.QuantiteCrois, bateau.Nom);
                            break;
                        case NomsBateau.Torpilleur:
                            
                            if (i == 1)
                                Console.WriteLine("Il vous reste {0} {1}(s) à couler.", _adversaire.MaFlotte.QuantiteTorpi, bateau.Nom);
                            i++;
                            break;
                        case NomsBateau.SousMarin:
                            
                            if (j == 1)
                                Console.WriteLine("Il vous reste {0} {1}(s) à couler.", _adversaire.MaFlotte.QuantiteSousMarin, bateau.Nom);
                            j++;
                            break;
                    }
                }
            }

            // Actualisation des grilles :
            Console.WriteLine("\r\nVotre grille :");                                    
            _joueur.MaGrille.Draw();
            Console.WriteLine("Grille de votre adversaire :");
            _joueur.SaGrille.Draw();
            return etatPartie;
        }

        static void Main(string[] args)
        {
            EtatPartie etat = EtatPartie.JRetire;
            Console.WriteLine("Création du joueur 1. Votre nom ?");
            Joueur joueur1 = new Joueur(Console.ReadLine(), NumJoueur.J1);
            joueur1.FaireGrille();

            Console.Clear();
            Console.WriteLine("Vos bateaux ont bien été placés. Laissez la main au second joueur... Appuyez sur une touche pour continuer.");
            Console.ReadKey();

            Console.WriteLine("Joueur 2. Votre nom ?");
            Joueur joueur2 = new Joueur(Console.ReadLine(), NumJoueur.J2);
            joueur2.FaireGrille();

            Console.Clear();
            Console.WriteLine("Vos bateaux ont bien été placés. La grille va être cachée... Appuyez sur une touche pour continuer.");
            Console.ReadKey();

            Console.WriteLine("Laissez la main au premier joueur.");
            Console.ReadKey();          

            DernierTir = NumJoueur.J2;
            while (etat != EtatPartie.PTerminée)
            {
                if (DernierTir == NumJoueur.J2)
                {
                    Console.WriteLine("Votre grille :");
                    joueur1.MaGrille.Draw();
                    Console.WriteLine("Grille de votre adversaire :");
                    joueur1.SaGrille.Draw();
                    do
                    {                    
                        Console.WriteLine("{0}({1}), choisissez une case sur laquelle tirer.", joueur1.Nom, joueur1.ID);
                        CaseTir = Console.ReadLine();
                        etat = Tirer(joueur1, joueur2, CaseTir);
                        DernierTir = NumJoueur.J1;
                    } while (etat == EtatPartie.JRetire);
                }
                else if (DernierTir == NumJoueur.J1)
                {
                    Console.WriteLine("Votre grille :");
                    joueur2.MaGrille.Draw();
                    Console.WriteLine("Grille de votre adversaire :");
                    joueur2.SaGrille.Draw();
                    do
                    {                       
                        Console.WriteLine("{0}({1}), choisissez une case sur laquelle tirer.", joueur2.Nom, joueur2.ID);
                        CaseTir = Console.ReadLine();
                        etat = Tirer(joueur2, joueur1, CaseTir);
                        DernierTir = NumJoueur.J2;
                    } while (etat == EtatPartie.JRetire);            
                }


                if (etat == EtatPartie.JSuivant)
                {
                    Console.WriteLine("Appuyez sur une touche et laissez la main au joueur suivant.");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("Appuyez sur une touche pour continuer.");
                    Console.ReadKey();
                }
            }

            Console.Clear();

            if (DernierTir == NumJoueur.J1)
                Console.WriteLine("Félicitations {0} ({1}), vous avez gagné la partie en {2} tirs !", joueur1.Nom, DernierTir, joueur1.NbTirs);
            else
                Console.WriteLine("Félicitations {0} ({1}), vous avez gagné la partie en {2} tirs !", joueur2.Nom, DernierTir, joueur2.NbTirs);

            Console.ReadKey();
        }
    }
}
