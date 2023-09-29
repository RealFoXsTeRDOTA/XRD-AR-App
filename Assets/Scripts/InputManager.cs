using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private ARRaycastManager _arRaycastManager;

    [SerializeField]
    private GameObject _spawnablePrefab;

    private const string Spawnable = "Spawnable";
    private Camera _arCam;
    private GameObject _spawnedObject;
    protected InputAction _pressAction;

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
        var _hits = new List<ARRaycastHit>();

        if (_arRaycastManager.Raycast(position, _hits))
        {
            if (Physics.Raycast(ray, out var hit))
            {
                SpawnPrefab(hit.point);
            }
        }
    }

    protected virtual void OnPressBegan(Vector3 position) { }

    protected virtual void OnPressCancel() { }

    private void SpawnPrefab(Vector3 spawnPosition)
    {
        var spawnedObjects = GameObject.FindGameObjectsWithTag(Spawnable);
        foreach (var spawnedObject in spawnedObjects)
        {
            Destroy(spawnedObject);
        }

        spawnPosition.y += 1;
        _spawnedObject = Instantiate(_spawnablePrefab, spawnPosition, Quaternion.identity);
    }
}
