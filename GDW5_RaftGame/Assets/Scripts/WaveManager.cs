using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Jobs;
using System;
using UnityEditor.Experimental.GraphView;
using Unity.Collections.LowLevel.Unsafe;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    public float phase = 0;
    public float depth = 10;
    public float gravity = 9;
    public float neighbourDistance = 0.001f;

    public Transform WaterPlane;
    public float visualYOffset = 0;

    [System.Serializable]
    public struct WaveData
    {
        public Vector3 direction;
        public float amplitude;
        public float timeScale;
    }

    [SerializeField]
    public WaveData[] waves = new WaveData[4];

    double GetTheta(WaveData wave, Vector3 position, float time)
    {
        float xFactor = wave.direction.x * position.x;
        float zFactor = wave.direction.z * position.z;

        float xzFactor = xFactor + zFactor;

        double frequency = GetFrequency(wave.direction, this.gravity, depth) * time;

        return xzFactor - frequency - phase;


    }

    double GetFrequency(Vector3 direction, float gravty, float depth)
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
        Vector3 dir = wave.direction;
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
        Vector3 totalDisplacement = Vector3.zero;

        for (int i = 0; i < 4; ++i)
        {
            totalDisplacement += GetGerstnerWaveOffset(position, waves[i], Time.time);
        }

        return WaterPlane.position.y + totalDisplacement.y;
    }

    private void Awake()
    {
        if (instance != null) GameObject.Destroy(instance.gameObject);
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
