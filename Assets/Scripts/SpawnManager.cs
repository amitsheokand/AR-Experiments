using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class SpawnManager : MonoBehaviour
{
    public GameObject objectToSpawn;

    private GameObject createdObject;
    
    private ARRaycastManager _arRaycastManager;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
    }


    void Update()
    {
        if(objectToSpawn == null) return;
        if(_arRaycastManager == null) return;
        
        if (Input.touchCount > 0)
        {
            var touchPosition = Input.touches[0].position;

            if (_arRaycastManager.Raycast(touchPosition, hits,TrackableType.PlaneWithinPolygon ))
            {
                // if true
                // var arPlane = (ARPlane) hits[0].trackable
                
                Pose hitPose = hits[0].pose;

                if (createdObject == null)
                {
                    createdObject = Instantiate(objectToSpawn, hitPose.position, hitPose.rotation);
                }
                else
                {
                    createdObject.transform.position = hitPose.position;
                }
            }
        }
    }
}
