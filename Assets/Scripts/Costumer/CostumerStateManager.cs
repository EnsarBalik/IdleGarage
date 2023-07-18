using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class CostumerStateManager : MonoBehaviour
{
    public CostumerBaseState CurrentState;
    public readonly CostumerMoveTaskPosState MoveTaskPosState = new CostumerMoveTaskPosState();
    public readonly CostumerTaskDoneState TaskDoneState = new CostumerTaskDoneState();
    public readonly CostumerReturnAreaState ReturnAreaState = new CostumerReturnAreaState();
    public readonly CostumerThiefCaughtState ThiefCaughtState = new CostumerThiefCaughtState();

    public NavMeshAgent _navMeshAgent;
    public SalesAreaController taskLocations;

    public Animator animatorController;
    [SerializeField] public Transform movePositionTransform;
    public Transform product;
    public Transform costumerArea;
    public bool isWalkDone;

    private const float CoolDownSec = 2f;
    private bool _isCoolDown = true;
    [SerializeField] private bool thief;
    private float _fillImage;
    public ParticleSystem thiefEffect;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        CurrentState = MoveTaskPosState;
        CurrentState.EnterState(this);
    }

    private void Update()
    {
        CurrentState.UpdateState(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CollectArea1"))
        {
            CurrentState.TriggerEnter(this, other);
        }

        if (other.gameObject.CompareTag("Costumer Area"))
        {
            SwitchState(MoveTaskPosState);
            ThiefEscaped();
        }

        if (other.gameObject.CompareTag("Player") && thief)
        {
            thief = false;
            ThiefCaught();
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void BuyProduct()
    {
        if (!_isCoolDown)
        {
            _isCoolDown = true;
            taskLocations.occupied = false;
            int randomInt = Random.RandomRange(0, 2);
            if (randomInt == 1)
            {
                GameManager.instance.CollectCoin(Random.Range(100, 200));
            }
            else
            {
                ThiefDetected();
            }

            SwitchState(ReturnAreaState);
            Destroy(product.gameObject);
        }

        if (_isCoolDown)
        {
            _fillImage += 1 / CoolDownSec * Time.deltaTime;
            if (_fillImage >= 1)
            {
                _fillImage = 0;
                _isCoolDown = false;
            }
        }
    }

    public void TaskIsDone()
    {
        _navMeshAgent.destination = costumerArea.position;
        isWalkDone = false;
        animatorController.SetBool("Walking", true);
    }

    public void SwitchState(CostumerBaseState state)
    {
        CurrentState = state;
        state.EnterState(this);
    }

    private void ThiefCaught()
    {
        transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color = Color.green;
        thiefEffect.Play();
        thief = false;
        _navMeshAgent.isStopped = true;
        GameManager.instance.CollectCoin(Random.Range(100, 200));
        animatorController.SetBool("Walking", false);
        PlayerMove.instance.baseballBat.SetActive(false);
        var valuableList = ValueController.instance.valuableList;
        for (int i = 0; i < valuableList.Count; i++)
        {
            valuableList[i].gameObject.SetActive(true);
        }
        SwitchState(ThiefCaughtState);
    }

    private void ThiefEscaped()
    {
        transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color = Color.green;
        thief = false;
        PlayerMove.instance.baseballBat.SetActive(false);
        var valuableList = ValueController.instance.valuableList;
        for (int i = 0; i < valuableList.Count; i++)
        {
            valuableList[i].gameObject.SetActive(true);
        }
    }

    private void ThiefDetected()
    {
        thief = true;
        transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color = Color.red;
        PlayerMove.instance.baseballBat.SetActive(true);
        var valuableList = ValueController.instance.valuableList;
        for (int i = 0; i < valuableList.Count; i++)
        {
            valuableList[i].gameObject.SetActive(false);
        }

        Debug.Log("Thief Detected");
    }

    public IEnumerator Timer()
    {
        yield return new WaitForSeconds(4f);
        _navMeshAgent.isStopped = false;
        animatorController.SetBool("Fallen", false);
        animatorController.SetBool("Walking", true);
        SwitchState(ReturnAreaState);
    }
}