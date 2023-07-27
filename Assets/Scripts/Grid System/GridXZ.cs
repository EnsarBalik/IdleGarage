using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GridXZ : MonoBehaviour
{
    [SerializeField] private Camera sceneCam;

    private Vector3 lastPosition;

    [SerializeField] private LayerMask placementLayerMask;

    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = sceneCam.nearClipPlane;
        Ray ray = sceneCam.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, placementLayerMask))
        {
            lastPosition = hit.point;
        }

        return lastPosition;
    }
}
