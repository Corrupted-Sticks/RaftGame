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


    [SerializeField] float minPitch = -30f;
    [SerializeField] float maxPitch = 60f;

    private void Start()
    {
        _camera = Camera.main;
    }
    Vector3 currentOffset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void LateUpdate()
    {
        Vector3 targetPosition = Target.position;
        transform.position = targetPosition;

        Vector3 dir = Target.position - transform.position;

        if (dir.sqrMagnitude > 0.0001f)
        {
            Quaternion look = Quaternion.LookRotation(dir, Vector3.up);

            Vector3 euler = look.eulerAngles;
            euler.x = ClampAngle(euler.x, minPitch, maxPitch);

            transform.rotation = Quaternion.Euler(euler);
        }
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle > 180f) angle -= 360f;
        return Mathf.Clamp(angle, min, max);
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
