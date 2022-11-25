using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{

    public int width, height;

    Grid placementGrid;

    private Dictionary<Vector3Int, StructureModel> tempRoadObjects = new Dictionary<Vector3Int, StructureModel>();

    private void Start()
    {
        placementGrid = new Grid(width, height);
    }
    internal bool CheckIfPositionIsFree(Vector3Int position)
    {
        return CheckIfPositionIsOffType(position, CellType.Empty);
    }

    private bool CheckIfPositionIsOffType(Vector3Int position, CellType type)
    {
        return placementGrid[position.x, position.z] == type;
    }

    internal bool CheckPositionInBound(Vector3Int position)
    {
        if(position.x >=0 && position.x<width &&position.z >=0 && position.z < height)
        {
            return true;
        }
        return false;
    }

    internal void PlaceTemporary(Vector3Int position, GameObject structurePrefab, CellType type)
    {
        placementGrid[position.x, position.z] = type;
        StructureModel structure = CreateNewStructureModel(position, structurePrefab,type);
        tempRoadObjects.Add(position, structure);
    }

    private StructureModel CreateNewStructureModel(Vector3Int position, GameObject structurePrefab, CellType type)
    {
        GameObject structure = new GameObject(type.ToString());
        structure.transform.SetParent(transform);
        structure.transform.localPosition = position;
        var structureModel = structure.AddComponent<StructureModel>();
        structureModel.CreateModel(structurePrefab);
        return structureModel;
    }


    private void ModifyStructureModel(Vector3Int position, GameObject newModel, Quaternion rotation)
    {
        if (tempRoadObjects.ContainsKey(position))
        {
            tempRoadObjects[position].SwapModel(newModel, rotation);
        }
    }
}
