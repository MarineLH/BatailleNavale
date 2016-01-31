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
        public static String Number2String(int number, bool isCaps)
        {
            Char c = (Char)((isCaps ? 65 : 97) + (number - 1));
            return c.ToString();
        }

        static void Main(string[] args)
        {
            // bool peutPlacer = true;

            NomsBateau NomBat;
            string CodeDep;
            int XBat, YBat;
            Orientation Orient;

            Grille MaGrille = new Grille();

            Console.WriteLine("Création de la grille ...");
            MaGrille.Draw();

            Flotte MaFlotte = new Flotte();
            List<Bateau> listBateaux = new List<Bateau>();

            while (MaFlotte.total != 0 )
            {
                Console.WriteLine(MaFlotte.total);

                Console.WriteLine("Vous pouvez placer {0} porte-avions, {1} cuirrassé(s), {2} croiseur(s), {3} torpilleurs(s), {4} sous-marin(s).", MaFlotte.QuantitePA, MaFlotte.QuantiteCuir, MaFlotte.QuantiteCrois, MaFlotte.QuantiteTorpi, MaFlotte.QuantiteSousMarin);

                Console.WriteLine("Choisissez un bateau à placer : PorteAvions / Cuirrasse / Croiseur / Torpilleur / SousMarin");
                NomBat = (NomsBateau) System.Enum.Parse(typeof (NomsBateau), Console.ReadLine());
                Console.WriteLine("Entrez le point de départ du bateau. (ex : A1 / C5 ...)");
                CodeDep = Console.ReadLine();
                XBat = int.Parse(CodeDep.Substring(1));
                YBat = char.ToUpper(char.Parse(CodeDep.Substring(0, 1))) - 64;
                Console.WriteLine("{0}, {1} {2}", CodeDep, XBat, YBat);
                Console.WriteLine("Entrez son orientation : Nord / Sud / Est / Ouest");
                Orient = (Orientation) System.Enum.Parse(typeof (Orientation), Console.ReadLine());

                switch (NomBat)
                {
                    case NomsBateau.PorteAvions:
                        Bateau PA = new PorteAvions(XBat, YBat, Orient);
                        listBateaux.Add(PA);
                        if (!PA.PeutPlacer(MaGrille, MaFlotte))
                            listBateaux.Remove(PA);
                        break;
                    case NomsBateau.Cuirrasse:
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
                    case NomsBateau.Torpilleur:
                        Bateau Torpi1 = new Torpilleur(XBat, YBat, Orient);
                        listBateaux.Add(Torpi1);
                        if (!Torpi1.PeutPlacer(MaGrille, MaFlotte))
                            listBateaux.Remove(Torpi1);
                        //Bateau Torpi2 = new Torpilleur(XBat, YBat, Orient);
                        break;
                    case NomsBateau.SousMarin:
                        Bateau SM1 = new SousMarin(XBat, YBat, Orient);
                        listBateaux.Add(SM1);
                        if (!SM1.PeutPlacer(MaGrille, MaFlotte))
                            listBateaux.Remove(SM1);
                        //Bateau SM2 = new SousMarin(XBat, YBat, Orient);
                        break;
                }


                Console.WriteLine("Actualisation de la grille...");
                MaGrille.Draw();
            }

            Console.ReadKey();
        }
    }
}
