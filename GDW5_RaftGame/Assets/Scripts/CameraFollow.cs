using SDS_Locations;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform Target;
    [SerializeField] Vector3 zoomedOutOffset;
    [SerializeField] float smoothing;

    [SerializeField] Vector3 zoomedInOffset;

    Vector3 velocity = Vector3.zero;

    bool zoomedIn = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void LateUpdate()
    {
        if (zoomedIn)
        {
            // desired position with offset
            Vector3 targetPosition = Target.position
                           - Target.forward * zoomedInOffset.z
                           + Vector3.up * zoomedInOffset.y
                           + Target.right * zoomedInOffset.x;

            // smoothly move the camera
            //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothing);
            transform.position = targetPosition;

            // smooth rotation to look at the player
            Quaternion targetRotation = Quaternion.LookRotation(Target.position - transform.position, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation * Quaternion.Euler(Vector3.left * 20), Time.deltaTime);
        }
        else
        {
            // desired position with offset
            Vector3 targetPosition = Target.position
                           - Target.forward * zoomedOutOffset.z
                           + Vector3.up * zoomedOutOffset.y
                           + Target.right * zoomedOutOffset.x;

            // smoothly move the camera
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothing);

            // smooth rotation to look at the boat
            Quaternion targetRotation = Quaternion.LookRotation(Target.position - transform.position, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 3f);
        }
    }

    public void ToggleCamera()
    {
        zoomedIn = !zoomedIn;

        if (zoomedIn)
        {
            Target = FindFirstObjectByType<PlayerMovement>().gameObject.transform;
        }
        else
        {
            Target = FindFirstObjectByType<BoatController>().gameObject.transform;
        }
    }
}
