using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UiManager : MonoBehaviour
{
    [SerializeField] private AppManager appManager;
    [SerializeField] private TrackableController trackableController;
    
    public UIDocument mainDoc;
    public VisualElement rootElement;

    private Toggle _usePlaneButton;
    private Toggle _usePointCloudButton;
    private Toggle _useAnchorButton;
    private Toggle _useTrackableImageButton;
    private Toggle _useFaceButton;
    private Toggle _useTrackedObjectButton;
    private Toggle _useParticipantButton;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (appManager == null)
        {
            appManager = FindAnyObjectByType<AppManager>();
        }
        
        if (mainDoc)
        {
            rootElement = mainDoc.rootVisualElement;

            _usePlaneButton = rootElement.Q<Toggle>("usePlaneButton");
            _usePointCloudButton = rootElement.Q<Toggle>("usePointCloudButton");
            _useAnchorButton = rootElement.Q<Toggle>("useAnchorButton");
            _useTrackableImageButton = rootElement.Q<Toggle>("useTrackableImageButton");
            _useFaceButton = rootElement.Q<Toggle>("useFaceButton");
            _useTrackedObjectButton = rootElement.Q<Toggle>("useTrackedObjectButton");
            _useParticipantButton = rootElement.Q<Toggle>("useParticipantButton");


            _usePlaneButton.RegisterValueChangedCallback(OnARPlaneManagerChanged);
            _usePointCloudButton.RegisterValueChangedCallback(OnARPointCloudManagerChanged);
            _useAnchorButton.RegisterValueChangedCallback(OnARAnchorManagerChanged);
            _useTrackableImageButton.RegisterValueChangedCallback(OnARTrackedImageManagerChanged);
            _useFaceButton.RegisterValueChangedCallback(OnAARFaceManagerChanged);
            _useTrackedObjectButton.RegisterValueChangedCallback(OnARTrackedObjectManagerChanged);
            _useParticipantButton.RegisterValueChangedCallback(OnARParticipantManagerChanged);
        }
    }



    public void OnARPlaneManagerChanged(ChangeEvent<bool> evt)
    {
        trackableController.usePlaneManager = evt.newValue;
        Debug.Log("Plane" + evt.newValue);
        trackableController.SetUpTrackableManager();
    }
    
    public void OnARPointCloudManagerChanged(ChangeEvent<bool> evt)
    {
        trackableController.usePointCloudManager = evt.newValue;
        trackableController.SetUpTrackableManager();
    }
    
    public void OnARAnchorManagerChanged(ChangeEvent<bool> evt)
    {
        trackableController.useAnchorManager = evt.newValue;
        trackableController.SetUpTrackableManager();
    }
    
    public void OnARTrackedImageManagerChanged(ChangeEvent<bool> evt)
    {
        trackableController.useTrackedImageManager = evt.newValue;
        trackableController.SetUpTrackableManager();
    }
    
    public void OnAARFaceManagerChanged(ChangeEvent<bool> evt)
    {
        trackableController.useFaceManager = evt.newValue;
        trackableController.SetUpTrackableManager();
    }
    
    public void OnARTrackedObjectManagerChanged(ChangeEvent<bool> evt)
    {
        trackableController.useTrackedObjectManager = evt.newValue;
        trackableController.SetUpTrackableManager();
    }
    
    public void OnARParticipantManagerChanged(ChangeEvent<bool> evt)
    {
        trackableController.useParticipantManager = evt.newValue;
        trackableController.SetUpTrackableManager();
    }
    
    

    public void ToggleSession(bool newState)
    {
        Debug.Log("New session state is " + newState);
        appManager.ToggleSession(newState);
    }
    
   
}
