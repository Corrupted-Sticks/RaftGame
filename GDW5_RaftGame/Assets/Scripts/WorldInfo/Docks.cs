using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace SDS_Locations
{

    public enum Docks
    {
        Dock1,
        Dock2,
        Dock3,
        Dock4,
        Dock5,
        Dock6,
        Dock7,
        Dock8,
        Dock9,
        Dock10,
        Dock11,
        Dock12,
        Dock13,
        Dock14,
        Dock15,
        Dock16,
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
            {Docks.Dock1, new Vector3(-130,28,-450) },
            {Docks.Dock2, new Vector3(-240,28,132) },
            {Docks.Dock3, new Vector3(-507,28,-148) },
            {Docks.Dock4, new Vector3(34,28,171) },
            {Docks.Dock5, new Vector3(10,28,-155) },
            {Docks.Dock6, new Vector3(-303,28,318) },
            {Docks.Dock7, new Vector3(-520,28,222)},
            {Docks.Dock8, new Vector3(-31,28,300) },
            {Docks.Dock9, new Vector3(-191,28,360) },
            {Docks.Dock10, new Vector3(-366,28,270)},
            {Docks.Dock11, new Vector3(325,28,155) },
            {Docks.Dock12, new Vector3(-310,28,-301) },
            {Docks.Dock13, new Vector3(-18,28,-328) },
            {Docks.Dock14, new  Vector3(186,28,-275)},
            {Docks.Dock15, new  Vector3(-28,28,-76)},
            {Docks.Dock16, new Vector3(-386,28,-130) },

        };

        public static Docks GetClosestIsland(Vector3 position)
        {
            Docks closestIsland = Docks.Dock1;
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

        public static string GetIslandStr(Docks island) => island.ToString();


    }



}





