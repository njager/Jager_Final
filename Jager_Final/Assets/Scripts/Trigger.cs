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

    // Update is called once per frame
    void Update()
    {
        
    }
}
