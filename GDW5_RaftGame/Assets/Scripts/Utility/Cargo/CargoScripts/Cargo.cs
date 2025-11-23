using SDS_Jobs;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Cargo : CargoSubject
{
    public IObjectPool<Cargo> Pool { get; set; }

    public float currentHealth;

    [SerializeField] public float maxHealth = 100.0f;
    [SerializeField] public float timeToSelfDestruct = 30.0f;

    public JobManager jobManager;

    protected void Awake()
    {
        jobManager = FindFirstObjectByType<JobManager>();
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    protected void OnEnable()
    {
        Attach(jobManager);
    }

    protected void OnDisable()
    {
        Detach(jobManager);
        ResetCargo();
    }

    protected void Despawn()
    {
        ReturnToPool();
    }

    protected void ReturnToPool()
    {
        Pool.Release(this);
    }

    protected void ResetCargo()
    {
        currentHealth = maxHealth;
    }

    protected void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0) Despawn();
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CargoDeath"))
        {
            NotifyObservers();
            Despawn();
        }
    }
}