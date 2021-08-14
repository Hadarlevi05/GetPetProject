using System;
using System.Collections.Generic;
using System.Text;

namespace GetPet.Data.Enums
{
    /// <summary>
    /// Determinte if the pet came from an external site or uploaded in GetPet
    /// </summary>
    public enum PetSource
    {
        External,
        Internal,
        Spca, 
        Rla,
        RehovotSpa
    }
}
