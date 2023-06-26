using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumerMoveTaskPosState : CostumerBaseState
{
    public override void EnterState(CostumerStateManager costumer)
    {
        Debug.Log("Test");
    }

    public override void UpdateState(CostumerStateManager costumer)
    {
        costumer._navMeshAgent.destination = costumer.movePositionTransform.position;
    }

    public override void TriggerEnter(CostumerStateManager costumer, Collider collision)
    {
        
    }
}