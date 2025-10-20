using Unity.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BuoyantBody3D : MonoBehaviour
{

    public Rigidbody rb;

    public bool isUnderWater = false;

    /// <summary>
    /// maximum depth for full float power to be applied.
    /// </summary>
    public float maxSubmersion = 5;
    /// <summary>
    /// how much force to apply per pontoon.
    /// </summary>
    public float floatPower = 1;

    public float waterDrag = 1;
    public float waterAngularDrag = 1;

    public float airDrag = 0;
    public float airAngularDrag = 0.05f;

    [SerializeField] Transform[] pontoons;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearDamping = airDrag;
        rb.angularDamping = airAngularDrag;
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        CheckPontoon();
    }

    void CheckPontoon()
    {
        bool underWater = false;
        if (pontoons.Length <= 0) return;
        foreach (Transform pontoon in pontoons)
        {
            float submersion = WaveManager.instance.GetWaterHeight(pontoon.position) - pontoon.position.y;
            if (submersion > 0)
            {
                float force = Mathf.Clamp01(submersion / maxSubmersion) * floatPower / pontoons.Length;
                rb.AddForceAtPosition(Vector3.up * force, pontoon.position, ForceMode.Acceleration);
                underWater = true;
            }
        }


        if(underWater != isUnderWater) // only update the state if we change from under to above, or above to under.
        {
            isUnderWater = underWater;
            if (isUnderWater) OnWaterEnter();
            else OnWaterExit();
        }

    }

    /// <summary>
    /// called when a pontoon is below the water surface, signifying the object is now (partially) underwater.
    /// </summary>
    void OnWaterEnter()
    {
        rb.linearDamping = waterDrag;
        rb.angularDamping = waterAngularDrag;
    }

    /// <summary>
    /// called when all pontoons are above the water surface.
    /// </summary>
    void OnWaterExit()
    {
        rb.linearDamping = airDrag;
        rb.angularDamping = airAngularDrag;
    }

}
