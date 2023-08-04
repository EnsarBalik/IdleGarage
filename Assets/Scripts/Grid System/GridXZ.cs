using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridXZ : MonoBehaviour
{
    [SerializeField] private Camera sceneCam;

    private Vector3 lastPosition;

    [SerializeField] private LayerMask placementLayerMask;
    [SerializeField] private PlacementSystem _placementSystem;
    
    public GameObject player;

    public event Action OnClicked, OnExit;

    private void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        //     OnClicked?.Invoke();
        // if (Input.GetKeyDown(KeyCode.A))
        //     OnExit?.Invoke();
    }

    public bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();

    public Vector3 GetSelectedMapPosition()
    {
        var mousePos = player.transform.position;
        mousePos.z = sceneCam.nearClipPlane;
        Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y,
            player.transform.position.z + 4);
        Ray ray = sceneCam.ScreenPointToRay(playerPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, placementLayerMask))
        {
            lastPosition = playerPos;
        }

        return lastPosition;
    }

    public void PlacementStart()
    {
        _placementSystem.StartPlacement(0);
        PlayerMove.instance.playerAnimator.SetBool("Build", true);
    }

    public void PlacementStop()
    {
        _placementSystem.StopPlacement();
        PlayerMove.instance.playerAnimator.SetBool("Build", false);
    }
    
    int queue;
    public void StartStopPlacement()
    {
            
        if (queue == 0)
        {
            PlacementStart();
            queue = 1;
        }
        else if (queue == 1)
        {
            PlacementStop();
            queue = 0;
        }
    }
    
}