using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform Target;
    [SerializeField] Vector3 offset;
    [SerializeField] float smoothing;

    Vector3 velocity = Vector3.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void LateUpdate()
    {
        // desired position with offset
        Vector3 targetPosition = Target.position + offset;

        // smoothly move the camera
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothing);

        // smooth rotation to look at the boat
        Quaternion targetRotation = Quaternion.LookRotation(Target.position - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 3f);
    }
}
