using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookatcamera : MonoBehaviour {

    Transform kampos;

    private void Start()
    {
        kampos = Camera.main.transform;
    }

    private void LateUpdate()
    {
        if (kampos != null)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(transform.position - new Vector3(kampos.position.x, transform.position.y, kampos.position.z)), 50f * Time.deltaTime);
    }

}
