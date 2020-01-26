using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject placementIndicator; // Indicator object in the scene
    public GameObject ground;
    public TextMeshProUGUI action;
    
    private ARRaycastManager raycastManager;
    private Pose placementPose; // To know the place in the space
                                // a pose object describes the position and rotation of a 3d point
    private bool placementPoseIsValid = false;
    private GameManager gm;

    public Pose Pose
    {
        get { return placementPose; }
    }
    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        gm = FindObjectOfType<GameManager>();
    }
    
    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if (gm.CurrentState == GameManager.GameState.Initializing)
        {
            if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                PlaceObject();
            }
        }
    }

    private void PlaceObject()
    {
        Instantiate(ground, placementPose.position, placementPose.rotation);
        gm.SetGameState(GameManager.GameState.Starting);
    }

    private void UpdatePlacementPose()
    {
        Vector2 screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f,0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>(); // Array of points in physical space where the ray hits a physical surface
        raycastManager.Raycast(screenCenter, hits, TrackableType.Planes); 
                                            // Sends a ray from some point in our screen to some point in the real world
        placementPoseIsValid = hits.Count > 0; // Placement pose is valid if there are hits
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose; // Update the placementPose with the first hit
            if (gm.CurrentState == GameManager.GameState.Initializing)
            {
                action.text = "Tap to place ground";
            }
        }
        else
        {
            if (gm.CurrentState == GameManager.GameState.Initializing)
            {
                action.text = "Look for available floor, indicator will show up.";
            }
        }
    }

    private void UpdatePlacementIndicator()
    {
        // placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        // Check if the pose is valid and enable the indicator if the pose placement is valid
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true); // Make the indicator visible
            // Vector3 pos = new Vector3(placementPose.position.x, height, placementPose.position.z);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false); // Make the indicator invisible
        }
    }
}
