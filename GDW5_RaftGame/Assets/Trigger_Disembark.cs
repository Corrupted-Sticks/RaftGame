using UnityEngine;

public class Trigger_Disembark : MonoBehaviour
{
    int inTrigger = 0; // int that counts how many players are in the trigger, as a simple bool check would have issues with multiple players.

    bool isOnBoat = true; // start on boat.

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player")) inTrigger++;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) inTrigger--; 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && inTrigger >0) Disembark();

    }

    void Disembark()
    {
        isOnBoat = !isOnBoat; // togle on boat status, then move.
        FindFirstObjectByType<PlayerMovement>().transform.position = isOnBoat ? BoatController.instance.RespawnPosition.position : transform.position;
    }

}
