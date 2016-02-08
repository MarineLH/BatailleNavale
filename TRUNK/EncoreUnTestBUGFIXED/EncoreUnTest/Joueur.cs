using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoreUnTest
{
    public class Joueur
    {
        public string Nom { get; set; }
        public NumJoueur ID { get; set; }
        public Grille MaGrille { get; set; }
        public Grille SaGrille { get; set; }
        public int NbTirs { get; set; }
        public List<Bateau> ListBateaux { get; set; }
        public Flotte MaFlotte { get; set; }

        public Joueur(string _nom, NumJoueur _num)
        {
            Nom = _nom;
            ID = _num;
            SaGrille = new Grille(NumJoueur.J2);
            NbTirs = 0;
        }
        
        // On initialise la grille du joueur
        public void FaireGrille()
        {
            MaGrille = new Grille(ID);
            Console.WriteLine("Création de la grille ...");
            MaGrille.Draw();
            PlacerMaFlotte(); // A quoi ça sert une grille sans bateaux ? :P
        }

        // Beaucoup de code très compliqué mais surtout très répétitif
        public void PlacerMaFlotte()
        {
            NomsBateau NomBat;
            string CodeDep;
            int XBat, YBat;
            Orientation Orient;

            // Initialiser la flotte et la liste des bateaux pour répertorier les différentes instances.
            MaFlotte = new Flotte(ID);
            ListBateaux = new List<Bateau>();

            while (MaFlotte.total != 0)
            {
                Console.WriteLine("Vous pouvez placer {0} porte-avions, {1} cuirassé(s), {2} croiseur(s), {3} torpilleur(s), {4} sous-marin(s).", MaFlotte.QuantitePA, MaFlotte.QuantiteCuir, MaFlotte.QuantiteCrois, MaFlotte.QuantiteTorpi, MaFlotte.QuantiteSousMarin);
                Console.WriteLine("Choisissez un bateau à placer : PorteAvions / Cuirassé / Croiseur / Torpilleur / SousMarin");
                while (!Enum.TryParse(Console.ReadLine(), out NomBat)) // Tant que ce qu'on écrit n'est pas un nom de bateau, demander à écrire un nom de bateau.
                {
                    Console.WriteLine("Attention à l'orthographe ! Vérifiez que vous avez écrit le nom du bateau comme les exemples ci-dessus !");
                }
                Console.WriteLine("Entrez la poupe (l'arrière) du bateau. (ex : A1 / C5 ...)");
                CodeDep = Console.ReadLine();
                while (!Program.IdCases.Contains(CodeDep)) // Tant que ce qu'on écrit n'est pas un code de case, demander à écrire un code de case.
                {
                    Console.WriteLine("Veuillez entrer une valeur crédible. (Pensez à écrire le code en majuscules ! (A1 / C2...))");
                    CodeDep = Console.ReadLine();
                }
                // Une fois que c'est fait, on peut retrouver les coordonnées avec le code.
                XBat = int.Parse(CodeDep.Substring(1));
                YBat = char.ToUpper(char.Parse(CodeDep.Substring(0, 1))) - 64;
                Console.WriteLine("Entrez son orientation : Nord / Sud / Est / Ouest");
                while (!Enum.TryParse(Console.ReadLine(), out Orient)) // Tant que ce qu'on écrit n'est pas une orientation, demander à écrire une orientation
                {
                    Console.WriteLine("Attention à l'orthographe ! Vérifiez que vous avez écrit l'orientation comme les exemples ci-dessus !");
                }

                Console.Clear();
                // En fonction du nom de bateau, on va instancier une sous-classe précise.
                switch (NomBat)
                {
                    case NomsBateau.PorteAvions:                  
                        if (MaFlotte.QuantitePA != 0)
                        {
                            MaFlotte.PlacerBateau(NomsBateau.PorteAvions);
                            Bateau PA = new PorteAvions(XBat, YBat, Orient); // On l'instancie.
                            ListBateaux.Add(PA); // On l'ajoute à la liste de nos bateaux.
                            if (!PA.PeutPlacer(MaGrille, MaFlotte))
                                // Mais s'il s'avère qu'il est impossible de le placer...
                                ListBateaux.Remove(PA);
                        } // On le retire de la liste.
                        else
                        {
                            Console.WriteLine("Vous ne pouvez plus placer de bateau de ce type.");
                        }
                        break;
                    case NomsBateau.Cuirassé: 
                             // Idem pour tous les autres cas.
                        if (MaFlotte.QuantiteCuir != 0)
                        {
                            MaFlotte.PlacerBateau(NomsBateau.Cuirassé);
                            Bateau Cuir = new Cuirrasse(XBat, YBat, Orient);
                            ListBateaux.Add(Cuir);
                            if (!Cuir.PeutPlacer(MaGrille, MaFlotte))
                                ListBateaux.Remove(Cuir);
                        }
                        else
                        {
                            Console.WriteLine("Vous ne pouvez plus placer de bateau de ce type.");
                        }
                        break;
                    case NomsBateau.Croiseur:
                        
                        if (MaFlotte.QuantiteCrois != 0)
                        {
                            MaFlotte.PlacerBateau(NomsBateau.Croiseur);
                            Bateau Crois = new Croiseur(XBat, YBat, Orient);
                            ListBateaux.Add(Crois);
                            if (!Crois.PeutPlacer(MaGrille, MaFlotte))
                                ListBateaux.Remove(Crois);
                        }
                        else
                        {
                            Console.WriteLine("Vous ne pouvez plus placer de bateau de ce type.");
                        }
                        break;
                    case NomsBateau.Torpilleur:                             // Cas particulier : le Torpi et les SM ont sont dispos DEUX fois. Il faut différencier
                        Bateau Torpi1 = null, Torpi2;                       // les instanciations.
                        if (MaFlotte.QuantiteTorpi == 0)
                        {
                            Console.WriteLine("Vous ne pouvez plus placer de bateau de ce type.");
                            break;
                        }
                        if (!ListBateaux.Contains(Torpi1))
                        {
                            MaFlotte.PlacerBateau(NomsBateau.Torpilleur);
                            Torpi1 = new Torpilleur(XBat, YBat, Orient);
                            ListBateaux.Add(Torpi1);
                            if (!Torpi1.PeutPlacer(MaGrille, MaFlotte))
                                ListBateaux.Remove(Torpi1);
                        }
                        else
                        {
                            MaFlotte.PlacerBateau(NomsBateau.Torpilleur);
                            Torpi2 = new Torpilleur(XBat, YBat, Orient);
                            ListBateaux.Add(Torpi2);
                            if (!Torpi2.PeutPlacer(MaGrille, MaFlotte))
                                ListBateaux.Remove(Torpi2);
                        }
                        break;
                    case NomsBateau.SousMarin:
                        Bateau SM1 = null, SM2;
                        if (MaFlotte.QuantiteSousMarin == 0)
                        {
                            Console.WriteLine("Vous ne pouvez plus placer de bateau de ce type.");
                            break;
                        }

                        if (!ListBateaux.Contains(SM1))
                        {
                            MaFlotte.PlacerBateau(NomsBateau.SousMarin);
                            SM1 = new SousMarin(XBat, YBat, Orient);
                            ListBateaux.Add(SM1);
                            if (!SM1.PeutPlacer(MaGrille, MaFlotte))
                                ListBateaux.Remove(SM1);
                        }
                        else
                        {
                            MaFlotte.PlacerBateau(NomsBateau.SousMarin);
                            SM2 = new SousMarin(XBat, YBat, Orient);
                            ListBateaux.Add(SM2);
                            if (!SM2.PeutPlacer(MaGrille, MaFlotte))
                                ListBateaux.Remove(SM2);
                        }
                        break;
                }

                // Quand on a placé un bateau, on actualise la grille à afficher à l'écran.
                Console.WriteLine("Actualisation de la grille...");
                MaGrille.Draw();
            }
            // On réinitialise la flotte pour les calculs
            MaFlotte = new Flotte(ID);
        }

        public bool ResteBateau()
        {
            bool ResteBat = false;
            foreach (var bateau in ListBateaux)
            {
                if (bateau.Coule == false)
                {
                    ResteBat = true;
                    break;
                }
            }
            return ResteBat;
        }
    }
}
