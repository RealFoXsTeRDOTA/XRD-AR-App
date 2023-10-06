using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 20f;
    private float _sliderValue = 1;

    private void Update()
    {
        transform.Rotate(Vector3.up, (rotationSpeed * _sliderValue) * Time.deltaTime);
    }
    
    public void SetSliderValue(float value)
    {
        _sliderValue = value;
    }
}
