using SDS_Locations;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform Target;
    [SerializeField] Vector3 zoomedOutOffset;
    [SerializeField] float smoothing;

    [SerializeField] Vector3 zoomedInOffset;

    Camera _camera;

    Vector3 velocity = Vector3.zero;

    bool zoomedIn = true;

    private void Start()
    {
        _camera = Camera.main;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void LateUpdate()
    {
        if (zoomedIn)
        {
            // desired position with offset
            Vector3 targetPosition = Target.position;

            // smoothly move the camera
            //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothing);
            transform.position = targetPosition;
        }
        else
        {
            // desired position with offset
            Vector3 targetPosition = Target.position;

            // smoothly move the camera
            transform.position = targetPosition;

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

            Vector3 cameraPosition = transform.position
                           - transform.forward * zoomedInOffset.z
                           + Vector3.up * zoomedInOffset.y
                           + transform.right * zoomedInOffset.x;
            _camera.transform.position = cameraPosition;
        }
        else
        {
            Target = FindFirstObjectByType<BoatController>().gameObject.transform;

            Vector3 cameraPosition = transform.position
                           - transform.forward * zoomedOutOffset.z
                           + Vector3.up * zoomedOutOffset.y
                           + transform.right * zoomedOutOffset.x;
            _camera.transform.position = cameraPosition;
        }

        transform.position = Target.position;
    }
}
