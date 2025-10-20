using UnityEngine;
using TMPro;

public class JobWaypoint : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI distanceText;

    [SerializeField] Transform directionArrow;
    [SerializeField] Transform directionArrowOrigin;
    [SerializeField] float directionArrowDistance = 3;

    public void UpdateWaypoint()
    {
        distanceText.text = GetDistance().ToString("0.###");
        UpdateDirectionArrow();
    }


    void UpdateDirectionArrow()
    {
        Vector3 sailPos = directionArrowOrigin.position;
        Vector3 waypointPos = transform.position;
        float storedSailY = sailPos.y; // we store the Y value of the sail position, as we will set to 0.
        sailPos.y = 0; // setting both y pos to 0 makes the direction arrow only move and rotate around the xz plane.
        waypointPos.y = 0;

        Vector3 direction = (waypointPos - sailPos).normalized;
        sailPos.y = storedSailY;// revert sail y pos, as we use it for the position offset.

        directionArrow.position = sailPos + (directionArrowDistance * direction);
        directionArrow.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0f, 0f, 90f); // 2nd quaternion is cuz the arrow would improperly rotate.
    }



    public float GetDistance()
    {
        Vector3 boatPos = BoatController.instance.transform.position;
        Vector3 waypointPos = transform.position;
        boatPos.y = 0;
        waypointPos.y = 0;
        return Vector3.Distance(waypointPos, boatPos);
    }
}
