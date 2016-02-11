namespace NavalStrike
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
            
            Code = string.Format("{0}{1}", Program.Number2String(_y, true), _x); // Transforme les abscisses et les ordonnées en un code composé d'une lettre et d'un nombre
            X = _x;
            Y = _y;
            // La grille est définie en 12 x 12. Les première et dernière lignes, ainsi que les première et dernière colonnes, sont inaccessibles.
            if (X == 0 || Y == 0 || X == 11 || Y == 11)
            {
                Etat = EtatCase.Inaccessible;
                Symbole = " ";
            }
            else // Lors de l'initialisation, toutes les cases n'ont que de l'eau.
            {
                Etat = EtatCase.Eau;
                Symbole = "-";
            }

        }       

        // Vérifier l'état de la case pour déterminer si l'on peut placer un bateau dessus ou non.
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

        // Méthode appelée lorsqu'un joueur effectue un tir. En fonction de l'état de la case, on va retourner le nouvel état de cette case.
        public EtatCase Tirer()
        {
            switch (Etat)
            {
                case EtatCase.Eau:
                    Etat = EtatCase.TirRate;
                    break;
                case EtatCase.TirRate:
                    Etat = EtatCase.TirRateAlready;
                    break;
                case EtatCase.Bateau:
                    Etat = EtatCase.BateauTouche;
                    break;
                case EtatCase.BateauTouche:
                    Etat = EtatCase.BateauToucheAlready;
                    break;
                case EtatCase.Coulé:
                    Etat = EtatCase.BateauToucheAlready;
                    break;
                case EtatCase.EauInaccessible:
                    Etat = EtatCase.TirRate;
                    break;
            }
            return Etat;
        }

        // Méthode appelée après un tir, ou bien après un placement de bateau. Elle doit être utilisée plutôt que le set(EtatBateau) 
        // car elle donne le symbole à afficher sur la grille.
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
                case EtatCase.Coulé:
                    Symbole = "#";
                    break;
                case EtatCase.Inaccessible:
                    Symbole = " ";
                    break;
                case EtatCase.EauInaccessible:
                    Symbole = "-";
                    break;
            }
        }
    }
}
