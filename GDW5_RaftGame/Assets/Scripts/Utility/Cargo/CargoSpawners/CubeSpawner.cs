using UnityEngine;

public class CubeSpawner : BaseSpawner
{
    public override void Spawn(GameObject location)
    {
        Instantiate(cargo, location.transform.position, location.transform.rotation);
    }
}
