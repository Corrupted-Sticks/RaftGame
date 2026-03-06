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
        if (jObj == null) return;
        cargoIcon.SetActive(true);
        rewardText.gameObject.SetActive(true);

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
        rewardText.text = "+$" + num.ToString();
    }

    void SetTimeText(int time)
    {
        int minutes = time / 60;
        int seconds = time % 60;

        timeText.text = minutes.ToString() + ":" + seconds.ToString();
    }

    void SetFromText(Docks dock)
    {
        fromText.text = Locations.GetIslandDisplayName(dock);
    }

    void SetToText(Docks dock)
    {
        toText.text = Locations.GetIslandDisplayName(dock);
    }

    public void ResetHUDJob()
    {
        cargoText.text = "0";
        lostCargo = 0;
        lostCargoText.text = "0";
        rewardText.text = "+$000000";
        timeText.text = "00:00";
        fromText.text = "???";
        toText.text = "???";
        cargoIcon.SetActive(false);
        rewardText.gameObject.SetActive(false);
    }
}
