using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Value : MonoBehaviour
{
    public int ID;
    public bool isCollected;

    private void Update()
    {
        if (isCollected)
        {
            var playerRot = PlayerMove.instance.transform.rotation;
            var rot = transform.rotation;
            rot = new Quaternion(playerRot.x, playerRot.y, playerRot.z, 1);
            transform.rotation = rot;
        }
    }
}
