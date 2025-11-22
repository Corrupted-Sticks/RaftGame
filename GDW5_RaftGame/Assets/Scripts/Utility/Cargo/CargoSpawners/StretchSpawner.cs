using UnityEngine;

public class StretchSpawner : BaseSpawner
{
    public override void Spawn(GameObject location)
    {
        Instantiate(cargo, location.transform.position, location.transform.rotation);
    }
}
