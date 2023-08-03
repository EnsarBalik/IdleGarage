using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class PlayerMove : MonoBehaviour
{
    public static PlayerMove instance;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private DynamicJoystick joystick;
    [SerializeField] private float moveSpeed;

    public Animator playerAnimator;
    public GameObject baseballBat;

    public bool playPauseSmoke;

    public GameObject test;

    float Xaxiss;
    float Yaxiss;
    
    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        rb.velocity = new Vector3(joystick.Horizontal * moveSpeed, rb.velocity.y, joystick.Vertical * moveSpeed);
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            transform.rotation = quaternion.Euler(0f, 0f, 0f);
            
            playerAnimator.SetFloat("X axis", joystick.Horizontal);
            playerAnimator.SetFloat("Y axis", joystick.Vertical);

            Xaxiss = joystick.Horizontal;
            Yaxiss = joystick.Vertical;
            
            playPauseSmoke = true;
            playerAnimator.speed = rb.velocity.magnitude / 10;
            if (!playerAnimator.GetBool("Build"))
                transform.rotation = Quaternion.LookRotation(rb.velocity);
            if (ValueController.instance.valuableList.Count > 1)
            {
                playerAnimator.SetBool("Running", false);
                playerAnimator.SetBool("CarryRun", true);
            }
            else
            {
                playerAnimator.SetBool("Running", true);
                playerAnimator.SetBool("CarryRun", false);
            }
        }
        else
        {
            playPauseSmoke = false;
            
            if(Xaxiss > 0f && Xaxiss != 0f) Xaxiss -= Time.deltaTime;
            else if(Xaxiss != 0f) Xaxiss += Time.deltaTime;
            if(Yaxiss > 0f && Yaxiss != 0f) Yaxiss -= Time.deltaTime;
            else if (Yaxiss < 0f && Yaxiss != 0f) Yaxiss += Time.deltaTime;
            
            playerAnimator.SetFloat("X axis", Xaxiss);
            playerAnimator.SetFloat("Y axis", Yaxiss);
            playerAnimator.SetBool("Running", false);
            playerAnimator.SetBool("CarryRun", false);
            playerAnimator.speed = 1;
        }

        playerAnimator.SetBool("IdleCarry", ValueController.instance.valuableList.Count > 1);
    }
}