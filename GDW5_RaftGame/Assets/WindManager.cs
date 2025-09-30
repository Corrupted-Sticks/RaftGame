using Sirenix.OdinInspector;
using UnityEngine;

public class WindManager : MonoBehaviour
{
    public static WindManager instance;

    /// <summary>
    /// Only positive. 0 = up ( +z on x/z plane)
    /// </summary>
    [FoldoutGroup("Wind Settings")][ShowInInspector][SerializeField] float _windAngle;

    public float WindAngle { 
        get { return _windAngle; } 
        set { 
            if (value >= 360) _windAngle = value % 360;
        }
    }
    public Vector2 WindDirection { get
        {
            float rad = _windAngle * Mathf.Deg2Rad;
            return new Vector2(Mathf.Sin(rad), Mathf.Cos(rad));

        }
    }

#if UNITY_EDITOR // remove in actual builds, as these are editor only variables.

    [FoldoutGroup("Wind Gizmo Settings")][SerializeField] float arrowLength = 2f;
    [FoldoutGroup("Wind Gizmo Settings")][SerializeField] float headLength = 0.5f;
    [FoldoutGroup("Wind Gizmo Settings")][SerializeField] float headAngle = 20f;
    [FoldoutGroup("Wind Gizmo Settings")][SerializeField] Color arrowColor = Color.cyan;
#endif

    private void Awake()
    {
        if (instance != null) Destroy(instance.gameObject);
        instance = this;
    }
    // auto removed when building.
    private void OnDrawGizmos()
    {
       
        if (WindDirection.sqrMagnitude <0)
            return;

        Gizmos.color = arrowColor;

        // Normalize to ensure consistent arrow size
        Vector3 dir = new Vector3(WindDirection.x, 0, WindDirection.y).normalized;

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
}
