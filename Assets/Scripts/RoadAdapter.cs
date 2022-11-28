using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadAdapter : MonoBehaviour
{
    
    public GameObject deadEnd, roadStr, corner, fourWay, threeWay;
    


    public void FixRoadAtPosition(PlacementManager placementManager, Vector3Int tempPosition)
    {

        var result = placementManager.GetNeighboursTypesFor(tempPosition);
        int roadCount = 0;
        roadCount = result.Where(x => x == CellType.Road).Count();
        switch (roadCount)
        {
            case 2:
                if (CreateStraightRoad(placementManager, result, tempPosition))
                    return;
                else
                    CreateCorner(placementManager, result, tempPosition);
                break;
            case 3:
                CreateThreeWayRoad(placementManager, result, tempPosition);
                break;
            case 4:
                CreateFourWayRoad(placementManager, result, tempPosition);
                break;
            default:
                CreateDeadEnd(placementManager, result, tempPosition);
                break;
                


        }
    }

    private void CreateCorner(PlacementManager placementManager, CellType[] result, Vector3Int tempPosition)
    {
        if (result[0] == CellType.Road && result[1] == CellType.Road)
        {
            placementManager.ModifyStructureModel(tempPosition, corner, Quaternion.identity);
        }
        else if (result[1] == CellType.Road && result[2] == CellType.Road)
        {
            placementManager.ModifyStructureModel(tempPosition, corner, Quaternion.Euler(0, 90, 0));
        }
        else if (result[2] == CellType.Road && result[3] == CellType.Road)
        {
            placementManager.ModifyStructureModel(tempPosition, corner, Quaternion.Euler(0, 180, 0));
        }
        else if (result[3] == CellType.Road && result[0] == CellType.Road)
        {
            placementManager.ModifyStructureModel(tempPosition, corner, Quaternion.Euler(0, 270, 0));
        }
    }

    private void CreateFourWayRoad(PlacementManager placementManager, CellType[] result, Vector3Int tempPosition)
    {
        placementManager.ModifyStructureModel(tempPosition, fourWay, Quaternion.identity);
    }

    private void CreateThreeWayRoad(PlacementManager placementManager, CellType[] result, Vector3Int tempPosition)
    {
        if (result[1] == CellType.Road && result[2] == CellType.Road && result[3] == CellType.Road)
        {
            placementManager.ModifyStructureModel(tempPosition, threeWay, Quaternion.identity);
        }
        else if (result[2] == CellType.Road && result[3] == CellType.Road && result[0] == CellType.Road)
        {
            placementManager.ModifyStructureModel(tempPosition, threeWay, Quaternion.Euler(0, 90, 0));
        }
        else if (result[3] == CellType.Road && result[0] == CellType.Road && result[1] == CellType.Road)
        {
            placementManager.ModifyStructureModel(tempPosition, threeWay, Quaternion.Euler(0, 180, 0));
        }
        else if (result[0] == CellType.Road && result[1] == CellType.Road && result[2] == CellType.Road)
        {
            placementManager.ModifyStructureModel(tempPosition, threeWay, Quaternion.Euler(0, 270, 0));
        }

    }

    private bool CreateStraightRoad(PlacementManager placementManager, CellType[] result, Vector3Int tempPosition)
    {
        if (result[0] == CellType.Road && result[2] == CellType.Road)
        {
            placementManager.ModifyStructureModel(tempPosition, roadStr, Quaternion.identity);
            return true;
        }
        else if (result[3] == CellType.Road && result[1] == CellType.Road)
        {
            placementManager.ModifyStructureModel(tempPosition, roadStr, Quaternion.Euler(0, 90, 0));
            return true;
        }
        return false;
    }

    private void CreateDeadEnd(PlacementManager placementManager, CellType[] result, Vector3Int tempPosition)
    {
        if (result[2] == CellType.Road)
        {
            placementManager.ModifyStructureModel(tempPosition, deadEnd, Quaternion.identity);
        }
        else if (result[3] == CellType.Road)
        {
            placementManager.ModifyStructureModel(tempPosition, deadEnd, Quaternion.Euler(0, 90, 0));
        }
        else if (result[0] == CellType.Road)
        {
            placementManager.ModifyStructureModel(tempPosition, deadEnd, Quaternion.Euler(0, 180, 0));
        }
        else if (result[1] == CellType.Road)
        {
            placementManager.ModifyStructureModel(tempPosition, deadEnd, Quaternion.Euler(0, 270, 0));
        }
    }
}
