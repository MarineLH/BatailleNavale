using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoreUnTest
{
    public class Bateau
    {
        public Orientation O { get; set; }
        public NomsBateau Nom { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Taille { get; set; }
        public int[] Plage { get; set; }

        // Vérifier que toutes les cases sur lesquelles se trouve le bateau sont disponibles.
        public bool PeutPlacer(Grille _grille, Flotte _flotte)
        {
            bool peutPlacer = true;
            foreach (var cell in Plage)
            {
                if (O == Orientation.Est || O == Orientation.Ouest)
                {
                    if (!_grille.grille[Y, cell].PeutPlacer()) // Les lignes et les colonnes sont dans l'autre sens dans les tableaux C# (donc Y à la place du X)
                    {
                        peutPlacer = false;
                        break;
                    }
                }
                else
                {
                    if (!_grille.grille[cell, X].PeutPlacer())
                    { 
                        peutPlacer = false;
                        break;
                    }
                }
            }
            if (peutPlacer)
            {
                if (O == Orientation.Est || O == Orientation.Ouest)
                {
                    foreach (var Case in Plage)
                    {
                        _grille.grille[Y, Case].ChangerEtat(EtatCase.Bateau);
                    }
                }
                else
                {
                    foreach (var Case in Plage)
                    {
                        _grille.grille[Case, X].ChangerEtat(EtatCase.Bateau);
                    }
                }
                _flotte.PlacerBateau(Nom);
                EauInaccessible(_grille);
            }
            else
            {
                Console.WriteLine("Impossible de placer le bateau. Suppression ...");
            }         
            return peutPlacer;
        }

        // Détermine le contour du bateau pour rendre cette zone inaccessible lors du placement des autres bateaux.
        public void EauInaccessible(Grille _grille)
        {
            // Les calculs changent en fonction de l'orientation du bateau.
            int PosXEauInacDebut, PosXEauInacFin, PosYEauInacDebut, PosYEauInacFin, x, y;
            if (O == Orientation.Est)
            {
                PosXEauInacDebut = X - 1;
                PosXEauInacFin = X + Taille;
                PosYEauInacDebut = Y - 1;
                PosYEauInacFin = Y + 1;

                y = PosYEauInacDebut;
                while (y <= PosYEauInacFin)
                {
                    x = PosXEauInacDebut;
                    while (x <= PosXEauInacFin)
                    {
                        if (_grille.grille[y, x].Etat == EtatCase.Eau)
                        {
                            _grille.grille[y, x].ChangerEtat(EtatCase.EauInaccessible);
                        }
                        x++;
                    }
                    y++;
                }
            }
            else if (O == Orientation.Ouest)
            {
                PosXEauInacDebut = X - Taille;
                PosXEauInacFin = X + 1;
                PosYEauInacDebut = Y - 1;
                PosYEauInacFin = Y + 1;

                y = PosYEauInacDebut;
                while (y <= PosYEauInacFin)
                {
                    x = PosXEauInacDebut;
                    while (x <= PosXEauInacFin)
                    {
                        if (_grille.grille[y, x].Etat == EtatCase.Eau)
                        {
                            _grille.grille[y, x].ChangerEtat(EtatCase.EauInaccessible);
                        }
                        x++;
                    }
                    y++;
                }
            }
            else if (O == Orientation.Nord)
            {
                PosXEauInacDebut = X - 1;
                PosXEauInacFin = X + 1;
                PosYEauInacDebut = Y - Taille;
                PosYEauInacFin = Y + 1;

                y = PosYEauInacDebut;
                while (y <= PosYEauInacFin)
                {
                    x = PosXEauInacDebut;
                    while (x <= PosXEauInacFin)
                    {
                        if (_grille.grille[y, x].Etat == EtatCase.Eau)
                        {
                            _grille.grille[y, x].ChangerEtat(EtatCase.EauInaccessible);
                        }
                        x++;
                    }
                    y++;
                }
            }
            else // Si orientation == Sud
            {
                PosXEauInacDebut = X - 1;
                PosXEauInacFin = X + 1;
                PosYEauInacDebut = Y - 1;
                PosYEauInacFin = Y + Taille;

                y = PosYEauInacDebut;
                while (y <= PosYEauInacFin)
                {
                    x = PosXEauInacDebut;
                    while (x <= PosXEauInacFin)
                    {
                        if (_grille.grille[y, x].Etat == EtatCase.Eau)
                        {
                            _grille.grille[y, x].ChangerEtat(EtatCase.EauInaccessible);
                        }
                        x++;
                    }
                    y++;
                }
            }
        }
    }

    // Définition de toutes les sous-classes :

    public class PorteAvions : Bateau
    {
        public PorteAvions(int _x, int _y, Orientation _or)
        {
            X = _x;
            Y = _y;
            O = _or;
            Nom = NomsBateau.PorteAvions;
            Taille = 5;
            Plage = new int[Taille];
            for (int i = 0; i < Taille; i++)
            {
                if (O == Orientation.Est)
                {
                    Plage[i] = X + i;
                }
                else if (O == Orientation.Ouest)
                {
                    Plage[i] = X - i;
                }
                else if (O == Orientation.Sud)
                {
                    Plage[i] = Y + i;
                }
                else
                {
                    Plage[i] = Y - i;
                }
            }
        }
    }

    public class Cuirrasse : Bateau
    {
        public Cuirrasse(int _x, int _y, Orientation _or)
        {
            X = _x;
            Y = _y;
            O = _or;
            Nom = NomsBateau.Cuirrasse;
            Taille = 4;
            Plage = new int[Taille];
            for (int i = 0; i < Taille; i++)
            {
                if (O == Orientation.Est)
                    Plage[i] = X + i;
                else if (O == Orientation.Ouest)
                    Plage[i] = X - i;
                else if (O == Orientation.Sud)
                    Plage[i] = Y + i;
                else
                    Plage[i] = Y - i;
            }
        }
    }

    public class Croiseur : Bateau
    {
        public Croiseur(int _x, int _y, Orientation _or)
        {
            X = _x;
            Y = _y;
            O = _or;
            Nom = NomsBateau.Croiseur;
            Taille = 3;
            Plage = new int[Taille];
            for (int i = 0; i < Taille; i++)
            {
                if (O == Orientation.Est)
                    Plage[i] = X + i;
                else if (O == Orientation.Ouest)
                    Plage[i] = X - i;
                else if (O == Orientation.Sud)
                    Plage[i] = Y + i;
                else
                    Plage[i] = Y - i;
            }
        }
    }

    public class Torpilleur : Bateau
    {
        public Torpilleur(int _x, int _y, Orientation _or)
        {
            X = _x;
            Y = _y;
            O = _or;
            Nom = NomsBateau.Torpilleur;
            Taille = 2;
            Plage = new int[Taille];
            for (int i = 0; i < Taille; i++)
            {
                if (O == Orientation.Est)
                    Plage[i] = X + i;
                else if (O == Orientation.Ouest)
                    Plage[i] = X - i;
                else if (O == Orientation.Sud)
                    Plage[i] = Y + i;
                else
                    Plage[i] = Y - i;
            }
        }
    }

    public class SousMarin : Bateau
    {
        public SousMarin(int _x, int _y, Orientation _or)
        {
            X = _x;
            Y = _y;
            O = _or;
            Nom = NomsBateau.SousMarin;
            Taille = 1;
            Plage = new int[Taille];
            for (int i = 0; i < Taille; i++)
            {
                if (O == Orientation.Est)
                    Plage[i] = X + i;
                else if (O == Orientation.Ouest)
                    Plage[i] = X - i;
                else if (O == Orientation.Sud)
                    Plage[i] = Y + i;
                else
                    Plage[i] = Y - i;
            }
        }
    }
}
