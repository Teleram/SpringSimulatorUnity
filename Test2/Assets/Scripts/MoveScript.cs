using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class MoveScript : NetworkBehaviour {

    public SelectScript selectScript;
    
    private NavMeshAgent agent;
    private AttackScript attackScript;

    public Vector3 myDestination;

    private GameMasterScript gameMasterScript;

	// Use this for initialization
    void Start ()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        attackScript = gameObject.GetComponent<AttackScript>();

        GameObject gameMaster = GameObject.Find("GameMaster");
        gameMasterScript = gameMaster.GetComponent<GameMasterScript>();
    }
	
	// Update is called once per frame
	void Update () 
	{
        if (hasAuthority && gameMasterScript.gameIsRunning())
        {

            if (attackScript.hasTarget())
            {
                agent.stoppingDistance = attackScript.attackRange;
                if (attackScript.target.transform.position != myDestination)
                {
                    myDestination = attackScript.target.transform.position;
                    agent.destination = myDestination;
                    //CmdUpdateDestination(myDestination);
                }
            }
            else
            {
                agent.stoppingDistance = 1.0f;
            }

            if (Input.GetMouseButtonDown(1) && selectScript.selected)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 1000) && hit.collider.tag == "Terrain")
                {
                    myDestination = hit.point;
                    agent.destination = myDestination;
                    //CmdUpdateDestination(myDestination);
                }
            }
        }

        if(gameMasterScript.gameTimerOver())
        {
            agent.isStopped = true;
        }
	}



    //[Command]
    //private void CmdUpdateDestination(Vector3 newDestination)
    //{
    //    myDestination = newDestination;
    //    agent.destination = myDestination;

    //    RpcUpdateDestination(myDestination);
    //}

    //[ClientRpc]
    //private void RpcUpdateDestination(Vector3 newDestination)
    //{
    //    myDestination = newDestination;
    //    agent.destination = myDestination;
    //}
}
