using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculator : MonoBehaviour
{
    public GameObject create;

    public List<GameObject> testList;

    public float radius = 1f;
    public int amount;
    public float frequency = 0.1f;
    public int count;


    private void Update()
    {
        Calculate();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateObject();
        }
    }

    private void Calculate()
    {
        amount = testList.Count;

        for (int i = 0; i < amount; i++)
        {
            var angle = i * Mathf.PI * 2f / amount;
            float angleXYZ = Mathf.Sin(angle) * radius;
            var newPos = new Vector3(Mathf.Cos(angle) * radius, angleXYZ, angleXYZ);
            testList[i].transform.localPosition = newPos;
        }
    }

    private void CreateObject()
    {
        for (int i = 0; i < count; i++)
        {
            var test = Instantiate(create);
            test.transform.parent = transform;
            testList.Add(test);
        }
    }
}