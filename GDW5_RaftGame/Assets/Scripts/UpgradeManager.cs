using SDS_Jobs;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{

    UpgradeSelection _upgradeSelection;


    public void SetSelection(UpgradeSelection selection)
    {
        _upgradeSelection = selection;
    }
    
    public void TryBuyUpgrade()
    {
        if (_upgradeSelection == null) return;
        if (JobManager.instance.CurrentMoney >= _upgradeSelection.Price)
        {
            JobManager.instance.CurrentMoney -= _upgradeSelection.Price;

            if (_upgradeSelection.upgradeType == BoatUpgrade.Handling) { SetBoatTurnRate(_upgradeSelection.upgradeMultiplier); }
            else
            {
                SetBoatSpeed(_upgradeSelection.upgradeMultiplier);
            }

            _upgradeSelection.GetComponent<Button>().interactable = false;


        }
    }

    public void SetBoatSpeed(float multiplier)
    {
        BoatController.instance.SetSpeedUpgrade(multiplier);
    }

    public void SetBoatTurnRate(float multiplier)
    {
        BoatController.instance.SetTurnUpgrade(multiplier);
    }
}
