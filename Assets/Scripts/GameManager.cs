using SVS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private CameraMovement cameraMovement;

    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private RoadManager roadManager;



    private void Start()
    {
        inputManager.onMouseClick += roadManager.PlaceRoad;
        inputManager.onMouseHold += roadManager.PlaceRoad;
        inputManager.onMouseUp += roadManager.FinishPlaceMode;
    }

 

    private void Update()
    {
        cameraMovement.MoveCamera(new Vector3(inputManager.CameraMovementVector.x, 0, inputManager.CameraMovementVector.y));
    }
}
