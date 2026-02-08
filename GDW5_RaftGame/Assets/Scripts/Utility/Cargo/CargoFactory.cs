using System.Collections.Generic;
using UnityEngine;

public class CargoFactory : MonoBehaviour
{
    public static CargoFactory instance;
    [SerializeField] List<BaseSpawner> spawns = new List<BaseSpawner>();
    [SerializeField] List<GameObject> spawnLocations = new List<GameObject>();

    private Dictionary<CARGO_TYPES, BaseSpawner> spawnDict = new();

    int spawnInt = 0;

    private void Awake()
    {

        if(instance != null)Destroy(instance);
        instance = this;

        foreach (BaseSpawner bs in spawns)
        {
            spawnDict.Add(bs.Types, bs);
        }
    }


    public void SpawnCargo(List<CARGO_TYPES> cargo)
    {
        foreach (CARGO_TYPES type in cargo)
        {
            spawnDict[type].Spawn(spawnLocations[spawnInt % 4]);
            spawnInt++;
        }
    }
}
