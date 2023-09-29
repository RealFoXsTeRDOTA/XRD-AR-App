using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

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
    private UserInterfaceManager userInterface;
    private GameObject _selectedPlanet;
    private Vector3 _planetPreviousPosition;

    private void Awake()
    {
        _pressAction = new InputAction("touch", binding: "<Pointer>/press");
        _arCam = GameObject.Find("Main Camera").GetComponent<Camera>();

        _pressAction.performed += ctx =>
        {
            if (ctx.control.device is Pointer device)
            {
                OnPress(device.position.ReadValue());
            }
        };

        _pressAction.Enable();
        userInterface = GetComponent<UserInterfaceManager>();
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

    private void OnDestroy()
    {
        _pressAction.Dispose();
    }

    private void OnPress(Vector3 position)
    {
        var ray = _arCam.ScreenPointToRay(position);

        if (Physics.Raycast(ray, out var hit))
        {
            var touchedObject = hit.transform.gameObject;
            if (touchedObject.CompareTag(Planet))
            {
                HandlePlanetPosition(touchedObject);
                return;
            }
        }

        var hits = new List<ARRaycastHit>();

        if (arRaycastManager.Raycast(position, hits, TrackableType.PlaneWithinPolygon))
        {
            SpawnSolarSystem(hits[0].pose.position);
        }
    }

    private void SpawnSolarSystem(Vector3 spawnPosition)
    {
        Destroy(_solarSystem);
        spawnPosition.y += 1;
        _solarSystem = Instantiate(spawnablePrefab, spawnPosition, Quaternion.identity);
        userInterface.HidePlanetDetails();
    }

    private void HandlePlanetPosition(GameObject planet)
    {
        if (_selectedPlanet == planet)
        {
            _selectedPlanet.transform.position = _planetPreviousPosition;
            _selectedPlanet.GetComponent<Orbit>().enabled = true;
            _selectedPlanet = null;
            userInterface.HidePlanetDetails();
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
            userInterface.ShowPlanetDetails(_selectedPlanet.GetComponent<Planet>());
        }
    }
}
