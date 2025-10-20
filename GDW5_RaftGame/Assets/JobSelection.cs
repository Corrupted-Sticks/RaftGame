using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using SDS_Locations;

public class JobSelection : MonoBehaviour
{
    [SerializeField] Islands _startIsland;
    public Islands StartIsland { get => _startIsland; }

    [SerializeField] Islands _endIsland;
    public Islands EndIsland { get => _endIsland; }

    public Job Job {get => new Job(_startIsland, _endIsland); }


    private void Start()
    {
        int start = Random.Range(0, (int)Islands.COUNT-1); // -1 because the COUNT element would be included otherwise.
        int end   = Random.Range(0, (int)Islands.COUNT-1);

        while (end == start)// if start and end are the same island, keep getting a new end until it isn't. 
            end = Random.Range(0, (int)Islands.COUNT-1);


        // once we have the random values, cast back to islands enum
        _startIsland = (Islands)start;
        _endIsland = (Islands)end;
    }

  
}
