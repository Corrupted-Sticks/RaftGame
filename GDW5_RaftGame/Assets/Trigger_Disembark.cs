using UnityEngine;

public class Trigger_Disembark : MonoBehaviour
{
    [SerializeField] bool inTrigger = false; // int that counts how many players are in the trigger, as a simple bool check would have issues with multiple players.

    [SerializeField] bool isOnBoat = true; // start on boat.

    private void OnTriggerStay(Collider other)
    {

        inTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inTrigger = false;
    }

    private void Update()
    {
        if (
            Input.GetKeyDown(KeyCode.F) && inTrigger
            ) Disembark();

    }

    void Disembark()
    {

        Transform player = FindFirstObjectByType<PlayerMovement>().transform;
        if (!isOnBoat)
        {
            player.position = BoatController.instance.transform.position;
            inTrigger = false;
        }
        else player.position = transform.position;

        isOnBoat = !isOnBoat; // togle on boat status
    }

}
