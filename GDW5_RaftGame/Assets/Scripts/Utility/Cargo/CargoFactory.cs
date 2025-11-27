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


    public void TEMPERARY_SpawnCargo() // TEMPERARY DEBUG: Replace with job specific cargo types later.
    {
        List<CARGO_TYPES> cargoOptions = new();
        for (int i = 0; i < 4; ++i)
        {
            cargoOptions.Add(
               (CARGO_TYPES)Random.Range(0, (int)CARGO_TYPES.COUNT) // gets random int from 0-cargo_type, then casts it back to a cargo type.
            );
        }
        foreach(var item in cargoOptions) { print(item); }
        SpawnCargo(cargoOptions);
    }
    public void SpawnCargo(List<CARGO_TYPES> cargo)
    {
        spawnInt = 0;

        foreach (CARGO_TYPES type in cargo)
        {
            if (spawnInt <= spawnLocations.Count)
            {
                poolDict[type].Spawn(spawnLocations[spawnInt]);

                //spawnDict[type].Spawn(spawnLocations[spawnInt]);
            }

            spawnInt++;
        }
    }
}
