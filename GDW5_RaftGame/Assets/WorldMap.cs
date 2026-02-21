using UnityEngine;

public class WorldMap : MonoBehaviour
{
    [Header("World bounds")]
    public Vector2 worldMin;
    public Vector2 worldMax;

    [Header("References")]
    public RectTransform mapRect;
    public RectTransform playerIcon;
    public RectTransform waypointIcon;

    public Transform player;
    public Transform waypoint;

    Vector2 mapSize;

    void Awake()
    {
        if (!mapRect)
            mapRect = (RectTransform)transform;
    }

    void Start()
    {
        mapSize = mapRect.rect.size;
    }

    void LateUpdate()
    {
        if (player)
            playerIcon.anchoredPosition = WorldToMap(player.position);

        if (waypoint)
            waypointIcon.anchoredPosition = WorldToMap(waypoint.position);
    }

    Vector2 WorldToMap(Vector3 worldPos)
    {
        float nx = Mathf.InverseLerp(worldMin.x, worldMax.x, worldPos.x);
        float ny = Mathf.InverseLerp(worldMin.y, worldMax.y, worldPos.z);

        return new Vector2(
            (nx - 0.5f) * mapSize.x,
            (ny - 0.5f) * mapSize.y
        );
    }
}