using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 20f;

    private void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
