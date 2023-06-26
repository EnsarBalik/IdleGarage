using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CostumerStateManager : MonoBehaviour
{
    public static CostumerStateManager instance;

    public CostumerBaseState currentState;
    public readonly CostumerMoveTaskPosState MoveTaskPosState = new CostumerMoveTaskPosState();
    public readonly CostumerTaskDoneState _taskDoneState = new CostumerTaskDoneState();

    public NavMeshAgent _navMeshAgent;

    public Animator animatorController;

    [SerializeField] public Transform movePositionTransform;
    public Transform product;
    public Transform costumerArea;
    public SalesAreaController taskLocations;
    public bool isWalkDone;

    private float _coolDownSec = 2f;
    private bool _isCoolDown = true;
    private float _fillImage;

    private void Start()
    {
        instance = this;

        _navMeshAgent = GetComponent<NavMeshAgent>();

        currentState = MoveTaskPosState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);

        // if (taskLocations.occupied)
        // {
        //     if (!isWalkDone)
        //     {
        //         _navMeshAgent.destination = movePositionTransform.position;
        //         animatorController.SetBool("Walking", true);
        //     }
        //     else
        //     {
        //         animatorController.SetBool("Walking", false);
        //         transform.rotation = movePositionTransform.rotation;
        //         BuyProduct();
        //     }
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.EnterState(this);
        if (other.gameObject.CompareTag("CollectArea1"))
        {
            isWalkDone = true;
            product = other.gameObject.transform.GetChild(1).transform.GetChild(1).transform;
        }
    }

    private void BuyProduct()
    {
        if (!_isCoolDown)
        {
            _isCoolDown = true;
            product.gameObject.SetActive(false);
            TaskIsDone();
        }

        if (_isCoolDown)
        {
            _fillImage += 1 / _coolDownSec * Time.deltaTime;
            if (_fillImage >= 1)
            {
                _fillImage = 0;
                _isCoolDown = false;
            }
        }
    }

    void AssignTask()
    {
        _navMeshAgent.destination = movePositionTransform.position;
    }

    private void TaskIsDone()
    {
        _navMeshAgent.destination = costumerArea.position;
        isWalkDone = false;
        animatorController.SetBool("Walking", true);
    }

    public void SwitchState(CostumerBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}