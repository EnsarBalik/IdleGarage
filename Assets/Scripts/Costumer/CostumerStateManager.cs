using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CostumerStateManager : MonoBehaviour
{
    private CostumerBaseState currentState;
    private readonly CostumerAssignmentState _assignmentState = new CostumerAssignmentState();

    public NavMeshAgent _navMeshAgent;

    public Animator animatorController;

    [SerializeField] public Transform movePositionTransform;
    public SalesAreaController taskLocations;
    public bool isWalkDone;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        currentState = _assignmentState;
    }

    private void Update()
    {
        if (taskLocations.occupied)
        {
            if (!isWalkDone)
            {
                _navMeshAgent.destination = movePositionTransform.position;
                animatorController.SetBool("Walking", true);
            }
            else
            {
                animatorController.SetBool("Walking", false);
                transform.rotation = movePositionTransform.rotation;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CollectArea1"))
        {
            isWalkDone = true;
        }
    }

    // public void SwitchState(CostumerBaseState state)
    // {
    //     currentState = state;
    //     state.EnterState(this);
    // }
}