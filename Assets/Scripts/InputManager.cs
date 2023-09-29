using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private ARRaycastManager arRaycastManager;

    [SerializeField]
    private GameObject spawnablePrefab;

    private const string Planet = "Planet";
    private Camera _arCam;
    private GameObject _solarSystem;
    private InputAction _pressAction;

    private GameObject _selectedPlanet;
    private Vector3 _planetPreviousPosition;

    protected virtual void Awake()
    {
        _pressAction = new InputAction("touch", binding: "<Pointer>/press");
        _arCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        _pressAction.started += ctx =>
        {
            if (ctx.control.device is Pointer device)
            {
                OnPressBegan(device.position.ReadValue());
            }
        };

        _pressAction.performed += ctx =>
        {
            if (ctx.control.device is Pointer device)
            {
                OnPress(device.position.ReadValue());
            }
        };

        _pressAction.canceled += _ => OnPressCancel();
    }
    
    private void Update()
    {
        if (_selectedPlanet != null)
        {
            var camTransform = _arCam.transform;
            _selectedPlanet.transform.position = Vector3.Lerp(
                _selectedPlanet.transform.position, 
                camTransform.position + camTransform.up * 0.3f + camTransform.forward,
                2 * Time.deltaTime);
        }
    }

    protected virtual void OnEnable()
    {
        _pressAction.Enable();
    }

    protected virtual void OnDisable()
    {
        _pressAction.Disable();
    }

    protected virtual void OnDestroy()
    {
        _pressAction.Dispose();
    }

    protected virtual void OnPress(Vector3 position)
    {
        var ray = _arCam.ScreenPointToRay(position);
        var hits = new List<ARRaycastHit>();

        if (!arRaycastManager.Raycast(position, hits))
        {
            return;
        }

        if (!Physics.Raycast(ray, out var hit))
        {
            return;
        }
        
        var touchedObject = hit.transform.gameObject;
        switch (touchedObject.tag)
        {
            case Planet:
                HandlePlanetPosition(touchedObject);
                break;
            default:
                SpawnSolarSystem(hit.point);
                break;
        }
    }

    protected virtual void OnPressBegan(Vector3 position) { }

    protected virtual void OnPressCancel() { }

    private void SpawnSolarSystem(Vector3 spawnPosition)
    {
        Destroy(_solarSystem);
        spawnPosition.y += 1;
        _solarSystem = Instantiate(spawnablePrefab, spawnPosition, Quaternion.identity);
    }

    private void HandlePlanetPosition(GameObject planet)
    {
        if (_selectedPlanet == planet)
        {
            _selectedPlanet.transform.position = _planetPreviousPosition;
            _selectedPlanet.GetComponent<Orbit>().enabled = true;
            _selectedPlanet = null;
        }
        else
        {
            if (_selectedPlanet != null)
            {
                _selectedPlanet.GetComponent<Orbit>().enabled = true;
                _selectedPlanet.transform.position = _planetPreviousPosition;
            }
            _selectedPlanet = planet;
            _planetPreviousPosition = planet.transform.position;
            _selectedPlanet.GetComponent<Orbit>().enabled = false;
        }
    }
}
