using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CostumerManager : MonoBehaviour
{
    public List<CostumerStateManager> costumerList;

    public void Test(SalesAreaController sac, Transform standHere)
    {
        CostumerStateManager costumer = default;
        
        foreach (var t in costumerList.Where(t => t.gameObject.activeInHierarchy && !t.isOccupied))
        {
            costumer = t;
            break;
        }

        if (costumer != null && costumer.gameObject.activeSelf)
        {
            costumer.taskLocations = sac;
            costumer.movePositionTransform = standHere;
            costumer.isOccupied = true;
        }
    }
}
