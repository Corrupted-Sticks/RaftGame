using UnityEngine;
using UnityEngine.Pool;

public class CargoObjectPool : MonoBehaviour
{
    public int maxPoolSize = 10;
    public int stackDefaultCapacity = 10;
    BaseSpawner cargoSpawner;
    GameObject local;

    public IObjectPool<Cargo> Pool
    {
        get
        {
            if (_pool == null)
            {
                _pool = new ObjectPool<Cargo>(
                    createFunc: CreateItem, 
                    actionOnGet: OnGet, 
                    actionOnRelease: OnRelease, 
                    actionOnDestroy: OnDestroyItem,
                    collectionCheck: true,
                    defaultCapacity: 10,
                    maxSize: 4
                );
            }
            return _pool;
        }
    }

    private IObjectPool<Cargo> _pool;

    public void SetSpawner(BaseSpawner spawner)
    {
        cargoSpawner = spawner;
    }

    private Cargo CreateItem()
    {
        cargoSpawner.Spawn(local);

        cargoSpawner.cargo.Pool = Pool;

        return cargoSpawner.cargo;
    }

    private void OnGet(Cargo cargo)
    {
        cargo.gameObject.SetActive(true);
    }

    private void OnRelease(Cargo cargo)
    {
        cargo.gameObject.SetActive(false);
    }

    private void OnDestroyItem(Cargo cargo)
    {
        Destroy(cargo.gameObject);
    }

    public void Spawn(GameObject location)
    {
        local = location;

        var cargo = Pool.Get();

        cargo.transform.position = local.transform.position;
        cargo.transform.rotation = local.transform.rotation;
    }
}
