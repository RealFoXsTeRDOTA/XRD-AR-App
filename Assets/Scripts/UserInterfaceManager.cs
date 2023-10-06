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
    [SerializeField] private TextMeshProUGUI planetLengthOfYear;
    [SerializeField] private TextMeshProUGUI planetOrbitalVelocity;
    [SerializeField] private TextMeshProUGUI planetMeanTemperature;
    [SerializeField] private TextMeshProUGUI planetMoonCount;
    [SerializeField] private TextMeshProUGUI daysPassedCount;
    [SerializeField] private TextMeshProUGUI yearsPassedCount;

    public void ShowPlanetDetails(Planet planet)
    {
        var planetDetails = planet.PlanetDetails;

        planetName.text = planetDetails.Name;
        planetMass.text = $"{planetDetails.Mass} (10²⁴ kg)";
        planetDiameter.text = $"{planetDetails.Diameter} (km)";
        planetDensity.text = $"{planetDetails.Density} (kg/m³)";
        planetGravity.text = $"{planetDetails.Gravity} (m/s²)";
        planetLengthOfDay.text = $"{planetDetails.LengthOfDay} (hrs)";
        planetDistanceFromSun.text = $"{planetDetails.DistanceFromSun}M (km)";
        planetLengthOfYear.text = $"{planetDetails.OrbitalPeriod} (days)";
        planetOrbitalVelocity.text = $"{planetDetails.OrbitalVelocity} (km/s)";
        planetMeanTemperature.text = $"{planetDetails.MeanTemperature} (ºC)";
        planetMoonCount.text = $"{planetDetails.MoonCount}";
        daysPassedCount.text = $"{(int)(planetDetails.OrbitalPeriod / 100 * planet.Orbit.OrbitalPeriodPercentage + (planetDetails.OrbitalPeriod * planet.Orbit.YearsPassed))}";
        yearsPassedCount.text = $"{planet.Orbit.YearsPassed}";
        userInterface.SetActive(true);
    }

    public void HidePlanetDetails()
    {
        userInterface.SetActive(false);
    }
}
