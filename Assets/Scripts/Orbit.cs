using UnityEngine;

public class Orbit : MonoBehaviour
{
    [SerializeField] private GameObject objectToOrbit;
    [SerializeField] private float orbitSpeed = 0.4f;

    private float currentAngle = 0f;
    private const float scale = 0.02f;
    private float _sliderValue = 1;
    private float orbitDistance = 0f;
    private const float oneFullRotation = 2 * Mathf.PI;

    public float OrbitalPeriodPercentage => currentAngle % oneFullRotation / oneFullRotation * 100;
    public int YearsPassed => (int)(currentAngle / oneFullRotation);

    private void Start()
    {
        orbitDistance = (objectToOrbit.transform.position - transform.position).magnitude;
    }

    private void Update()
    {
        var xPosition = orbitDistance * Mathf.Sin(currentAngle);
        var zPosition = orbitDistance * Mathf.Cos(currentAngle);

        transform.localPosition = new Vector3(xPosition, 0f, zPosition);
        currentAngle += orbitSpeed * Time.deltaTime * (scale * _sliderValue);
    }

    public void SetSliderValue(float value)
    {
        _sliderValue = value;
    }
}
