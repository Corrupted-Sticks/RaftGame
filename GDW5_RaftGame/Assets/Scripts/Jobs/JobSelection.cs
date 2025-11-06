using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using SDS_Locations;
using SDS_Jobs;
using TMPro;
using UnityEngine.UI;

public class JobSelection : MonoBehaviour
{
    [SerializeField] Docks _startIsland;
    public Docks StartIsland { get => _startIsland; }

    [SerializeField] Docks _endIsland;
    public Docks EndIsland { get => _endIsland; }

    public Job Job { get => new Job(_startIsland, _endIsland); }

    TextMeshProUGUI _buttonText;


    int _reward = 0;
    public int Reward { get => _reward; }

    int _distance = 0;

    public int Distance { get => _distance; }


    Button _button;


    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(()=>JobManager.instance.ExecuteJobAction(transform));

        _buttonText = GetComponentInChildren<TextMeshProUGUI>();

        /*
        _startIsland = Locations.GetClosestIsland(BoatController.instance.transform.position); // always start at the island you are on when accepting the job.
        int end = Random.Range(0, (int)Docks.COUNT);
        int start = (int)_startIsland;

        while (end == start)// if start and end are the same island, keep getting a new end until it isn't. 
            end = Random.Range(0, (int)Docks.COUNT);

        _endIsland = (Docks)end;

        _buttonText.text = $"{_startIsland} ->{_endIsland}";

        _distance = Mathf.RoundToInt(Vector3.Distance(Locations.IslandPositions[_startIsland], Locations.IslandPositions[_endIsland]));

        _reward = Mathf.FloorToInt(_distance * 0.678f);// arbitrary modifier, later will make to change with difficulty.*/


    }

    public void UpdateInfo(Docks startDock, Docks endDock)
    {

        _buttonText = GetComponentInChildren<TextMeshProUGUI>();

        _startIsland = startDock; // always start at the island you are on when accepting the job.

        _endIsland = endDock;

        _buttonText.text = $"{_startIsland} ->{_endIsland}";

        _distance = Mathf.RoundToInt(Vector3.Distance(Locations.IslandPositions[_startIsland], Locations.IslandPositions[_endIsland]));

        _reward = Mathf.FloorToInt(_distance * 0.678f);// arbitrary modifier, later will make to change with difficulty.
    }


}
