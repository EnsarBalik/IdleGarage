using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CostumerManager : MonoBehaviour
{
    public List<CostumerStateManager> costumerList;


    private void Start()
    {
        for (int i = 0; i < costumerList.Count; i++)
        {
            costumerList[i].gameObject.SetActive(false);
        }
    }

    public void FindSaleArea(SalesAreaController areaController, Transform standHere)
    {
        CostumerStateManager costumer = default;
        
        foreach (var t in costumerList.Where(t => !t.isOccupied))
        {
            costumer = t;
            break;
        }

        if (costumer != null)
        {
            costumer.taskLocations = areaController;
            costumer.movePositionTransform = standHere;
            costumer.isOccupied = true;
            costumer.gameObject.SetActive(true);
        }
    }
}
