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

    public bool playPauseSmoke;
    
    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        rb.velocity = new Vector3(joystick.Horizontal * moveSpeed, rb.velocity.y, joystick.Vertical * moveSpeed);
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            playPauseSmoke = true;
            playerAnimator.speed = rb.velocity.magnitude / 10;
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
            playerAnimator.SetBool("Running", false);
            playerAnimator.SetBool("CarryRun", false);
            playerAnimator.speed = 1;
        }
        
        playerAnimator.SetBool("IdleCarry", ValueController.instance.valuableList.Count > 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Costumer"))
        {
            playerAnimator.SetBool("Attack", true);
        }
    }
}