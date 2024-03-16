using System;
using System.Collections.Generic;
using NCU;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARAnchorManager))]
[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(TouchControls))]
public class SpawnManager : MonoBehaviour
{
    public SpawnObjectType spawnObjectType;
    public GameObject objectToSpawn;
    public UIDocument mainDoc;

    public VisualElement rootElement;
    private EnumField _dropdownField;
    
    private List<ARAnchor> spawnedAnchors;
    private GameObject createdObject;
    
    private ARRaycastManager _arRaycastManager;
    private ARAnchorManager _arAnchorManager;
    private TouchControls _touchControls;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
        _arAnchorManager = GetComponent<ARAnchorManager>();
        spawnedAnchors = new List<ARAnchor>();
        _touchControls = GetComponent<TouchControls>();

        if (mainDoc)
        {
            rootElement = mainDoc.rootVisualElement;
            _dropdownField = rootElement.Q<EnumField>();
            _dropdownField.RegisterValueChangedCallback(OnObjectTypeChanged);
        }
    }

    private void OnObjectTypeChanged(ChangeEvent<Enum> evt)
    {
        spawnObjectType = (SpawnObjectType)evt.newValue;
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
                var arPlane = (ARPlane)hits[0].trackable;
                
                if (arPlane.normal == Vector3.up)
                {
                    Debug.Log("Horizontal Plane");
                }
                else
                {
                    Debug.Log("Vertical Plane");
                }
                
                Pose hitPose = hits[0].pose;

                switch (spawnObjectType)
                {
                    case SpawnObjectType.Object:
                    {
                        if (createdObject == null)
                        {
                            createdObject = Instantiate(objectToSpawn, hitPose.position, hitPose.rotation);

                            _touchControls.obJectToRotate = createdObject;
                            _touchControls.isArMode = true;
                            
                            var objectBounds = createdObject.GetObjectBounds();

                            createdObject.transform.position = new Vector3(hitPose.position.x,
                                hitPose.position.y + objectBounds.size.y, hitPose.position.z);
                        }
                        else
                        {
                            // createdObject.transform.position = hitPose.position;
                            
                            // set y position 
                            // keep position on top of plane
                            var objectBounds = createdObject.GetObjectBounds();

                            createdObject.transform.position = new Vector3(hitPose.position.x,
                                hitPose.position.y + objectBounds.size.y, hitPose.position.z);

                        }
                    }
                        break;

                    case SpawnObjectType.Anchor:
                    {
                        // Create an instance of the prefab
                        var instance = Instantiate(_arAnchorManager.anchorPrefab, hitPose.position, hitPose.rotation);

                        // Add an ARAnchor component if it doesn't have one already.
                        if (instance.GetComponent<ARAnchor>() == null)
                        {
                            instance.AddComponent<ARAnchor>();
                        }
                        
                        spawnedAnchors.Add(instance.GetComponent<ARAnchor>());
                    }
                        break;
                    
                }
            }
        }
    }

}
