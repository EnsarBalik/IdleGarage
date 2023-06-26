using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumerReturnAreaState : CostumerBaseState
{
    public override void EnterState(CostumerStateManager costumer)
    {
        Debug.Log("Costumer Return Area State");
    }

    public override void UpdateState(CostumerStateManager costumer)
    {
        costumer.TaskIsDone();
    }

    public override void TriggerEnter(CostumerStateManager costumer, Collider collision)
    {
        
    }
}
