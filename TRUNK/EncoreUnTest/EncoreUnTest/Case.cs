using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EncoreUnTest
{
    public class Case
    {
        public string Code { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public EtatCase Etat { get; set; }
        public string Symbole { get; set; }

        public Case(int _x, int _y)
        {
            
            Code = string.Format("{0}{1}", Program.Number2String(_y, true), _x);
            X = _x;
            Y = _y;
            if (X == 0 || Y == 0 || X == 11 || Y == 11)
            {
                Etat = EtatCase.Inaccessible;
                Symbole = "0";
            }
            else
            {
                Etat = EtatCase.Eau;
                Symbole = "-";
            }

        }
        public Case(string _code)
        {
            Code = _code;
            X = int.Parse(Code.Substring(1, 1));
            Y = char.ToUpper(char.Parse(Code.Substring(0, 1))) - 64;

            if (X == 0 || Y == 0 || X == 11 || Y == 11)
            {
                Etat = EtatCase.Inaccessible;
                Symbole = "0";
            }
            else
            {
                Etat = EtatCase.Eau;
                Symbole = "-";
            }
        }

        public bool PeutPlacer()
        {
            switch (Etat)
            {
                case EtatCase.Eau:
                    return true;
                case EtatCase.Bateau:
                    return false;
                case EtatCase.Inaccessible:
                    return false;
                case EtatCase.EauInaccessible:
                    return false;
                default:
                    return true;
            }
        }

        public void Tirer()
        {
            switch (Etat)
            {
                case EtatCase.Eau:
                    Etat = EtatCase.TirRate;
                    Console.WriteLine("Vous avez tiré dans l'eau.");
                    break;
                case EtatCase.TirRate:
                    Console.WriteLine("Vous avez déjà tiré ici !");
                    break;
                case EtatCase.Bateau:
                    Etat = EtatCase.BateauTouche;
                    Console.WriteLine("Vous avez touché un bateau !");
                    break;
                case EtatCase.BateauTouche:
                    Console.WriteLine("Vous avez déjà touché un bateau ici.");
                    break;
                case EtatCase.Inaccessible:
                    Console.WriteLine("Case inaccessible");
                    break;
                case EtatCase.EauInaccessible:
                    Etat = EtatCase.TirRate;
                    Console.WriteLine("Vous avez tiré dans l'eau.");
                    break;
            }
        }

        public void ChangerEtat(EtatCase _Etat)
        {
            Etat = _Etat;
            switch (Etat)
            {
                case EtatCase.Eau:
                    Symbole = "-";
                    break;
                case EtatCase.TirRate:
                    Symbole = "o";
                    break;
                case EtatCase.Bateau:
                    Symbole = "s";
                    break;
                case EtatCase.BateauTouche:
                    Symbole = "X";
                    break;
                case EtatCase.Inaccessible:
                    Symbole = "0";
                    break;
                case EtatCase.EauInaccessible:
                    Symbole = "T";
                    break;
            }
        }
    }
}
