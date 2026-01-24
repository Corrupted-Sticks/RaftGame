using SDS_Locations;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform Target;
    [SerializeField] Vector3 zoomedOutOffset;
    [SerializeField] float smoothing;

    [SerializeField] Vector3 zoomedInOffset;

    Camera _camera = Camera.main;

    Vector3 velocity = Vector3.zero;

    bool zoomedIn = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void LateUpdate()
    {
        if (zoomedIn)
        {
            // desired position with offset
            Vector3 targetPosition = Target.position + new Vector3(0,3f,0);

            Vector3 cameraPosition = Target.position
                           - Target.forward * zoomedInOffset.z
                           + Vector3.up * zoomedInOffset.y
                           + Target.right * zoomedInOffset.x;

            // smoothly move the camera
            //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothing);
            transform.position = targetPosition;
            _camera.transform.position = Vector3.SmoothDamp(_camera.transform.position, cameraPosition, ref velocity, smoothing);
        }
        else
        {
            Vector3 targetPosition = Target.position;
            Debug.Log("Hi!");
            // desired position with offset
            Vector3 cameraPosition = Target.position
                           - Target.forward * zoomedOutOffset.z
                           + Vector3.up * zoomedOutOffset.y
                           + Target.right * zoomedOutOffset.x;

            // smoothly move the camera
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothing);
            _camera.transform.position = Vector3.SmoothDamp(_camera.transform.position, cameraPosition, ref velocity, smoothing);
            
            // smooth rotation to look at the boat
            //Quaternion targetRotation = Quaternion.LookRotation(Target.position - transform.position, Vector3.up);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 3f);
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
