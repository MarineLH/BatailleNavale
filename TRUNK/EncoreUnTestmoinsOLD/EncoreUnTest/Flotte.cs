using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoreUnTest
{
    public class Flotte
    {
        public NumJoueur IdJoueur { get; set; }
        public int QuantitePA { get; set; }
        public int QuantiteCuir { get; set; }
        public int QuantiteCrois { get; set; }
        public int QuantiteTorpi { get; set; }
        public int QuantiteSousMarin { get; set; }
        public int total { get; private set; }

        public Flotte(NumJoueur _num)
        {
            IdJoueur = _num;
            QuantitePA = 1;
            QuantiteCuir = 1;
            QuantiteCrois = 1;
            QuantiteTorpi = 2;
            QuantiteSousMarin = 2;
            total = QuantitePA + QuantiteCuir + QuantiteCrois + QuantiteTorpi + QuantiteSousMarin;
        }
        /* La flotte détermine le nombre de vaisseaux à placer par un joueur.
        Chaque joueur a droit à un nombre limité de bateaux. A chaque nouveau placement,
        il faut décrémenter la quantité de ce type de bateau.*/
        public void PlacerBateau(NomsBateau _nom)
        {
            switch (_nom)
            {
                case NomsBateau.PorteAvions:
                    if (QuantitePA > 0)
                        QuantitePA--;
                    break;
                case NomsBateau.Cuirassé:
                    if (QuantiteCuir > 0)
                        QuantiteCuir--;
                    break;
                case NomsBateau.Croiseur:
                    if (QuantiteCrois > 0)
                        QuantiteCrois--;
                    break;
                case NomsBateau.Torpilleur:
                    if (QuantiteTorpi > 0)
                        QuantiteTorpi--;
                    break;
                case NomsBateau.SousMarin:
                    if (QuantiteSousMarin > 0)
                        QuantiteSousMarin--;
                    break;
            }
            total = QuantitePA + QuantiteCuir + QuantiteCrois + QuantiteTorpi + QuantiteSousMarin;
        }
    }
}
