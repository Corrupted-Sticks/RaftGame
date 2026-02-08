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

    TextMeshProUGUI _buttonText;


    int _reward = 0;
    public int Reward { get => _reward; }

    int _distance = 0;

    public int Distance { get => _distance; }


    Button _button;

    JobObject _job;



    private void Start()
    {
        _button = GetComponent<Button>();
        
        _button.onClick.AddListener(OnSelect);

        _buttonText = GetComponentInChildren<TextMeshProUGUI>();

    }


    public void OnSelect()
    {
        JobManager.instance.OnJobSelect(_button, _job);
    }
    public void UpdateInfo(JobObject job)
    {

        _buttonText = GetComponentInChildren<TextMeshProUGUI>();

        _startIsland = job.StartDock; // always start at the island you are on when accepting the job.

        _endIsland = job.EndDock;

        _buttonText.text = $"{Locations.GetIslandDisplayName(_endIsland)}";

        _distance = Mathf.RoundToInt(Vector3.Distance(Locations.IslandPositions[_startIsland], Locations.IslandPositions[_endIsland]));

        _reward = job.Reward;

        _job = job;
    }


}
