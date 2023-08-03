using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumerMoveTaskPosState : CostumerBaseState
{
    public override void EnterState(CostumerStateManager costumer)
    {
        Debug.Log("Costumer Move Task State");
    }

    public override void UpdateState(CostumerStateManager costumer)
    {
        if (costumer.taskLocations == null && costumer.movePositionTransform == null) return;
        if (!costumer.taskLocations.occupied && costumer.isWalkDone) return;
        costumer._navMeshAgent.destination = costumer.movePositionTransform.position;
        costumer.animatorController.SetBool("Walking", true);
    }

    public override void TriggerEnter(CostumerStateManager costumer, Collider collision)
    {
        if (!collision.gameObject.CompareTag("CollectArea1")) return;
        costumer.product = collision.gameObject.transform.GetChild(1);
        costumer.isWalkDone = true;
        costumer.SwitchState(costumer.TaskDoneState);
    }
}