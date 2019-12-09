using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.EventSystems;

public class ViveControllerLookModule : PointerInputModule
{
    public Camera ControllerCamera;
    public SteamVR_TrackedObject RightController;
    public GameObject reticle;
    public Transform laserTransform;
    private Transform rightControllerTransform;
    // The size of the reticle will get scaled with this value
    public float reticleSizeMultiplier = 0.02f;
    private PointerEventData pointerEventData;
    private RaycastResult currentRaycast;
    private GameObject currentLookAtHandler;

    public SteamVR_Input_Sources handType;

    

    void Awake()
    {
        rightControllerTransform = RightController.transform;
    }

    public override void Process()
    {
        HandleLook();
        HandleSelection();
    }

    void HandleLook()
    { 
        if (pointerEventData == null)
        {
            pointerEventData = new PointerEventData(eventSystem);
        }

        pointerEventData.position = ControllerCamera.ViewportToScreenPoint(new Vector3(.5f, .5f));
        List<RaycastResult> raycastResults = new List<RaycastResult>(); 
        eventSystem.RaycastAll(pointerEventData, raycastResults);
        currentRaycast = pointerEventData.pointerCurrentRaycast = FindFirstRaycast(raycastResults);
        // Move reticle  
        reticle.transform.position = rightControllerTransform.position + (rightControllerTransform.forward * currentRaycast.distance);
        laserTransform.position = Vector3.Lerp(rightControllerTransform.position, reticle.transform.position, .5f);
        laserTransform.LookAt(reticle.transform); 
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, currentRaycast.distance);
        float reticleSize = currentRaycast.distance * reticleSizeMultiplier;
        //Scale reticle so it’s always the same size  
        reticle.transform.localScale = new Vector3(reticleSize, reticleSize, reticleSize);
        // Pass the pointer data to the event system so entering and   
        // exiting of objects is detected 
        ProcessMove(pointerEventData);

    }

    private bool IsTriggerPressed()
    {
        return SteamVR_Input.GetStateDown("InteractUI", SteamVR_Input_Sources.Any);
    }

    void HandleSelection()
    {
        if (pointerEventData.pointerEnter != null)
        {  
            currentLookAtHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(pointerEventData.pointerEnter);
  
            if (currentLookAtHandler != null && IsTriggerPressed())
            {
                ExecuteEvents.ExecuteHierarchy(currentLookAtHandler, pointerEventData, ExecuteEvents.pointerClickHandler);
            }
        }
        else
        {
            currentLookAtHandler = null;
        }
    } 

}
