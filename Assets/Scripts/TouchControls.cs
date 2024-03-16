using NCU;
using Unity.XR.CoreUtils;
using UnityEngine;

public class TouchControls : MonoBehaviour //, IPointerDownHandler, IPointerUpHandler 
{
    public GameObject obJectToRotate;

    private float minZoom = 0.1f;
    private float maxZoom = 5.0f;
    private float ZoomSpeed = -0.001f; 

    private float rotationRate = 0.15f;

    private Vector3 lastTouchZeroPos;
    private Vector3 lastTouchOnePos;

    public bool isArMode = false;
    private bool _isDragging = false;

    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) 
        {
            _isDragging = true;
        }
        

        if (_isDragging && obJectToRotate != null && isArMode)
        {
            if (Input.touchCount == 1)
            {
                foreach (Touch touch in Input.touches)
                {
                    // Debug.Log("Touching at: " + touch.position);
                
                    if (touch.phase == TouchPhase.Began)
                    {
                        // Debug.Log("Touch phase began at: " + touch.position);
                    }
                    else if (touch.phase == TouchPhase.Moved)
                    {
                        // Debug.Log("Touch phase Moved");
                        // obJectToRotate.transform.Rotate(0,
                        //                  -touch.deltaPosition.x * rotationRate, 0, Space.World);
                        
                        // obJectToRotate.transform.Rotate(
                        //     -touch.deltaPosition.y * rotationRate,0, 0, Space.Self);
                        //
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        // Debug.Log("Touch phase Ended");
                        _isDragging = false;
                    }
                }
            }
            else if (Input.touchCount == 2)  // If there are two touches on the device...
            {
                // Store both touches.
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                // Find the position in the previous frame of each touch.
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                // Find the magnitude of the vector (the distance) between the touches in each frame.
                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                // Find the difference in the distances between each frame.
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                obJectToRotate.transform.localScale += Vector3.one * (deltaMagnitudeDiff * ZoomSpeed);

                float tempScale = Mathf.Clamp(obJectToRotate.transform.localScale.x, minZoom, maxZoom);

                obJectToRotate.transform.localScale = (Vector3.one) * tempScale;
                
                obJectToRotate.transform.Rotate(0, -touchOne.deltaPosition.x * rotationRate, 0, Space.World);
                
            }
            else if (Input.touchCount == 3) // || Input.GetMouseButton(1)) // Handling in PlaceOnPlane, Only for AR mode
            {

                // Transform itemTransform = obJectToRotate.transform;
                // Vector3 change = Input.mousePosition - lastClickPos;
                // float speed = 0.0002f;
                // itemTransform.position = new Vector3(itemTransform.position.x + change.x * speed, itemTransform.position.y + change.y * speed, itemTransform.position.z);


            }
        }
    }
}