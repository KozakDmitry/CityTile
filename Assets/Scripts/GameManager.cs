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
    [SerializeField]
    private UIManager uiManager;
    [SerializeField]
    private StructureManager structureManager;
        


    private void Start()
    {
        uiManager.OnRoadPlacement += RoadPlacementHandler;
        uiManager.OnRoadPlacement += HousePlacementHandler;
        uiManager.OnRoadPlacement += SpecialPlacementHandler;

    }

    private void SpecialPlacementHandler()
    {
        ClearInputActions();
        inputManager.onMouseClick += structureManager.PlaceSpecial;
    }

    private void HousePlacementHandler()
    {
        ClearInputActions();
        inputManager.onMouseClick += structureManager.PlaceHouse;
    }

    private void RoadPlacementHandler()
    {
        ClearInputActions();
        inputManager.onMouseClick += roadManager.PlaceRoad;
        inputManager.onMouseHold += roadManager.PlaceRoad;
        inputManager.onMouseUp += roadManager.FinishPlaceMode;
    }

    private void ClearInputActions()
    {
        inputManager.onMouseClick += null;
        inputManager.onMouseHold += null;
        inputManager.onMouseUp += null;
    }

    private void Update()
    {
        cameraMovement.MoveCamera(new Vector3(inputManager.CameraMovementVector.x, 0, inputManager.CameraMovementVector.y));
    }
}
