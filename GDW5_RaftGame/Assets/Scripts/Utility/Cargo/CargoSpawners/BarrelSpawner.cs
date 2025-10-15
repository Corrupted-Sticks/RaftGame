using UnityEngine;

public class BarrelSpawner : BaseSpawner
{
    [SerializeField] BarrelCargo cargo;

    public override void Spawn(GameObject location)
    {
        Instantiate(cargo, location.transform);
    }
}
