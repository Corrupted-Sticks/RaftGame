using SDS_Jobs;
using System.Collections.Generic;
using UnityEngine;

using SDS_Locations;
using Unity.VisualScripting;

public class JobSelectionSpawner : MonoBehaviour
{

    public static JobSelectionSpawner instance;
    [SerializeField] GameObject _jobSelectionPrefab;

    [SerializeField] List<JobSelection> _pool = new();

    const int minQuant = 3;
    const int maxQuant = 6;

    private void Awake()
    {
        if (instance != null) Destroy(instance.gameObject);
        instance = this;

    }


    private void Start()
    {
        // initialize pool.
        for (int i = 0; i < maxQuant; i++)
        {
            var obj = Instantiate(_jobSelectionPrefab, transform);
            obj.SetActive(false);
            _pool.Add(obj.GetComponent<JobSelection>());
    }
    }


    public void ClearCurrentOptions()
    {
        foreach(JobSelection js in _pool) js.gameObject.SetActive(false);
        JobManager.instance.ClearUndoStack();
    }

    public void CreateRandomQuantity()
    {
        int quantity = Random.Range(minQuant, maxQuant);
        Docks startDock = Locations.GetClosestIsland(BoatController.instance.transform.position);
        HashSet<int> islandIndecies = new();



        for (int i = 0; i < quantity; i++)
        {
            if (i > (int)Docks.COUNT-1 || i >= _pool.Count) break; // if more 
            int targetDock = Random.Range(0, (int)Docks.COUNT);
            
            while (!islandIndecies.Add(targetDock)) // continue generating and trying to add random numbers to the hashset until one is not taken.
            {
                targetDock = Random.Range(0, (int)Docks.COUNT);
            }
            AssignJobInfo(_pool[i], startDock,  (Docks)targetDock);


        }

    }


    void AssignJobInfo(JobSelection js, Docks start, Docks target)
    {
        js.gameObject.SetActive(true);
        js.UpdateInfo(start, target);

    }
}
