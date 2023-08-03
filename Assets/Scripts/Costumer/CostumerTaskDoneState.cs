using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumerTaskDoneState : CostumerBaseState
{
    public override void EnterState(CostumerStateManager costumer)
    {
        Debug.Log("Costumer Task Done State");
        costumer.transform.rotation = costumer.movePositionTransform.rotation;
        if (costumer.isWalkDone)
        {
            costumer.animatorController.SetBool("Walking", false);
        }
    }

    public override void UpdateState(CostumerStateManager costumer)
    {
        costumer.BuyProduct();
    }

    public override void TriggerEnter(CostumerStateManager costumer, Collider collision)
    {
        
    }
}
