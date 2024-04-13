using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


[RequireComponent(typeof(ARPlaneManager))]
public class TankSetup : MonoBehaviour
{
    public GameObject tankobject;
    public GameObject uiObjects;
    private ARPlaneManager _arPlaneManager;

    private bool tankCreated = false;
    private ARPlane tankPlane = null;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _arPlaneManager = GetComponent<ARPlaneManager>();
        _arPlaneManager.planesChanged += ArPlaneManagerOnplanesChanged;
        
        tankobject.SetActive(false);
        uiObjects.SetActive(false);
    }

    private void ArPlaneManagerOnplanesChanged(ARPlanesChangedEventArgs args)
    {

        foreach (var newPlane in args.added)
        {
            if (tankPlane != null)
            {
                newPlane.gameObject.SetActive(false);
            }
            
            if (newPlane.alignment == PlaneAlignment.HorizontalUp && !tankCreated)
            {
                // spawn player and ui

                tankobject.transform.position = newPlane.center + new Vector3(0, 0.1f, 0);
                tankobject.SetActive(true);
                uiObjects.SetActive(true);

                tankPlane = newPlane;
                tankCreated = true;
            }
        }
            
        if (args.removed.Count > 0)
        {
            if (_arPlaneManager.trackables.count == 0)
            {
                tankobject.SetActive(false);
                uiObjects.SetActive(false);
            }
        }
        
        
        // only  planes which are updated, size changed etc
        // spawn new objects, 
        
    }
}
