using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

namespace SDS_Weather
{

    public enum WeatherTypes
    {
        none,
        wind,
        rain,
        thunder,
        COUNT
    };

    public class WeatherInfo
    {
        public WeatherTypes type;
        public int strength;
        public float windDirection;

        public WeatherInfo()
        {

            type = (WeatherTypes)UnityEngine.Random.Range(0, (int)WeatherTypes.COUNT);
            strength = UnityEngine.Random.Range(0, 3);
            windDirection = UnityEngine.Random.Range(0, 360.0f);
        }

        public WeatherInfo(WeatherTypes type, int strength, float windDir) { 
            this.type = type; 
            this.strength = strength; 
            windDirection = windDir;
        }
    }

    public class WeatherManager : MonoBehaviour
    {

        public static WeatherManager instance;

        WeatherInfo _currentWeather;
        public WeatherInfo CurrentWeather
        {
            get => _currentWeather;
            set
            {
                _currentWeather = value;
                onWeatherChanged.Invoke(_currentWeather);
            }
        }

        public UnityEvent<WeatherInfo> onWeatherChanged = new();

        private void Awake()
        {
            if (instance != null) { Destroy(instance.gameObject); }
            instance = this;
        }

        public void SetWeather(WeatherInfo newWeather) => CurrentWeather = newWeather;

        public void SetRandomWeather() => CurrentWeather = new WeatherInfo();



    }

}
