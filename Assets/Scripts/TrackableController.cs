using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARPlaneManager))]
[RequireComponent(typeof(ARPointCloudManager))]
[RequireComponent(typeof(ARAnchorManager))]
[RequireComponent(typeof(ARTrackedImageManager))]
[RequireComponent(typeof(ARFaceManager))]
[RequireComponent(typeof(ARTrackedObjectManager))]
[RequireComponent(typeof(ARParticipantManager))]
public class TrackableController : MonoBehaviour
{
    public UiManager _UiManager;
    
    private ARPlaneManager _arPlaneManager;
    private ARPointCloudManager _pointCloudManager;
    private ARAnchorManager _arAnchorManager;
    private ARTrackedImageManager _arTrackedImageManager;
    private ARFaceManager _arFaceManager;
    private ARTrackedObjectManager _trackedObjectManager;
    private ARParticipantManager _arParticipantManager;
    
    public bool usePlaneManager;
    public bool usePointCloudManager;
    public bool useAnchorManager;
    public bool useTrackedImageManager;
    public bool useFaceManager;
    public bool useTrackedObjectManager;
    public bool useParticipantManager;

    private void Awake()
    {
        _arPlaneManager = GetComponent<ARPlaneManager>();
        _pointCloudManager = GetComponent<ARPointCloudManager>();
        _arAnchorManager = GetComponent<ARAnchorManager>();
        _arTrackedImageManager = GetComponent<ARTrackedImageManager>();
        _arFaceManager = GetComponent<ARFaceManager>();
        _trackedObjectManager = GetComponent<ARTrackedObjectManager>();
        _arParticipantManager = GetComponent<ARParticipantManager>();
    }

    private void OnEnable()
    {
        SetUpTrackableManager();
    }
    
    private void OnDisable()
    {
        DisableAll();
    }
    

    public void SetUpTrackableManager()
    {
        _arPlaneManager.enabled = usePlaneManager;
        _pointCloudManager.enabled = usePointCloudManager;
        _arAnchorManager.enabled = useAnchorManager;
        _arTrackedImageManager.enabled = useTrackedImageManager;
        _arFaceManager.enabled = useFaceManager;
        _trackedObjectManager.enabled = useTrackedObjectManager;
        _arParticipantManager.enabled = useParticipantManager;

        // making sure we are not subscribing twice
        DisableAll();
        
        if (usePlaneManager)
        {
            _arPlaneManager.planesChanged += ArPlaneManagerOnplanesChanged;
        }

        if (usePointCloudManager)
        {
            _pointCloudManager.pointCloudsChanged += PointCloudManagerOnpointCloudsChanged;
        }

        if (useAnchorManager)
        {
            _arAnchorManager.anchorsChanged += ArAnchorManagerOnanchorsChanged;
        }

        if (useTrackedImageManager)
        {
            _arTrackedImageManager.trackedImagesChanged += ArTrackedImageManagerOntrackedImagesChanged;
        }

        if (useFaceManager)
        {
            _arFaceManager.facesChanged += ArFaceManagerOnfacesChanged;
        }

        if (useTrackedObjectManager)
        {
            _trackedObjectManager.trackedObjectsChanged += TrackedObjectManagerOntrackedObjectsChanged;
        }

        if (useParticipantManager)
        {
            _arParticipantManager.participantsChanged += ArParticipantManagerOnparticipantsChanged;
        }
    }

    private void DisableAll()
    {
        _arPlaneManager.planesChanged -= ArPlaneManagerOnplanesChanged;
        _pointCloudManager.pointCloudsChanged -= PointCloudManagerOnpointCloudsChanged;
        _arAnchorManager.anchorsChanged -= ArAnchorManagerOnanchorsChanged;
        _arTrackedImageManager.trackedImagesChanged -= ArTrackedImageManagerOntrackedImagesChanged;
        _arFaceManager.facesChanged -= ArFaceManagerOnfacesChanged;
        _trackedObjectManager.trackedObjectsChanged -= TrackedObjectManagerOntrackedObjectsChanged;
        _arParticipantManager.participantsChanged -= ArParticipantManagerOnparticipantsChanged;
    }

    

    private void ArPlaneManagerOnplanesChanged(ARPlanesChangedEventArgs args)
    {
        // Lists all planes
        // _arPlaneManager.trackables 
        
        // only new planes which are added
        // args.added

        foreach (var newPlane in args.added)
        {
            if (newPlane.alignment == PlaneAlignment.Vertical)
            {
                // make wall etc
            }
        }
            
            
        // only  planes which are removed
        // args.removed
        
        // only  planes which are updated, size changed etc
        // args.updated
        
    }

    private void PointCloudManagerOnpointCloudsChanged(ARPointCloudChangedEventArgs obj)
    {
    }
    
    private void ArAnchorManagerOnanchorsChanged(ARAnchorsChangedEventArgs obj)
    {
    }
    
    private void ArTrackedImageManagerOntrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        
    }
    
    private void ArFaceManagerOnfacesChanged(ARFacesChangedEventArgs args)
    {
        if (args.added.Count > 0)
        {
            Debug.Log("Face detected !");
            
            // hide lock screen
            _UiManager.HideLockScreen();
        }
        
    }
    
    private void TrackedObjectManagerOntrackedObjectsChanged(ARTrackedObjectsChangedEventArgs obj)
    {
        
    }
    
    private void ArParticipantManagerOnparticipantsChanged(ARParticipantsChangedEventArgs obj)
    {
    }

}
