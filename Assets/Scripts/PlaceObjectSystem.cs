using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceObjectSystem : MonoBehaviour
{
    [SerializeField, Header("要放置的物件")]
    private GameObject prefabPlaceObject;

    private bool isPlaced;
    private ARRaycastManager arRaycastManager;
    private Vector2 mousePosition;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();    
    }

    private void Update()
    {
        PlaceObject();
    }

    private void PlaceObject()
    {
        if (isPlaced) return; 
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            mousePosition = Input.mousePosition;
            if(arRaycastManager.Raycast(mousePosition, hits, TrackableType.Planes))
            {
                Vector3 point = hits[0].pose.position;
                Instantiate(prefabPlaceObject, point, Quaternion.identity);
                isPlaced = true;
            }
                
        }

    }
}
