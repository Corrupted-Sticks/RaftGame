using UnityEngine;
using TMPro;
using SDS_Locations;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;

    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI cargoText;
    [SerializeField] TextMeshProUGUI lostCargoText;
    [SerializeField] TextMeshProUGUI rewardText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI fromText;
    [SerializeField] TextMeshProUGUI toText;

    [SerializeField] GameObject cargoIcon;

    int lostCargo = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMoney(int num)
    {
        moneyText.text = num.ToString();
    }

    public void SetLostCargo()
    {
        lostCargo++;
        lostCargoText.text = lostCargo.ToString();
    }

    public void SendJobObj(JobObject jObj)
    {
        cargoIcon.SetActive(true);

        SetCargoText(jObj.CargoTypes.Count);
        SetRewardText(jObj.Reward);
        //SetTimeText(jObj.Time);
        SetFromText(jObj.StartDock);
        SetToText(jObj.EndDock);

        lostCargo = 0;
    }

    void SetCargoText(int num)
    {
        cargoText.text = num.ToString();
    }

    void SetRewardText(int num)
    {
        rewardText.text = "Reward: $" + num.ToString();
    }

    void SetTimeText(int time)
    {
        int minutes = time / 60;
        int seconds = time % 60;

        timeText.text = "Time: " + minutes.ToString() + ":" + seconds.ToString();
    }

    void SetFromText(Docks dock)
    {
        fromText.text = "From: " + Locations.GetIslandDisplayName(dock);
    }

    void SetToText(Docks dock)
    {
        toText.text = "To: " + Locations.GetIslandDisplayName(dock);
    }

    public void ResetHUDJob()
    {
        cargoText.text = "0";
        lostCargo = 0;
        lostCargoText.text = "0";
        rewardText.text = "Reward: $000000";
        timeText.text = "Time: 00:00";
        fromText.text = "From: N/A";
        toText.text = "To: N/A";
        cargoIcon.SetActive(false);
    }
}
