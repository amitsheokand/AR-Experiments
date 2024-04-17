using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Random = UnityEngine.Random;


[RequireComponent(typeof(ARPlaneManager))]
public class TankSetup : MonoBehaviour
{
    public GameObject tankobject;
    public GameObject uiObjects;

    public List<GameObject> hurdlePrefabs;
    
    private ARPlaneManager _arPlaneManager;
    private List<GameObject> hurdles;

    private bool tankCreated = false;
    private ARPlane tankPlane = null;

    private void Awake()
    {
        hurdles = new List<GameObject>();
    }


    // Start is called before the first frame update
    void Start()
    {
        _arPlaneManager = GetComponent<ARPlaneManager>();
        _arPlaneManager.planesChanged += ArPlaneManagerOnplanesChanged;
        
        tankobject.SetActive(false);
        uiObjects.SetActive(false);

        // debug code
        for (int i = 0; i < 10; i++)
        {
            SpawnHurdle(new List<Vector3>()
            {
                new Vector3(5, 0, 5),
                new Vector3(-5, 0, 5),
                new Vector3(-5, 0, -5),
                new Vector3(5, 0, -5),
            }, 0);
        }
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
                
                tankPlane.boundaryChanged += PlaneBoundaryChanged;
                
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

    private void PlaneBoundaryChanged(ARPlaneBoundaryChangedEventArgs args)
    {
        List<Vector3> _3dPoints = new List<Vector3>(args.plane.boundary.Length);

        foreach (var _2dPoint in args.plane.boundary)
        {
            _3dPoints.Add(new Vector3(_2dPoint.x, tankPlane.transform.position.y, _2dPoint.y));
        }

        SpawnHurdle(_3dPoints, tankPlane.transform.position.y);

    }

    private void SpawnHurdle(List<Vector3> _3dPoints, float yPos)
    {
        Vector3 randomPoint = GeneratePointInsidePolygon(_3dPoints, yPos);
        var prefabToSpawn = hurdlePrefabs[Random.Range(0, hurdlePrefabs.Count)];
        
        // TODO : check if we can spawn it
        
        
        // TODO: check if randomPoint is not close to play or any other hurdle
        
        var newHurdle = Instantiate(prefabToSpawn, randomPoint, Quaternion.identity);
        
        hurdles.Add(newHurdle);
    }
    
    private Vector3 GeneratePointInsidePolygon(List<Vector3> polygon, float yPos)
    {
        Vector3 MinVec = MinPointOnThePolygon(polygon, yPos);
        Vector3 MaxVec = MaxPointOnThePolygon(polygon, yPos);
        Vector3 GenVector;
    
        float x = ((Random.value) * (MaxVec.x- MinVec.x)) + MinVec.x;
        float z = ((Random.value) * (MaxVec.z - MinVec.z)) + MinVec.z;
        GenVector = new Vector3(x, yPos, z);
    
        // TODO : check !OverlappingAnyting()
        
        while(!IsPointInPolygon(polygon,GenVector))
        {
            x = ((Random.value) * (MaxVec.x - MinVec.x)) + MinVec.x;
            z = ((Random.value) * (MaxVec.z - MinVec.z)) + MinVec.z;
            GenVector.x = x;
            GenVector.z = z;
        }
        return GenVector;

    }

    private Vector3 MinPointOnThePolygon(List<Vector3> polygon, float yPos)
    {
        float minX = polygon[0].x;
        float minZ = polygon[0].z;
        for (int i = 1; i<polygon.Count;i++)
        {
            if(minX > polygon[i].x)
            {
                minX = polygon[i].x;
            }
            if (minZ > polygon[i].z)
            {
                minZ = polygon[i].z;
            }
        }
        return new Vector3(minX, yPos, minZ);
    }

    private Vector3 MaxPointOnThePolygon(List<Vector3> polygon, float yPos)
    {
        float maxX = polygon[0].x;
        float maxZ = polygon[0].z;
        for (int i = 1; i < polygon.Count; i++)
        {
            if (maxX < polygon[i].x)
            {
                maxX = polygon[i].x;
            }
            if (maxZ < polygon[i].z)
            {
                maxZ = polygon[i].z;
            }
        }
        return new Vector3(maxX, yPos, maxZ);
    }

    private bool IsPointInPolygon(List<Vector3> polygon, Vector3 point)
    {
        bool isInside = false;
        for (int i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
        {
            if (((polygon[i].x > point.x) != (polygon[j].x > point.x)) &&
                (point.z < (polygon[j].z - polygon[i].z) * (point.x - polygon[i].x) / (polygon[j].x - polygon[i].x) + polygon[i].z))
            {
                isInside = !isInside;
            }
        }
        return isInside;
    }
}
