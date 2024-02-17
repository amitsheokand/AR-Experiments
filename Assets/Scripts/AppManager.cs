using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class AppManager : MonoBehaviour
{
    [SerializeField] private ARSession _arSession;
    
    // Start is called before the first frame update
    
    
    IEnumerator Start() {
        if ((ARSession.state == ARSessionState.None) || (ARSession.state == ARSessionState.CheckingAvailability)){
            yield return ARSession.CheckAvailability();
        }
        if (ARSession.state == ARSessionState.Unsupported){
        
            // Start some fallback experience for unsupported devices
        }
        else{
        
            // Start the AR session
            _arSession.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleSession(bool newState)
    {
        if (_arSession)
        {
            _arSession.enabled = newState;
        }
    }
}
