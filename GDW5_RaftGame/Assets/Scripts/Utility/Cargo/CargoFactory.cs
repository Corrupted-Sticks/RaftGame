using System.Collections.Generic;
using UnityEngine;

public class CargoFactory : MonoBehaviour
{
    [SerializeField] List<BaseSpawner> spawns = new List<BaseSpawner>();
    [SerializeField] List<GameObject> spawnLocations = new List<GameObject>();

    private Dictionary<CARGO_TYPES, BaseSpawner> spawnDict = new();
    private Dictionary<CARGO_TYPES, CargoObjectPool> poolDict = new();

    int spawnInt = 0;

    private void Awake()
    {
        foreach (BaseSpawner bs in spawns)
        {
            spawnDict.Add(bs.Types, bs);
            poolDict.Add(bs.Types, gameObject.AddComponent<CargoObjectPool>());
            poolDict[bs.Types].SetSpawner(bs);
        }
    }

    public void sendTest()
    {
        SpawnCargo(new List<CARGO_TYPES> { CARGO_TYPES.Cube, CARGO_TYPES.Barrel, CARGO_TYPES.Stretch, CARGO_TYPES.Barrel });
    }

    public void SpawnCargo(List<CARGO_TYPES> cargo)
    {
        spawnInt = 0;

        foreach (CARGO_TYPES type in cargo)
        {
            if (spawnInt >= spawnLocations.Count)
            {
                poolDict[type].Spawn(spawnLocations[spawnInt]);

                //spawnDict[type].Spawn(spawnLocations[spawnInt]);
            }

            spawnInt++;
        }
    }
}
