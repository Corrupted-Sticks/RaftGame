using UnityEngine;

public class UI_BoatControl : MonoBehaviour
{
    [SerializeField] GameObject EnterBoatControl;
    [SerializeField] GameObject ExitBoatControl;
    [SerializeField] GameObject DockBoatControl;
    [SerializeField] GameObject uiImage;

    public void SetEnterBoatControl(bool value) => EnterBoatControl.SetActive(value);
    public void SetExitBoatControl(bool value) => ExitBoatControl.SetActive(value);
    public void SetDockBoatControl(bool value) {
        if (EnterBoatControl.activeSelf || ExitBoatControl.activeSelf) return;
        DockBoatControl.SetActive(value);
    }
    public void SetUIImage(bool value) => uiImage.SetActive(value);
}
