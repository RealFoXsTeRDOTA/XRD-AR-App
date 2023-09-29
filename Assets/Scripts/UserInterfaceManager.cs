using TMPro;
using UnityEngine;

public class UserInterfaceManager : MonoBehaviour
{
    [SerializeField] private GameObject userInterface;
    [SerializeField] private TextMeshProUGUI planetName;
    [SerializeField] private TextMeshProUGUI planetMass;
    [SerializeField] private TextMeshProUGUI planetDiameter;
    [SerializeField] private TextMeshProUGUI planetDensity;
    [SerializeField] private TextMeshProUGUI planetGravity;
    [SerializeField] private TextMeshProUGUI planetLengthOfDay;
    [SerializeField] private TextMeshProUGUI planetDistanceFromSun;
    [SerializeField] private TextMeshProUGUI planetOrbitalPeriod;
    [SerializeField] private TextMeshProUGUI planetOrbitalVelocity;
    [SerializeField] private TextMeshProUGUI planetMeanTemperature;
    [SerializeField] private TextMeshProUGUI planetMoonCount;

    public void ShowPlanetDetails(Planet planet)
    {
        var planetDetails = planet.PlanetDetails;

        planetName.text = planetDetails.Name;
        planetMass.text = $"{planetDetails.Mass} (10^24 kg)";
        planetDiameter.text = $"{planetDetails.Diameter} (km)";
        planetDensity.text = $"{planetDetails.Density} (kg/m^3)";
        planetGravity.text = $"{planetDetails.Gravity} (m/s^2)";
        planetLengthOfDay.text = $"{planetDetails.LengthOfDay} (hrs)";
        planetDistanceFromSun.text = $"{planetDetails.DistanceFromSun} (10^6 km)";
        planetOrbitalPeriod.text = $"{planetDetails.OrbitalPeriod} (days)";
        planetOrbitalVelocity.text = $"{planetDetails.OrbitalVelocity} (km/s)";
        planetMeanTemperature.text = $"{planetDetails.MeanTemperature} (ºC)";
        planetMoonCount.text = $"{planetDetails.MoonCount}";
        userInterface.SetActive(true);
    }

    public void HidePlanetDetails()
    {
        userInterface.SetActive(false);
    }
}
