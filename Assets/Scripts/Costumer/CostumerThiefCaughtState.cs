using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumerThiefCaughtState : CostumerBaseState
{
    public override void EnterState(CostumerStateManager costumer)
    {
        costumer.animatorController.SetBool("Fallen",true);
        costumer.StartCoroutine(costumer.Timer());
    }

    public override void UpdateState(CostumerStateManager costumer)
    {
        
    }

    public override void TriggerEnter(CostumerStateManager costumer, Collider collision)
    {
        
    }
}
