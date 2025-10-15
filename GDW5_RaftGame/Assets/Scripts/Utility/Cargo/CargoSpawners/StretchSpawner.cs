using UnityEngine;

public class StretchSpawner : BaseSpawner
{
    [SerializeField] StretchCargo cargo;

    public override void Spawn(GameObject location)
    {
        Instantiate(cargo, location.transform);
    }
}
