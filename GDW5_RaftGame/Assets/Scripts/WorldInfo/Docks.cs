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
            {Docks.Dock1, new Vector3(-329,20,-262) },
            {Docks.Dock2, new Vector3(-440,20,281) },
            {Docks.Dock3, new Vector3(-707,20,-26) },
            {Docks.Dock4, new Vector3(-165,20,348) },
            {Docks.Dock5, new Vector3(-493,20,489) },
            {Docks.Dock6, new Vector3(-736,20,383)},
            {Docks.Dock7, new Vector3(-238,20,457) },
            {Docks.Dock8, new Vector3(-401,20,511) },
            {Docks.Dock9, new Vector3(-328,20,-269)},
            {Docks.Dock10, new Vector3(-510,20,-148) },
            {Docks.Dock11, new Vector3(-219,20,-150) },
            {Docks.Dock12, new  Vector3(-20,20,-107)},
            {Docks.Dock13, new  Vector3(-235,20,-82)},
            {Docks.Dock14, new Vector3(-570,20,-130) },

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





