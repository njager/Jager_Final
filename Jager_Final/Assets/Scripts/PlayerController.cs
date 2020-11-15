using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    //member variables
    private NavMeshAgent agent;
    private RaycastHit clickInfo = new RaycastHit();

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out clickInfo))
                agent.destination = clickInfo.point;
        }

        if (agent.isOnOffMeshLink)
        {
            agent.CompleteOffMeshLink();
        }
    }

    void CompleteLink()
    {
        Vector3 startLink = agent.currentOffMeshLinkData.startPos;
        Vector3 endLink = agent.currentOffMeshLinkData.endPos;
        float linkDistance = Vector3.Distance(startLink, endLink);
        endLink.y = agent.currentOffMeshLinkData.endPos.y + 1;
    }
}
