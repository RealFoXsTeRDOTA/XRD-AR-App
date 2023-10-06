using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private PlanetDetails planetDetails;
    [SerializeField] private Orbit orbit;

    public PlanetDetails PlanetDetails => planetDetails;
    public Orbit Orbit => orbit;
}
