using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private PlanetDetails planetDetails;

    public PlanetDetails PlanetDetails => planetDetails;
}
