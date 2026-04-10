using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace SDS_Locations
{
    public enum Docks
    {
        DewsburyN,
        DewsburyS,
        NamelessIsleN,
        NamelessIsleS,
        NamelessIsleW,
        MawN,
        MawE,
        MawW,
        WhirlpoolPeak,
        PiratesRefuge,
        Outcoast,
        COUNT// by keeping a count element at the end, we can do Docks.COUNT to get the number of elements in the enum.
    }

    /// <summary>
    /// holds information about important locations.
    /// </summary>
    public static class Locations
    {

        /// <summary>
        /// maps <see cref="Docks"/> to their world space coordinates.
        /// </summary>
        [ShowInInspector]
        [SerializeField]
        public static Dictionary<Docks, Vector3> IslandPositions = new Dictionary<Docks, Vector3>{
            {Docks.DewsburyN, new Vector3(-219,20,-150) },
            {Docks.DewsburyS, new Vector3(-329,20,-262) },
            {Docks.NamelessIsleN, new Vector3(-578,20, 30) },
            {Docks.NamelessIsleS, new Vector3(-510,20,-148) },
            {Docks.NamelessIsleW, new Vector3(-707,20,-26) },
            {Docks.MawN, new Vector3(-165,20,348) },
            {Docks.MawE, new  Vector3(-190,20,3)},
            {Docks.MawW, new Vector3(-239,20,80)},
            {Docks.WhirlpoolPeak, new Vector3(-238,20,457) },
            {Docks.PiratesRefuge, new  Vector3(-20,20,-107)},
            {Docks.Outcoast, new Vector3(130,20, 320) },

        };

        public static Docks GetClosestIsland(Vector3 position)
        {
            Docks closestIsland = Docks.DewsburyS;
            float closestDistance = float.MaxValue;

            foreach (var pair in IslandPositions)
            {
                float distance = Vector3.Distance(position, pair.Value);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestIsland = pair.Key;
                }
            }

            return closestIsland;
        }

        public static string GetIslandRawStr(Docks island) => island.ToString();
        public static string GetIslandDisplayName(Docks island)
        {
            switch (island)
            {
                case Docks.DewsburyS:
                    return "Dewsbury (South)";
                case Docks.NamelessIsleW :
                    return "The Namless Isle (West)";
                case Docks.MawN :
                    return "The Maw (North)";
                case Docks.WhirlpoolPeak :
                    return "Whirlpool Peak";
                case Docks.MawW :
                    return "The Maw (West)";
                case Docks.NamelessIsleS :
                    return "The Nameless Isle (South)";
                case Docks.DewsburyN :
                    return "Dewsbury (North)";
                case Docks.PiratesRefuge:
                    return "Pirate's Refuge";
                case Docks.MawE:
                    return "The Maw (East)";
                case Docks.NamelessIsleN :
                    return "The Nameless Isle (North)";
                case Docks.Outcoast :
                    return "Outcoast";
                default:
                    return " Unknown Dock:  " + island.ToString();

            }
        }


    }



}





