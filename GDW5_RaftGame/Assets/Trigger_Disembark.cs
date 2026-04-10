using UnityEngine;

public class Trigger_Disembark : MonoBehaviour
{

    UI_BoatControl Tutorial;
    [SerializeField] bool inTrigger = false; // int that counts how many players are in the trigger, as a simple bool check would have issues with multiple players.

    [SerializeField] bool isOnBoat = true; // start on boat.

    private void OnTriggerStay(Collider other)
    {
        if ( !(other.CompareTag("Player") || other.CompareTag("Boat"))) return;
        Tutorial.SetDockBoatControl(true);
        Tutorial.SetUIImage(true);
        inTrigger = true;
    }

    private void OnTriggerExit(Collider other) {

        Tutorial.SetDockBoatControl(false);
        Tutorial.SetUIImage(false);
        inTrigger = false;
    }

    private void Start() {
        Tutorial = FindAnyObjectByType<UI_BoatControl>();
    }
    private void Update()
    {
        if (
            Input.GetKeyDown(KeyCode.F) && inTrigger
            ) Disembark();

    }

    void Disembark()
    {

        PlayerMovement player = FindFirstObjectByType<PlayerMovement>();
        if (!isOnBoat)
        {
            player.HipBone.position = BoatController.instance.transform.position;
            player.transform.SetParent(null);
            inTrigger = false;
        }
        else
        {
            player.HipBone.position = transform.position;
            player.transform.SetParent(BoatController.instance.transform);
        }

        isOnBoat = !isOnBoat; // togle on boat status
    }

}
