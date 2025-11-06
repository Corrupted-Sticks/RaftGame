using SDS_Jobs;
using System.Collections.Generic;
using UnityEngine;

using SDS_Locations;
public class JobSelectionSpawner : MonoBehaviour
{

    public static JobSelectionSpawner instance;
    [SerializeField] GameObject _jobSelectionPrefab;

    private void Awake()
    {
        if (instance != null) Destroy(instance.gameObject);
        instance = this;
        
    }

    public void ClearCurrentOptions()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        JobManager.instance.ClearUndoStack();
    }

    public void CreateRandomQuantity()
    {
        int quantity = Random.Range(3, 9);
        Docks startDock = Locations.GetClosestIsland(BoatController.instance.transform.position);
        HashSet<int> islandIndecies = new();


        while (islandIndecies.Count < quantity)
            islandIndecies.Add(Random.Range(0, (int)Docks.COUNT));

        foreach (int i in islandIndecies)
            Instantiate(_jobSelectionPrefab, transform).GetComponent<JobSelection>().UpdateInfo(startDock, (Docks)i);

    }
}
