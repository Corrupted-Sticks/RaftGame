using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class Trigger_SailControl : MonoBehaviour
{
    public int playersInside = 0;
    //[SerializeField]PlayerMovement pmove; // current working playerMovement script.
    [SerializeField] PlayerMovement _pmove; // current working playerMovement script.

    public bool isPlayerControlling = false;

    [SerializeField] UI_BoatControl boatControllUI;

    public CameraFollow cFollow;

    bool tutorialDone = false;

    Vector3 playerLocal = Vector3.zero;

   

    private void Update()
    {
        if (_pmove != null && isPlayerControlling)
        {
            _pmove.gameObject.transform.localPosition = playerLocal;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.CompareTag("Player")) return;
        if (!isPlayerControlling) _pmove = collision.GetComponentInParent<PlayerMovement>();
        if (collision.GetComponentInParent<PlayerMovement>() == _pmove && playersInside != 0) return;
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

    public void TakeControl(PlayerMovement pmove)
    {
        BoatController.instance.ToggleInput(true);
        _pmove = pmove;
        _pmove.canMove = false;
        isPlayerControlling = true;
        _pmove.transform.SetParent(transform.root);
        playerLocal = _pmove.gameObject.transform.localPosition;
        cFollow.ToggleCamera();
        AudioManager.instance.PlaySFX(3); //Anchor sfx

        //pmove.RB.isKinematic = true;
        //pmove.Collider.enabled = false;

        if (!tutorialDone)
        {
            boatControllUI.SetExitBoatControl(true);
            boatControllUI.SetEnterBoatControl(false);
        }
    }

    public void ExitControl(PlayerMovement pmove)
    {
        BoatController.instance.ToggleInput(false);
        _pmove.canMove = true;

        isPlayerControlling = false;
        _pmove.transform.SetParent(null);
        cFollow.ToggleCamera();
        AudioManager.instance.PlaySFX(2); //Sail sfx
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
