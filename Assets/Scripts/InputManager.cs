using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class InputManager : MonoBehaviour
{
    [SerializeField] private ARRaycastManager arRaycastManager;
    [SerializeField] private GameObject spawnablePrefab;
    [SerializeField] private float returnToOrbitSeconds = 1.5f;

    private const string Sun = "Sun";
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
            else if (touchedObject.CompareTag(Sun))
            {
                StopAllCoroutines();
                Destroy(_solarSystem);
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
        GetComponent<SimulationManager>().UpdateOrbitAndRotationScripts();
    }

    private void HandlePlanetPosition(GameObject planet)
    {
        if (_selectedPlanet == planet)
        {
            userInterface.HidePlanetDetails();
            StartCoroutine(ReturnToOrbit(_selectedPlanet, _planetPreviousPosition));
            _selectedPlanet = null;
        }
        else
        {
            if (!planet.GetComponent<Orbit>().enabled)
            {
                return;
            }

            if (_selectedPlanet != null)
            {
                StartCoroutine(ReturnToOrbit(_selectedPlanet, _planetPreviousPosition));
            }

            _selectedPlanet = planet;
            _planetPreviousPosition = planet.transform.position;
            _selectedPlanet.GetComponent<Orbit>().enabled = false;
            _selectedPlanet.GetComponent<TrailRenderer>().emitting = false;
            userInterface.ShowPlanetDetails(_selectedPlanet.GetComponent<Planet>());
        }
    }

    private IEnumerator ReturnToOrbit(GameObject planet, Vector3 endPosition)
    {
        var startPosition = planet.transform.position;
        var elapsedTime = 0f;

        while (elapsedTime < returnToOrbitSeconds)
        {
            planet.transform.position = Vector3.Lerp(startPosition, endPosition, (elapsedTime / returnToOrbitSeconds));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        planet.GetComponent<Orbit>().enabled = true;
        planet.GetComponent<TrailRenderer>().emitting = true;
    }
}
