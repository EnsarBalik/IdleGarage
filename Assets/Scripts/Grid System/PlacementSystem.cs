using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject mouseTest, cellIndicator;
    [SerializeField] private GridXZ gridXZ;
    [SerializeField] private Grid grid;

    private void Update()
    {
        Vector3 mousePos = gridXZ.GetSelectedMapPosition();
        Vector3Int gridPos = grid.WorldToCell(mousePos);
        mouseTest.transform.position = mousePos;
        cellIndicator.transform.position = grid.CellToWorld(gridPos);
    }
}
