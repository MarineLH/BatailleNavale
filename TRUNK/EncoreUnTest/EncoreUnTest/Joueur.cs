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
        public List<Bateau> listBateaux { get; set; }
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
            listBateaux = new List<Bateau>();

            while (MaFlotte.total != 0)
            {
                Console.WriteLine("Vous pouvez placer {0} porte-avions, {1} cuirrassé(s), {2} croiseur(s), {3} torpilleurs(s), {4} sous-marin(s).", MaFlotte.QuantitePA, MaFlotte.QuantiteCuir, MaFlotte.QuantiteCrois, MaFlotte.QuantiteTorpi, MaFlotte.QuantiteSousMarin);
                Console.WriteLine("Choisissez un bateau à placer : PorteAvions / Cuirrasse / Croiseur / Torpilleur / SousMarin");
                while (!Enum.TryParse(Console.ReadLine(), out NomBat)) // Tant que ce qu'on écrit n'est pas un nom de bateau, demander à écrire un nom de bateau.
                {
                    Console.WriteLine("Attention à l'orthographe ! Vérifiez que vous avez écrit le nom du bateau comme les exemples ci-dessus !");
                }
                Console.WriteLine("Entrez la poupe (l'arrière) du bateau. (ex : A1 / C5 ...)");
                CodeDep = Console.ReadLine();
                while (!Program.IDCases.Contains(CodeDep)) // Tant que ce qu'on écrit n'est pas un code de case, demander à écrire un code de case.
                {
                    Console.WriteLine("Veuillez entrer une valeur crédible.");
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
                
                // En fonction du nom de bateau, on va instancier une sous-classe précise.
                switch (NomBat)
                {
                    case NomsBateau.PorteAvions:
                        Bateau PA = new PorteAvions(XBat, YBat, Orient);    // On l'instancie.
                        listBateaux.Add(PA);                                // On l'ajoute à la liste de nos bateaux.
                        if (!PA.PeutPlacer(MaGrille, MaFlotte))             // Mais s'il s'avère qu'il est impossible de le placer...
                            listBateaux.Remove(PA);                         // On le retire de la liste.
                        break;
                    case NomsBateau.Cuirrasse:                              // Idem pour tous les autres cas.
                        Bateau Cuir = new Cuirrasse(XBat, YBat, Orient);
                        listBateaux.Add(Cuir);
                        if (!Cuir.PeutPlacer(MaGrille, MaFlotte))
                            listBateaux.Remove(Cuir);
                        break;
                    case NomsBateau.Croiseur:
                        Bateau Crois = new Croiseur(XBat, YBat, Orient);
                        listBateaux.Add(Crois);
                        if (!Crois.PeutPlacer(MaGrille, MaFlotte))
                            listBateaux.Remove(Crois);
                        break;
                    case NomsBateau.Torpilleur:                             // Cas particulier : le Torpi et les SM ont sont dispos DEUX fois. Il faut différencier
                        Bateau Torpi1 = null, Torpi2;                       // les instanciations.
                        if (!listBateaux.Contains(Torpi1))
                        {
                            Torpi1 = new Torpilleur(XBat, YBat, Orient);
                            listBateaux.Add(Torpi1);
                            if (!Torpi1.PeutPlacer(MaGrille, MaFlotte))
                                listBateaux.Remove(Torpi1);
                        }
                        else
                        {
                            Torpi2 = new Torpilleur(XBat, YBat, Orient);
                            listBateaux.Add(Torpi2);
                            if (!Torpi2.PeutPlacer(MaGrille, MaFlotte))
                                listBateaux.Remove(Torpi2);
                        }
                        break;
                    case NomsBateau.SousMarin:
                        Bateau SM1 = null, SM2;
                        if (!listBateaux.Contains(SM1))
                        {
                            SM1 = new SousMarin(XBat, YBat, Orient);
                            listBateaux.Add(SM1);
                            if (!SM1.PeutPlacer(MaGrille, MaFlotte))
                                listBateaux.Remove(SM1);
                        }
                        else
                        {
                            SM2 = new SousMarin(XBat, YBat, Orient);
                            listBateaux.Add(SM2);
                            if (!SM2.PeutPlacer(MaGrille, MaFlotte))
                                listBateaux.Remove(SM2);
                        }
                        break;
                }

                // Quand on a placé un bateau, on actualise la grille à afficher à l'écran.
                Console.WriteLine("Actualisation de la grille...");
                MaGrille.Draw();
            }           
        }
    }
}
