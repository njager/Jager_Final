﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    //public variables
    public GameObject target;
    public string triggerVariable;
    public AudioClip SFXplatform;

    //private variables
    private PlayerController player;
    private Animator targetAnimator;
    private bool isPlayerRestored;
    AudioSource triggerSource;

    // Start is called before the first frame update
    void Start()
    {
        targetAnimator = target.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        triggerSource = GetComponent<AudioSource>();

        if(triggerVariable == null)
        {
            Debug.LogWarning(name + " has no supplied trigger variable. Nothing will be triggered");
        }
    }

    //toggles the animator parameter and parents player to moving platform
    void TriggerAction()
    {
        bool targetVar = targetAnimator.GetBool(triggerVariable);
        targetAnimator.SetBool(triggerVariable, !targetVar);
        triggerSource.Play();

        player.EnableCleanMove(target.transform);
        isPlayerRestored = false;
    }

    //reverses trigger action
    void RestorePlayer()
    {
        player.DisableCleanMove();
        isPlayerRestored = true;
    }

    //when player enters trigger, call trigger action
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerAction();
        }
    }

    //determines whether player is ready to be enabled ; called last every frame
    private void LateUpdate()
    {
        if (targetAnimator.IsInTransition(0))
        {
            //Debug.Log("Player activated trigger " + name);
        }
        else
        {
            if (!isPlayerRestored)
            {
                RestorePlayer();
            }
            /*else
            {
                Debug.Log("Player is restored by " + name);
            }*/
        }
    }

}
