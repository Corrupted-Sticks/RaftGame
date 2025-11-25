using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class Trigger_SailControl : MonoBehaviour
{
    [SerializeField]int playersInside = 0;
    //[SerializeField]PlayerMovement pmove; // current working playerMovement script.
    [SerializeField]PlayerCMovement pmove; // current working playerMovement script.

    [SerializeField] bool isPlayerControlling = false;

    [SerializeField]UI_BoatControl boatControllUI;
    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.CompareTag("Player")) return;
        playersInside++;
        // pmove = collision.GetComponentInChildren<PlayerMovement>();
        pmove = collision.GetComponentInChildren<PlayerCMovement>();

        boatControllUI.SetEnterBoatControl(true);


    }

    private void Update()
    {
        if (pmove == null || playersInside <= 0) return;

        //DEBUG : rn if both players entered this trigger, and one of them pressed E/esc, it would effect whoever entered the trigger most recently.


        // DEBUG : REPLACE WITH INPUT SYSTEM
        if (!isPlayerControlling && Input.GetKeyDown(KeyCode.E)) TakeControl();

        // DEBUG : REPLACE WITH INPUT SYSTEM
        else if (isPlayerControlling && Input.GetKeyDown(KeyCode.E)) ExitControl();
    }


    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playersInside--;
        if (playersInside <= 0) boatControllUI.SetEnterBoatControl(false);
    }


    void TakeControl()
    {
        BoatController.instance.ToggleInput(true);
        pmove.ToggleInput(false);
        isPlayerControlling = true;
        pmove.transform.SetParent(transform.root);
        pmove.RB.isKinematic = true;
        pmove.Collider.enabled = false;
        boatControllUI.SetExitBoatControl(true);
        boatControllUI.SetEnterBoatControl(false);
    }

    public void ExitControl()
    {
        BoatController.instance.ToggleInput(false);
        pmove.ToggleInput(true);
        pmove.enabled = true;
        isPlayerControlling = false;
        pmove.transform.SetParent(null);
        pmove.RB.isKinematic = false;
        pmove.Collider.enabled = true;

        boatControllUI.SetExitBoatControl(false);

        if (playersInside <= 0)
            pmove = null;
        else
            boatControllUI.SetEnterBoatControl(true);


    }

}
