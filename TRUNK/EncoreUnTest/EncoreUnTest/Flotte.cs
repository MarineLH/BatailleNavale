using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoreUnTest
{
    public class Flotte
    {
        public int QuantitePA { get; set; }
        public int QuantiteCuir { get; set; }
        public int QuantiteCrois { get; set; }
        public int QuantiteTorpi { get; set; }
        public int QuantiteSousMarin { get; set; }
        public int total { get; private set; }

        public Flotte()
        {
            QuantitePA = 1;
            QuantiteCuir = 1;
            QuantiteCrois = 1;
            QuantiteTorpi = 2;
            QuantiteSousMarin = 2;
            total = QuantitePA + QuantiteCuir + QuantiteCrois + QuantiteTorpi + QuantiteSousMarin;
        }

        public void PlacerBateau(NomsBateau _nom)
        {
            switch (_nom)
            {
                case NomsBateau.PorteAvions:
                    if (QuantitePA > 0)
                        this.QuantitePA--;
                    break;
                case NomsBateau.Cuirrasse:
                    if (QuantiteCuir > 0)
                        this.QuantiteCuir--;
                    break;
                case NomsBateau.Croiseur:
                    if (QuantiteCrois > 0)
                        this.QuantiteCrois--;
                    break;
                case NomsBateau.Torpilleur:
                    if (QuantiteTorpi > 0)
                        this.QuantiteTorpi--;
                    break;
                case NomsBateau.SousMarin:
                    if (QuantiteSousMarin > 0)
                        this.QuantiteSousMarin--;
                    break;
                default:
                    Console.WriteLine("Erreur, vous n'avez plus ce type de bateau disponible.");
                    break;
            }
            total = QuantitePA + QuantiteCuir + QuantiteCrois + QuantiteTorpi + QuantiteSousMarin;
        }
    }
}
