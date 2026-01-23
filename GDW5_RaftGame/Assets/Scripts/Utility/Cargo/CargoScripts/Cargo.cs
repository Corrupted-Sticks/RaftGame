using SDS_Jobs;
using System.Collections;
using UnityEngine;

public abstract class Cargo : CargoSubject
{
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
        Attach(jobManager);
    }

    protected void OnEnable()
    {
        Attach(jobManager);
    }

    protected void OnDestroy()
    {
        Detach(jobManager);
    }

    protected void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            NotifyObservers();
            Destroy(gameObject);
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CargoDeath"))
        {
            NotifyObservers();
            Destroy(gameObject);
        }
    }
}