using UnityEngine;

public class Orbit : MonoBehaviour
{
    [SerializeField] private GameObject objectToOrbit;
    [SerializeField] private float currentAngle = 0f;
    [SerializeField] private float orbitSpeed = 0.4f;

    private float orbitDistance;

    private void Start()
    {
        orbitDistance = (objectToOrbit.transform.position - transform.position).magnitude;
    }

    private void Update()
    {
        var xPosition = orbitDistance * Mathf.Sin(currentAngle);
        var zPosition = orbitDistance * Mathf.Cos(currentAngle);

        transform.localPosition = new Vector3(xPosition, 0f, zPosition);
        currentAngle += orbitSpeed * Time.deltaTime;
    }
}
