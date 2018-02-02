using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class MoveScript : NetworkBehaviour {

	//public Transform pointer;
	public SelectScript selectScript;
    //public SpriteRenderer cursor;

    public GameObject me;
    private NavMeshAgent agent;

    // for Debug
    public Vector3 myDestination;



	// Use this for initialization
    void Start () {
        if (!hasAuthority)
            return;

        agent = (NavMeshAgent)me.GetComponent("NavMeshAgent");
        myDestination = me.transform.position;
        agent.destination = myDestination;
    }
	
	// Update is called once per frame
	void Update () 
	{
        if (!hasAuthority)
            return;

        if (Input.GetMouseButtonDown( 1 ) && selectScript.selected)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if(Physics.Raycast(ray, out hit, 1000) && hit.collider.tag == "Terrain")
			{
                myDestination = hit.point;
                agent.destination = myDestination;				
			}
		}

        if(myDestination == me.transform.position)
        {
            agent.isStopped = true;
        }
	}
    
}
