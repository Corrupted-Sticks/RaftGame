using UnityEngine;

public class Trigger_ExitBoat : MonoBehaviour
{
    bool inTrigger = false; // DEBUG modify to account for multiple players later.
    public Transform GetOffPosition; // Make assignable from elsewhere so multiple docks would work.

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        inTrigger = true;



    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        inTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(inTrigger && Input.GetKeyDown(KeyCode.E))
        {
            FindObjectOfType<PlayerMovement>().transform.position = GetOffPosition.position;
        }
    }
}
