using SDS_Jobs;
using System.Collections.Generic;
using UnityEngine;
using SDS_Locations;

public class JobSelectionSpawner : MonoBehaviour
{
    public static JobSelectionSpawner instance;

    [SerializeField] JobDatabase jobDatabase;

    [SerializeField] private GameObject _jobSelectionPrefab;

    private readonly Queue<JobSelection> _pool = new(); 

    private void Awake()
    {
        if (instance != null) Destroy(instance.gameObject);
        instance = this;
    }

    /// <summary>
    /// returns active options back into the pool.
    /// </summary>
    public void ClearCurrentOptions()
    {
        foreach (Transform child in transform)
        {
            var sel = child.GetComponent<JobSelection>();
            if (sel != null)
            {
                sel.gameObject.SetActive(false);
                sel.transform.SetParent(transform, false);
                _pool.Enqueue(sel);
            }
        }
    }

    /// <summary>
    /// retrieves object from the pool or makes new one.
    /// </summary>
    private JobSelection GetPooled()
    {
        if (_pool.Count > 0)
        {
            var pooled = _pool.Dequeue();
            pooled.gameObject.SetActive(true);
            return pooled;
        }

        // Create new only when needed
        return Instantiate(_jobSelectionPrefab).GetComponent<JobSelection>();
    }

    /// <summary>
    /// creates random number of job selections from pooled objects.
    /// </summary>
    public void CreateRandomQuantity()
    {

        var jobs = jobDatabase.GetJobsFromClosestDock();

        if (jobs == null) { { Debug.LogError("jobs null"); return; } }
        if (jobs.Count <= 0) { Debug.LogError("No jobs found for closest dock"); return; }
        int quantity = Random.Range(1, jobs.Count);

        
        HashSet<JobObject> islandIndecies = new();

        while (islandIndecies.Count < quantity)
        {
            islandIndecies.Add(jobs[Random.Range(0,jobs.Count)]);
        }

        foreach (JobObject obj in islandIndecies)
        {
            var selection = GetPooled();
            selection.transform.SetParent(transform, false);
            selection.UpdateInfo(obj);
        }
    }
}
