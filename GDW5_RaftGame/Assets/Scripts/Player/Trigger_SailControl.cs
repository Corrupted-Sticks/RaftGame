using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class Trigger_SailControl : MonoBehaviour
{
    [SerializeField] int playersInside = 0;
    //[SerializeField]PlayerMovement pmove; // current working playerMovement script.
    [SerializeField] PlayerMovement pmove; // current working playerMovement script.

    [SerializeField] bool isPlayerControlling = false;

    [SerializeField] UI_BoatControl boatControllUI;

    [SerializeField] CameraFollow cFollow;

    bool tutorialDone = false;

    Vector3 playerLocal = Vector3.zero;

    private void Update()
    {
        if (pmove == null || playersInside <= 0) return;

        //DEBUG : rn if both players entered this trigger, and one of them pressed E/esc, it would effect whoever entered the trigger most recently.

        // DEBUG : REPLACE WITH INPUT SYSTEM
        if (!isPlayerControlling && Input.GetKeyDown(KeyCode.E))
        {
            TakeControl();
            cFollow.ToggleCamera();
            AudioManager.instance.PlaySFX(2); //Sail sfx
        }
        // DEBUG : REPLACE WITH INPUT SYSTEM
        else if (isPlayerControlling && Input.GetKeyDown(KeyCode.E)) 
        {
            ExitControl();
            cFollow.ToggleCamera();
            AudioManager.instance.PlaySFX(3); //Anchor sfx
        }

        if (pmove != null && isPlayerControlling)
        {
            pmove.gameObject.transform.localPosition = playerLocal;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.CompareTag("Player")) return;
        if (!isPlayerControlling) pmove = collision.GetComponentInParent<PlayerMovement>();
        if (collision.GetComponentInParent<PlayerMovement>() == pmove && playersInside != 0) return;
        playersInside++;

        if (!tutorialDone)
        {
            boatControllUI.SetUIImage(true);
            boatControllUI.SetEnterBoatControl(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playersInside--;
        if (isPlayerControlling && playersInside == 0) playersInside++; 
        if (playersInside <= 0) boatControllUI.SetEnterBoatControl(false);
    }

    void TakeControl()
    {
        BoatController.instance.ToggleInput(true);
        pmove.ToggleInput(false);
        isPlayerControlling = true;
        pmove.transform.SetParent(transform.root);
        playerLocal = pmove.gameObject.transform.localPosition;
        //pmove.RB.isKinematic = true;
        //pmove.Collider.enabled = false;

        if (!tutorialDone)
        {
            boatControllUI.SetExitBoatControl(true);
            boatControllUI.SetEnterBoatControl(false);
        }
    }

    public void ExitControl()
    {
        BoatController.instance.ToggleInput(false);
        pmove.ToggleInput(true);
        pmove.enabled = true;
        isPlayerControlling = false;
        pmove.transform.SetParent(null);
        //pmove.RB.isKinematic = false;
        //pmove.Collider.enabled = true;

        boatControllUI.SetExitBoatControl(false);
        boatControllUI.SetUIImage(false);

        tutorialDone = true;

        if (playersInside <= 0)
            pmove = null;
        //else
            //boatControllUI.SetEnterBoatControl(true);
    }
}
