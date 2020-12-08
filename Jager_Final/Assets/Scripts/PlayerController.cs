using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    //member variables
    private NavMeshAgent agent;
    private RaycastHit clickInfo = new RaycastHit();
    private Vector3 storedTarget;
    private float endTimer = 0;
    private bool playerWon;

    //public variables
    public float interpolantValue = 100;
    public float disconnectMargin = 1.5f;
    public ParticleSystem particleSnow;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        endTimer = 0;
        playerWon = false;
    }

    // Update is called once per frame
    void Update()
    {
        storedTarget = agent.pathEndPosition;

        //checks for mouse input and location, then sends player to mouse click
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out clickInfo))
                agent.destination = clickInfo.point;
        }

        //if player is on the link, call link function
        if (agent.isOnOffMeshLink)
        {
            CompleteLink();
        }
        //check if it's been long enough since player finished the level
        if (playerWon == true)
        {
            endTimer += Time.deltaTime;
            if (endTimer >= 3f)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    //performs the link transition between platforms using LERP
    void CompleteLink()
    {
        //variables for transition
        Vector3 startLink = agent.currentOffMeshLinkData.startPos;
        Vector3 endLink = agent.currentOffMeshLinkData.endPos;
        float linkDistance = Vector3.Distance(startLink, endLink);
        endLink.y = agent.currentOffMeshLinkData.endPos.y + 1;

        //sets player's new position to other link end via Linear Interpolation, which creates a smooth transition
        transform.position = Vector3.Lerp(transform.position, endLink, linkDistance / interpolantValue);

        //completes the actual link movement as LERP just gets nearer and nearer to end without reaching
        if (Vector3.Distance(transform.position, endLink) < disconnectMargin)
        {
            agent.Warp(endLink);
            agent.SetDestination(storedTarget);
        }
    }

    //to be called by the envrionment when motion is triggered
    public void EnableCleanMove(Transform parentTransform)
    {
        transform.SetParent(parentTransform);
        agent.enabled = false;
    }

    //called when motion is ended
    public void DisableCleanMove()
    {
        transform.SetParent(null);
        agent.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Win"))
        {
            print("Good job! You beat the level.");
            particleSnow.Play();
            playerWon = true;
        }
    }
}
