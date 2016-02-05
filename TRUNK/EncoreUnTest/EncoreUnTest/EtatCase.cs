using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoreUnTest
{
    // Simple énumération des différents états d'une case.
    public enum EtatCase
    {
        Eau,
        TirRate,
        TirRateAlready,
        Bateau,
        BateauTouche,
        BateauToucheAlready,
        Inaccessible,
        EauInaccessible,
        Coulé
    }
}
