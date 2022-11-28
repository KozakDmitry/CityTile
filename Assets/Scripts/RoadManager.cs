using SVS;
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

    private Vector3Int startPosition;
    private bool placemode = false;
    
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
        if(placemode == false)
        {
            AdjasentPositionsToAdapt.Clear();
            temporaryPlacementPos.Clear();
            placemode = true;
            startPosition = position;
            
            temporaryPlacementPos.Add(position);
            placementManager.PlaceTemporary(position, roadAdapter.deadEnd, CellType.Road);
          
        }
        else
        {
            placementManager.RemoveAllTempStructures();
            temporaryPlacementPos.Clear();
            AdjasentPositionsToAdapt.Clear();

            temporaryPlacementPos = placementManager.GetPathBetween(startPosition, position);
            foreach(var pos in temporaryPlacementPos)
            {
                if (placementManager.CheckIfPositionIsFree(pos) == false)
                {
                    continue;
                }
                placementManager.PlaceTemporary(pos, roadAdapter.deadEnd, CellType.Road);
                
            }
        }
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
                if (AdjasentPositionsToAdapt.Contains(roadPosition) == false)
                {
                    AdjasentPositionsToAdapt.Add(roadPosition);
                }
              
            }
        }
        foreach(var positionToAdapt in AdjasentPositionsToAdapt)
        {
            roadAdapter.FixRoadAtPosition(placementManager, positionToAdapt);
        }

    }

    public void FinishPlaceMode()
    {
        placemode = false;
        placementManager.AddTempStructuresToStructDict();
        if (temporaryPlacementPos.Count > 0)
        {
            AudioPlayer.instance.PlayPlacementSound();
        }
        temporaryPlacementPos.Clear();
        startPosition = Vector3Int.zero;
    }
}
