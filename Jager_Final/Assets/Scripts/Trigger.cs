using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    //public variables
    public GameObject target;
    public string triggerVariable;

    //private variables
    private PlayerController player;
    private Animator targetAnimator;
    private bool isPlayerRestored;

    // Start is called before the first frame update
    void Start()
    {
        targetAnimator = target.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

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

        player.EnableCleanMove(target.transform);
        isPlayerRestored = false;
    }

    //reverses trigger aciton
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
