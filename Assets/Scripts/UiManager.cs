using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private AppManager appManager;
    
    // Start is called before the first frame update
    void Start()
    {
        if (appManager == null)
        {
            appManager = FindAnyObjectByType<AppManager>();
        }
        
    }

    public void ToggleSession(bool newState)
    {
        Debug.Log("New session state is " + newState);
        appManager.ToggleSession(newState);
    }
    
   
}
