using UnityEngine;

[CreateAssetMenu(fileName = "New Planet Details", menuName = "Planet")]
public class PlanetDetails : ScriptableObject
{
    [SerializeField] private new string name;
    [SerializeField] private float mass;
    [SerializeField] private float diameter;
    [SerializeField] private float gravity;
    [SerializeField] private float lengthOfDay;
    [SerializeField] private float distanceFromSun;
    [SerializeField] private float orbitalPeriod;
    [SerializeField] private float meanTemperature;
    [SerializeField] private float moonCount;
    [SerializeField] private bool hasRingSystem;
}
