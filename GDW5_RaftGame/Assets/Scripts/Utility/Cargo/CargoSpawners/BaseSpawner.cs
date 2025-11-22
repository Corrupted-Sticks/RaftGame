using UnityEngine;

public abstract class BaseSpawner : MonoBehaviour
{
    [SerializeField] CARGO_TYPES types;
    public CARGO_TYPES Types { get { return types; } }
    public Cargo cargo;
    public abstract void Spawn(GameObject location);
}
