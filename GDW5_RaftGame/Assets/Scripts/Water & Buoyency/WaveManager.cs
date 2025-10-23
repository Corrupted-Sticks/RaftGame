using UnityEngine;
using Unity.Mathematics;
using System;
using Sirenix.OdinInspector;
using System.Runtime.InteropServices;

[System.Serializable]
public struct WaterShaderSettings
{
    // frag shader
    float visualWaveDepth;


    // vertex shader
    float phase;
    float waveDepth;
    float gravity;
    WaveData[] waves;

}




[System.Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct WaveData
{
    public Vector3DLL direction;
    public float amplitude;
    public float timeScale;


    public WaveData(float timeScale, float amp, Vector3 dir)
    {
        this.timeScale = timeScale;
        amplitude = amp;
        direction = new Vector3DLL(dir);
    }
}

[System.Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct Vector3DLL
{
    public float x, y, z;

    public Vector3DLL(Vector3 v)
    {
        x = v.x;
        y = v.y;
        z = v.z;
    }

    public Vector3 ToUV3() => new Vector3(x, y, z);
}

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    public float phase = 0;
    public float depth = 10;
    public float gravity = 9;
    [Space]
    public Transform WaterPlane;
    [SerializeField] Material defaultWaterMaterial;
    public Material DefaultWaterMaterial { get => defaultWaterMaterial; }



    [SerializeField]
    public WaveData[] waves = new WaveData[4];


    [SerializeField] Transform _followTarget;
    /// <summary>
    /// The WaterMeshMaker script makes a plane with varying vertex density, the most verticies around the center.
    /// we can use this follow target to update the vertex shader, and move the center most
    /// </summary>
    public Transform _FollowTarget { get => _followTarget; }


    void UpdateVertexShaderFollowTarget(Material material = null)
    {
        if (_followTarget == null) return;
        if (material == null) material = defaultWaterMaterial;
        material.SetVector("_followPosition", _followTarget.position);

    }


    double GetTheta(WaveData wave, Vector3 position, float time)
    {
        float xFactor = wave.direction.x * position.x;
        float zFactor = wave.direction.z * position.z;

        float xzFactor = xFactor + zFactor;

        double frequency = GetFrequency(wave.direction.ToUV3(), this.gravity, depth) * time;

        return xzFactor - frequency - phase;


    }

    double GetFrequency(Vector3 direction, float gravity, float depth)
    {
        float dirLength = direction.magnitude;
        float g = gravity * dirLength;
        float depthFactor = depth * dirLength;

        double depthTanh = Math.Tanh(depthFactor);

        return math.sqrt(g * depthTanh);
    }


    Vector3 GetGerstnerWaveOffset(Vector3 position, WaveData wave, float time)
    {
        double theta = GetTheta(wave, position, time * wave.timeScale);
        Vector3 dir = wave.direction.ToUV3();
        float dirMag = dir.magnitude;
        float a = dir.x / dirMag;
        double b = wave.amplitude / math.tanh(dirMag * depth);

        double xComponent = -1 * a * b * Math.Sin(theta);


        double yComponent = math.cos(theta) * wave.amplitude;


        a = dir.z / dirMag;
        b = wave.amplitude / math.tanh(dirMag * depth);

        double zComponent = -1 * a * b * Math.Sin(theta);


        return new Vector3((float)xComponent, (float)yComponent, (float)zComponent);
    }


    /// <summary>
    /// gets the water height at a given position based on waves input to the wavemanager. 
    /// FOR THIS TO WORK you MUST mirror the wave material's values to the wave manager. 
    /// TODO/DEBUG make wavemanager read properties in start, and be able to modify from setters here as well.
    /// </summary>
    /// <param name="position">position to get the point's height at.</param>
    /// <returns>Height of water according to waves assigned, at given position.</returns>
    public float GetWaterHeight(Vector3 position)
    {
        return CalculateWaterHeight(
            new Vector3DLL(position),
            waves,
            4,
            Time.time,
            new Vector3DLL(WaterPlane.position),
            depth
            );

    }


    [DllImport("SDS_Waves")]
    public static extern float CalculateWaterHeight(
        Vector3DLL position,
        [In] WaveData[] waves,
        int waveCount,
        float time,
        Vector3DLL waterPlanePos,
        float newDepth
        );


    [Button("Update Wave From ShaderParams")]
    public void GetAndUpdateParamsFromWaterMaterial(Material material = null)
    {
        if(material == null) material = defaultWaterMaterial; // by default can be called without a material, and will use the default water material.

        phase = material.GetFloat("_phase"); // phase of waves
        depth = material.GetFloat("_depth"); // depth of the actual water.
        gravity = material.GetFloat("_gravity");

        for (int i = 1; i < 5; ++i) // 1 to <5 because i made the wave params in shader 1 indexed and im too lazy to change it ngl.
        {
            waves[i - 1] = new WaveData(
                material.GetFloat("_timescale_" + i.ToString()),
                material.GetFloat("_amplitude_" + i.ToString()),
                material.GetVector("_direction_" + i.ToString())
                );
        }

    }


    public void SetWaterShaderParams(Material material, WaterShaderSettings settings)
    {

    }
    private void Awake()
    {
        if (instance != null) GameObject.Destroy(instance.gameObject);
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetAndUpdateParamsFromWaterMaterial(defaultWaterMaterial);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVertexShaderFollowTarget();
    }
}
