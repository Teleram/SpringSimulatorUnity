using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class MoveScript : NetworkBehaviour {
    private TargetingScript targetingScript;
    private AttackScript attackScript;

    private NavMeshAgent agent;
    
    private GameMasterScript gameMasterScript;

	// Use this for initialization
    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        targetingScript = GetComponent<TargetingScript>();
        attackScript = GetComponent<AttackScript>();

        GameObject gameMaster = GameObject.Find("GameMaster");
        gameMasterScript = gameMaster.GetComponent<GameMasterScript>();
    }
	
	// Update is called once per frame
	void Update () 
	{
        if (hasAuthority && gameMasterScript.GameIsRunning())
        {
            if (targetingScript.destinationHasChanged)
            {
                targetingScript.destinationHasChanged = false;
                agent.destination = targetingScript.myDestination;
                if (targetingScript.HasTarget())
                {
                    if (attackScript != null)
                    {
                        agent.stoppingDistance = attackScript.attackRange;
                    }
                }
                else
                {
                    agent.stoppingDistance = 1.0f;
                }
            }
        }

        if(gameMasterScript.GameTimerOver())
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
