using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Cargo : MonoBehaviour
{
    public IObjectPool<Cargo> Pool { get; set; }

    public float currentHealth;

    [SerializeField] public float maxHealth = 100.0f;
    [SerializeField] public float timeToSelfDestruct = 30.0f;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void OnEnable()
    {

    }

    public void OnDisable()
    {
        ResetCargo();
    }

    public void Despawn()
    {
        ReturnToPool();
    }

    public void ReturnToPool()
    {
        Pool.Release(this);
    }

    public void ResetCargo()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0) ReturnToPool();
    }
}
