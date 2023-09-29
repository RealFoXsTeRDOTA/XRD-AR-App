using UnityEngine;

[CreateAssetMenu(menuName = "Planet details")]
public class PlanetDetails : ScriptableObject
{
    [SerializeField] private new string name;
    [SerializeField] private float mass;
    [SerializeField] private float diameter;
    [SerializeField] private float density;
    [SerializeField] private float gravity;
    [SerializeField] private float lengthOfDay;
    [SerializeField] private float distanceFromSun;
    [SerializeField] private float orbitalPeriod;
    [SerializeField] private float orbitalVelocity;
    [SerializeField] private float meanTemperature;
    [SerializeField] private float moonCount;

    public string Name => name;
    public float Mass => mass;
    public float Diameter => diameter;
    public float Density => density;
    public float Gravity => gravity;
    public float LengthOfDay => lengthOfDay;
    public float DistanceFromSun => distanceFromSun;
    public float OrbitalPeriod => orbitalPeriod;
    public float OrbitalVelocity => orbitalVelocity;
    public float MeanTemperature => meanTemperature;
    public float MoonCount => moonCount;
}
