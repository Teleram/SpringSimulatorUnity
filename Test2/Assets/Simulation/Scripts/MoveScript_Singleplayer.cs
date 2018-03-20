using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveScript_Singleplayer : MonoBehaviour {

    private TargetingScript_Singleplayer targetingScript;
    private AttackScript_Singleplayer attackScript;

    private NavMeshAgent agent;

    private GameMasterScript_Singleplayer gameMasterScript;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        targetingScript = GetComponent<TargetingScript_Singleplayer>();
        attackScript = GetComponent<AttackScript_Singleplayer>();

        GameObject gameMaster = GameObject.FindGameObjectWithTag("GameMaster");
        gameMasterScript = gameMaster.GetComponent<GameMasterScript_Singleplayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMasterScript.GameIsRunning())
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

        if (gameMasterScript.GameTimerOver())
        {
            agent.isStopped = true;
        }
    }
}
