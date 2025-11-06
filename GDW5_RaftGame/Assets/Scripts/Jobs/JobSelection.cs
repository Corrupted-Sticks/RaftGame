using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using SDS_Locations;
using SDS_Jobs;
using TMPro;

public class JobSelection : MonoBehaviour
{
    [SerializeField] Islands _startIsland;
    public Islands StartIsland { get => _startIsland; }

    [SerializeField] Islands _endIsland;
    public Islands EndIsland { get => _endIsland; }

    public Job Job { get => new Job(_startIsland, _endIsland); }

    TextMeshProUGUI _buttonText;


    int _reward = 0;
    public int Reward { get => _reward; }

    int _distance = 0;

    public int Distance { get => _distance; }



    private void Start()
    {

        _buttonText = GetComponentInChildren<TextMeshProUGUI>();


        _startIsland = Locations.GetClosestIsland(BoatController.instance.transform.position); // always start at the island you are on when accepting the job.
        int end = Random.Range(0, (int)Islands.COUNT);
        int start = (int)_startIsland;

        while (end == start)// if start and end are the same island, keep getting a new end until it isn't. 
            end = Random.Range(0, (int)Islands.COUNT);

        _endIsland = (Islands)end;

        _buttonText.text = $"{_startIsland} ->{_endIsland}";

        _distance = Mathf.RoundToInt(Vector3.Distance(Locations.IslandPositions[_startIsland], Locations.IslandPositions[_endIsland]));

        _reward = Mathf.FloorToInt(_distance * 0.678f);// arbitrary modifier, later will make to change with difficulty.


    }


}
