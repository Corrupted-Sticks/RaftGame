using Unity.VisualScripting;
using UnityEngine;

public class Trigger_ChangeBoatSettings : MonoBehaviour
{
    // [SerializeField] float newAcceleration;
    [SerializeField] float newAngularDrag;
    float oldAngularDrag;
    BuoyantBody3D boat;
    int playersEntered= 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            boat = BoatController.instance.transform.root.GetComponent<BuoyantBody3D>();
            oldAngularDrag = boat.waterAngularDrag;
            boat.waterAngularDrag = newAngularDrag;
            boat.rb.angularDamping = newAngularDrag;
            playersEntered++;
            BoatController.instance.ToggleDock(true);


        }
    }



    private void OnTriggerExit(Collider other)
    {
        playersEntered--;
        if(playersEntered == 0)
        {
            boat.waterAngularDrag = oldAngularDrag;
            boat.rb.angularDamping = oldAngularDrag;
            boat = null;
            BoatController.instance.ToggleDock(false);
        }
    }

    
}
