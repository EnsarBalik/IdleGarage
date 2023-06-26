using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    private PlayerMove _playerMove;

    public ParticleSystem dustEffect;

    private void Start()
    {
        _playerMove = gameObject.GetComponent<PlayerMove>();

        InvokeRepeating(nameof(StartSmoke), 0.1f, 1f);
    }

    private void LateUpdate()
    {
    }

    private void StartSmoke()
    {
        switch (_playerMove.playPauseSmoke)
        {
            case true:
                dustEffect.Play();
                var dustEffectMain = dustEffect.main;
                dustEffectMain.startSpeed = _playerMove.GetComponent<Rigidbody>().velocity.magnitude / 10;
                break;
            case false:
                dustEffect.Stop();
                break;
        }
    }

    private void StopSmoke()
    {
        if (!_playerMove.playPauseSmoke)
        {
            dustEffect.Stop();
        }
    }
}