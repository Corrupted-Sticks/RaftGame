using UnityEngine;


public enum BoatUpgrade
{
    Sail1,
    Sail2,
    Handling,
}
public class UpgradeSelection : MonoBehaviour
{
    public int Price;
    public BoatUpgrade upgradeType;
    public float upgradeMultiplier;
}
