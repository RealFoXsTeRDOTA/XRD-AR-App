using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    private float _orbitSlider = 1;
    private float _rotationSlider = 1;
    
    public void SetOrbitSlider(float value)
    {
        _orbitSlider = value;
        UpdateOrbitScripts();
    }
    
    public void SetRotationSlider(float value)
    {
        _rotationSlider = value;
        UpdateRotationScripts();
    }

    public void UpdateOrbitScripts()
    {
        var scripts = FindObjectsByType<Orbit>(FindObjectsSortMode.None);
        foreach (var script in scripts)
        {
            script.SetSliderValue(_orbitSlider);
        }
    }
    
    public void UpdateRotationScripts()
    {
        var scripts = FindObjectsByType<Rotate>(FindObjectsSortMode.None);
        foreach (var script in scripts)
        {
            script.SetSliderValue(_rotationSlider);
        }
    }

    public void UpdateOrbitAndRotationScripts()
    {
        UpdateOrbitScripts();
        UpdateRotationScripts();
    }
}
