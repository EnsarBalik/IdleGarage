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

    public GameObject player;

    public event Action OnClicked, OnExit;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnClicked?.Invoke();
        if (Input.GetKeyDown(KeyCode.A))
            OnExit?.Invoke();
    }

    public bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();

    public Vector3 GetSelectedMapPosition()
    {
        var mousePos = player.transform.position;
        mousePos.z = sceneCam.nearClipPlane;
        Vector3 test = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        Ray ray = sceneCam.ScreenPointToRay(test);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, placementLayerMask))
        {
            lastPosition = hit.point;
        }

        return lastPosition;
    }
}