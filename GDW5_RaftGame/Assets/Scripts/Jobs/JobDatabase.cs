using SDS_Locations;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Same Day Shipping/New Job Category", fileName = "Job Category")]
public class JobDatabase : ScriptableObject
{
    [SerializeField] List<JobObject> allJobs;

    Dictionary<Docks, List<JobObject>> jobsByStartDock;

    public IReadOnlyList<JobObject> GetJobsFromClosestDock()
    {
        return GetJobsFromDock(
            SDS_Locations.Locations.GetClosestIsland(BoatController.instance.transform.position)
            );
    }

    public IReadOnlyList<JobObject> GetJobsFromDock(Docks dock)
    {
        BuildDatabaseIfNeeded();
        if (jobsByStartDock.TryGetValue(dock, out var list)) return list;
        return System.Array.Empty<JobObject>();
    }

    /// <summary>
    /// builds the jobs by start dock dictionary.
    /// </summary>
    void BuildDatabaseIfNeeded()
    {
        if (jobsByStartDock != null) return; // exit if already built.

        jobsByStartDock = new Dictionary<Docks, List<JobObject>>();

        foreach (var job in allJobs)
        {
            if (!jobsByStartDock.TryGetValue(job.StartDock, out var list))
            {
                list = new List<JobObject>();
                jobsByStartDock.Add(job.StartDock, list);
            }
            list.Add(job);
        }
    }

}
