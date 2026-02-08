using System.Collections.Generic;
using UnityEngine;
using SDS_Locations;

[CreateAssetMenu(menuName = "Same Day Shipping/New Job Object", fileName = "Job Object")]
public class JobObject : ScriptableObject
{
    [SerializeField] private string npcName;

    public string NPCName => npcName;

    [SerializeField] private List<CARGO_TYPES> cargo;

    public List<CARGO_TYPES> CargoTypes => cargo;


    [SerializeField] private int reward;

    public int Reward =>reward;



    [Tooltip("time limit in seconds")]
    [SerializeField] private int time;
    public int Time => time;


    [TextArea(minLines:3, maxLines:8)]
    [SerializeField] private string description;
    public string Description => description;



    [SerializeField] private Docks startDock;
    public Docks StartDock => startDock;



    [SerializeField] private Docks endDock;
    public Docks EndDock => endDock;



}