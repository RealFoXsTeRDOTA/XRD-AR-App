using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SpawnableManager : MonoBehaviour
{
    [SerializeField] private ARRaycastManager _arRaycastManager;
    [SerializeField] private GameObject _spawnablePrefab;

    private const string Spawnable = "Spawnable";
    private List<ARRaycastHit> _hits = new();
    private Camera _arCam;
    private GameObject _spawnedObject;

    // Start is called before the first frame update
    void Start()
    {
        _spawnedObject = null;
        _arCam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
        {
            return;
        }

        RaycastHit hit;
        Ray ray = _arCam.ScreenPointToRay(Input.GetTouch(0).position);
        
        if (_arRaycastManager.Raycast(Input.GetTouch(0).position, _hits))
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began && _spawnedObject == null)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.CompareTag(Spawnable))
                    {
                        _spawnedObject = hit.collider.gameObject;
                    }
                    else
                    {
                        SpawnPrefab(_hits[0].pose.position);
                    }
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved && _spawnedObject != null)
            {
                _spawnedObject.transform.position = _hits[0].pose.position;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                _spawnedObject = null;
            }
        }
    }

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