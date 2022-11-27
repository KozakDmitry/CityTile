using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    [SerializeField]
    private PlacementManager placementManager;

    public List<Vector3Int> temporaryPlacementPos = new List<Vector3Int>();
    public List<Vector3Int> AdjasentPositionsToAdapt = new List<Vector3Int>();

    [SerializeField]
    private GameObject roadStraight;
    
    private RoadAdapter roadAdapter;

    private void Start()
    {
        roadAdapter = GetComponent<RoadAdapter>();
    }
    public void PlaceRoad(Vector3Int position)
    {
        if (placementManager.CheckPositionInBound(position) == false)
        {
            return;
        }
        if (placementManager.CheckIfPositionIsFree(position)== false)
        {
            return;
        }
        temporaryPlacementPos.Clear();
        temporaryPlacementPos.Add(position);
    
        placementManager.PlaceTemporary(position, roadStraight, CellType.Road);
        AdaptRoadPrefabs();
    }

    private void AdaptRoadPrefabs()
    {
        foreach(var tempPosition in temporaryPlacementPos)
        {
            roadAdapter.FixRoadAtPosition(placementManager, tempPosition);
            var neighbors = placementManager.GetNeighboursTypesFor(tempPosition, CellType.Road);
            foreach(var roadPosition in neighbors)
            {
                AdjasentPositionsToAdapt.Add(roadPosition);
            }
        }
        foreach(var positionToAdapt in AdjasentPositionsToAdapt)
        {
            roadAdapter.FixRoadAtPosition(placementManager, positionToAdapt);
        }

    }
}
