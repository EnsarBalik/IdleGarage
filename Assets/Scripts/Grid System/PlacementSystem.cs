using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Serialization;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private GridXZ inputManager;

    [SerializeField] private Grid grid;

    [SerializeField] private ObjectsDatabaseSO database;
    [SerializeField] private GameObject gridVisualization;
    [SerializeField] private GameObject buildPanel;
    public GameObject player;

    private GridData floorData, furnitureData;

    [SerializeField] private PreviewSystem preview;

    private Vector3 lastDetectedPosition;

    [SerializeField] private ObjectPlacer _objectPlacer;

    private IBuildingState buildingState;

    private void Start()
    {
        StopPlacement();
        floorData = new();
        furnitureData = new();
    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        gridVisualization.SetActive(true);

        buildingState = new PlacementState(ID, grid, preview, database, floorData, furnitureData, _objectPlacer);

        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
        buildPanel.SetActive(true);
    }

    public void PlaceStructure()
    {
        Vector3 mousePos = inputManager.GetSelectedMapPosition();
        Vector3Int gridPos = grid.WorldToCell(mousePos);

        buildingState.OnAction(gridPos);
    }

    // private bool CheckPlacementValidity(Vector3Int gridPos, int i)
    // {
    //     GridData selectedData = database.ObjectDatas[selectedObjectIndex].ID == 0 ? floorData : furnitureData;
    //
    //     return selectedData.CanPlaceObjectAt(gridPos, database.ObjectDatas[selectedObjectIndex].Size);
    // }

    public void StopPlacement()
    {
        //if (buildingState == null) return;
        gridVisualization.SetActive(false);
        preview.StopShowingPreview();
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
        buildPanel.SetActive(false);
        lastDetectedPosition = player.transform.position;
        buildingState = null;
    }

    private void Update()
    {
        if (buildingState == null) return;
        Vector3 mousePos = inputManager.GetSelectedMapPosition();
        Vector3Int gridPos = grid.WorldToCell(mousePos);

        if (lastDetectedPosition == gridPos) return;
        buildingState.UpdateState(gridPos);
        lastDetectedPosition = gridPos;
    }
}