using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject mouseTest;

    [SerializeField] private GridXZ inputManager;

    [SerializeField] private Grid grid;

    [SerializeField] private ObjectsDatabaseSO database;
    private int selectedObjectIndex = -1;
    [SerializeField] private GameObject gridVisualization;

    private GridData floorData, furnitureData;

    [SerializeField] private PreviewSystem preview;

    private Vector3Int lastDetectedPosition = Vector3Int.zero;

    private bool isRemoving;

    [SerializeField] private ObjectPlacer _objectPlacer;
    
    private void Start()
    {
        StopPlacement();
        floorData = new();
        furnitureData = new();
    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        selectedObjectIndex = database.ObjectDatas.FindIndex(data => data.ID == ID);
        if (selectedObjectIndex < 0)
        {
            Debug.LogError($"NO ID FOUND {ID} ");
            return;
        }

        gridVisualization.SetActive(true);
        preview.StartShowingPlacementPreview(database.ObjectDatas[selectedObjectIndex].Prefab,
            database.ObjectDatas[selectedObjectIndex].Size);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    private void PlaceStructure()
    {
        if (inputManager.IsPointerOverUI())
        {
            return;
        }

        Vector3 mousePos = inputManager.GetSelectedMapPosition();
        Vector3Int gridPos = grid.WorldToCell(mousePos);

        bool placementValidity = CheckPlacementValidity(gridPos, selectedObjectIndex);
        if (!placementValidity)
            return;

        int index = _objectPlacer.PlaceObject(database.ObjectDatas[selectedObjectIndex].Prefab, grid.CellToWorld(gridPos));
        
        GridData selectedData = database.ObjectDatas[selectedObjectIndex].ID == 0 ? floorData : furnitureData;
        selectedData.AddObjectAt(gridPos, database.ObjectDatas[selectedObjectIndex].Size,
            database.ObjectDatas[selectedObjectIndex].ID, index);
        preview.UpdatePosition(grid.CellToWorld(gridPos), false);
    }

    private bool CheckPlacementValidity(Vector3Int gridPos, int i)
    {
        GridData selectedData = database.ObjectDatas[selectedObjectIndex].ID == 0 ? floorData : furnitureData;

        return selectedData.CanPlaceObjectAt(gridPos, database.ObjectDatas[selectedObjectIndex].Size);
    }

    private void StopPlacement()
    {
        selectedObjectIndex = -1;
        gridVisualization.SetActive(false);
        preview.StopShowingPreview();
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
        lastDetectedPosition = Vector3Int.zero;
    }

    private void Update()
    {
        if (selectedObjectIndex < 0)
            return;
        Vector3 mousePos = inputManager.GetSelectedMapPosition();
        Vector3Int gridPos = grid.WorldToCell(mousePos);
        
        if (lastDetectedPosition == gridPos) return;
        bool placementValidity = CheckPlacementValidity(gridPos, selectedObjectIndex);

        mouseTest.transform.position = mousePos;
        preview.UpdatePosition(grid.CellToWorld(gridPos), placementValidity);
        lastDetectedPosition = gridPos;
    }
}