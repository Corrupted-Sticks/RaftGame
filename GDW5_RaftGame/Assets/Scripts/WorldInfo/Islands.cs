using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace SDS_Locations
{

    public enum Islands
    {
        Island1,
        Island2,
        Island3,
       /* Island4,
        Island5,
        Island6,
        Island7,
        Island8,*/
        COUNT// by keeping a count element at the end, we can do Islands.COUNT to get the number of elements in the enum.
    }

    /// <summary>
    /// holds information about important locations.
    /// </summary>
    public static class Locations
    {

        /// <summary>
        /// maps <see cref="Islands"/> to their world space coordinates.
        /// </summary>
        [ShowInInspector]
        [SerializeField]
        public static Dictionary<Islands, Vector3> IslandPositions = new Dictionary<Islands, Vector3>{
            {Islands.Island1, new Vector3(350,-50,350) },
            {Islands.Island2, new Vector3(-923,0,1416) },
            {Islands.Island3, new Vector3(-1380,0,58) },
            /*{Islands.Island4, new Vector3(4,0,0) },
            {Islands.Island5, new Vector3(5,0,0) },
            {Islands.Island6, new Vector3(6,0,0) },
            {Islands.Island7, new Vector3(7,0,0) },
            {Islands.Island8, new Vector3(8,0,0) },*/

        };

        public static Islands GetClosestIsland(Vector3 position)
        {
            Islands closestIsland = Islands.Island1;
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

        public static string GetIslandStr(Islands island) => island.ToString();


    }



}





