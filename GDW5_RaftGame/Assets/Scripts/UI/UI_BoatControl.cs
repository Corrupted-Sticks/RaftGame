using UnityEngine;

public class UI_BoatControl : MonoBehaviour
{
    [SerializeField] GameObject EnterBoatControl;
    [SerializeField] GameObject ExitBoatControl;


    public void SetEnterBoatControl(bool value) => EnterBoatControl.SetActive(value);
    public void SetExitBoatControl(bool value) => ExitBoatControl.SetActive(value);

}
