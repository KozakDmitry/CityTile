using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    [SerializeField]
    private PlacementManager placementManager;

    public List<Vector3Int> temporaryPlacementPos = new List<Vector3Int>();

    public GameObject roadStraight;

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
        placementManager.PlaceTemporary(position, roadStraight, CellType.Road);
    }

}
