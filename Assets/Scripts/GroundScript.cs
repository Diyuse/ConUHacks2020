using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    public GameObject indicator;
    public TextMeshProUGUI action;

    private Pose pose;
    private ARTapToPlaceObject arTapToPlaceObject;
    private Vector3 difference;
    private GameManager gameManager;
    private GameObject player;
    
    void Start()
    {
        arTapToPlaceObject = FindObjectOfType<ARTapToPlaceObject>();
        gameManager = FindObjectOfType<GameManager>();
        player = GameObject.FindWithTag("Player");
        player.SetActive(false);
        action.text = "";
    }

    private void Update()
    {
        if (gameManager.CurrentState == GameManager.GameState.Initializing)
        {
            action.text = "Tap to Place the World.";
            pose = arTapToPlaceObject.Pose;
            difference =
                (indicator.transform.position -
                 transform.position); // Vector that points from the player to the indicator
            transform.Translate(difference); // Move the player using that vector
            transform.rotation = pose.rotation; // Rotate player with the camera
            
            if (Input.touchCount > 0)
            {
                gameManager.SetGameState(GameManager.GameState.Playing);
                player.SetActive(true);
            }
        }

    }
}
