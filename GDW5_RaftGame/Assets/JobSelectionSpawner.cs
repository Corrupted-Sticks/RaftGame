using SDS_Jobs;
using System.Collections.Generic;
using UnityEngine;
using SDS_Locations;

public class JobSelectionSpawner : MonoBehaviour
{
    public static JobSelectionSpawner instance;

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

        JobManager.instance.ClearUndoStack();
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
        int quantity = Random.Range(3, 9);

        Docks startDock = Locations.GetClosestIsland(BoatController.instance.transform.position);
        HashSet<int> islandIndecies = new();

        while (islandIndecies.Count < quantity)
        {
            islandIndecies.Add(Random.Range(0, (int)Docks.COUNT));
        }

        foreach (int i in islandIndecies)
        {
            var selection = GetPooled();
            selection.transform.SetParent(transform, false);
            selection.UpdateInfo(startDock, (Docks)i);
        }
    }
}
