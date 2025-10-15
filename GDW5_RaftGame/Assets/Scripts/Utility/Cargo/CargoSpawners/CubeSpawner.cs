using UnityEngine;

public class CubeSpawner : BaseSpawner
{
    [SerializeField] CubeCargo cargo;

    public override void Spawn(GameObject location)
    {
        Instantiate(cargo, location.transform);
    }
}
