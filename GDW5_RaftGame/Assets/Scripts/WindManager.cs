using SDS_Weather;
using Sirenix.OdinInspector;
using UnityEngine;

public class WindManager : MonoBehaviour
{
    public static WindManager instance;

    /// <summary>
    /// Only positive. 0 = up ( +z on x/z plane)
    /// </summary>
    [FoldoutGroup("Wind Settings")][ShowInInspector][SerializeField] float _windAngle;

    [SerializeField] Transform _windDirectionArrow;
    [SerializeField] Transform _windDirectionArrowOrigin;
    [SerializeField] Transform _windEffect;
    [SerializeField] float _windDirectionArrowDistance = 5;

    public float WindAngle
    {
        get { return _windAngle; }
        set
        {
            _windAngle = value;
            if (_windAngle >= 360)  _windAngle %= 360;
            UpdateWindDirectionArrow();
        }
    }
    public Vector3 WindDirection
    {
        get
        {
            float rad = _windAngle * Mathf.Deg2Rad;
            return new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad)).normalized;

        }
    }

//#if UNITY_EDITOR // remove in actual builds, as these are editor only variables.

    [FoldoutGroup("Wind Gizmo Settings")][SerializeField] float arrowLength = 2f;
    [FoldoutGroup("Wind Gizmo Settings")][SerializeField] float headLength = 0.5f;
    [FoldoutGroup("Wind Gizmo Settings")][SerializeField] float headAngle = 20f;
    [FoldoutGroup("Wind Gizmo Settings")][SerializeField] Color arrowColor = Color.cyan;
//#endif

    private void Awake()
    {
        if (instance != null) Destroy(instance.gameObject);
        instance = this;
    }
    // auto removed when building.
    private void OnDrawGizmos()
    {

        if (WindDirection.sqrMagnitude < 0)
            return;

        Gizmos.color = arrowColor;

        // Normalize to ensure consistent arrow size
        Vector3 dir = WindDirection.normalized;

        // Arrow base and tip
        Vector3 start = transform.position;
        Vector3 end = start + dir * arrowLength;

        Gizmos.DrawLine(start, end);

        // Draw arrow head
        Vector3 right = Quaternion.LookRotation(dir) * Quaternion.Euler(0, 180 + headAngle, 0) * Vector3.forward;
        Vector3 left = Quaternion.LookRotation(dir) * Quaternion.Euler(0, 180 - headAngle, 0) * Vector3.forward;

        Gizmos.DrawLine(end, end + right * headLength);
        Gizmos.DrawLine(end, end + left * headLength);
    }
    private void Start() => WeatherManager.instance.onWeatherChanged.AddListener(UpdateWindDirectionArrow);


    public void UpdateWindDirectionArrow(WeatherInfo info) { WindAngle = info.windDirection;  Debug.Log("called");}
    private void FixedUpdate()
    {
        UpdateWindDirectionArrow();
        UpdateWindEffect();
    }

    void UpdateWindDirectionArrow()
    {
        Vector3 originPos = _windDirectionArrowOrigin.position;
        Vector3 waypointPos = transform.position;
        float storedOriginY = originPos.y; // we store the Y value of the sail position, as we will set to 0.
        originPos.y = 0; // setting both y pos to 0 makes the direction arrow only move and rotate around the xz plane.
        waypointPos.y = 0;

        Vector3 direction = WindDirection;
        originPos.y = storedOriginY;// revert sail y pos, as we use it for the position offset.

        _windDirectionArrow.position = originPos + (_windDirectionArrowDistance * direction);
        _windDirectionArrow.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0f, 0f, 90f); // 2nd quaternion is cuz the arrow would improperly rotate.
    }

    void UpdateWindEffect()
    {
        _windEffect.rotation = Quaternion.LookRotation(WindDirection);
    }

}
