using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class AttackScript : NetworkBehaviour {

    private TargetingScript targetingScript;

    public float attackRange;
    public float attackCooldown;
    public int attackDamage;
    private float remainingAttackCooldown;

    private GameMasterScript gameMasterScript;

	// Use this for initialization
	void Start () 
    {
        targetingScript = GetComponent<TargetingScript>();

        GameObject gameMaster = GameObject.Find("GameMaster");
        gameMasterScript = gameMaster.GetComponent<GameMasterScript>();

        remainingAttackCooldown = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (gameMasterScript.gameIsRunning())
        {
            if (targetingScript.hasTarget() && !targetingScript.targetIsFriendly())
            {
                float distanceToTarget = targetingScript.distanceToTarget();

                if ((remainingAttackCooldown <= 0) && (distanceToTarget <= attackRange))
                {
                    remainingAttackCooldown = attackCooldown;

                    int targetId = targetingScript.target.GetComponent<LivingScript>().myObjectId;
                    CmdAttack(attackDamage, targetId);
                }
            }

            if (remainingAttackCooldown > 0)
            {
                remainingAttackCooldown -= Time.deltaTime;
            }
        }
	}

        
    [Command]
    private void CmdAttack(int damage, int targetId)
    {
        RpcAttack(damage, targetId);
    }

    [ClientRpc]
    private void RpcAttack(int damage, int targetId)
    {
        GameObject myTarget = gameMasterScript.getObjectById(targetId);
        myTarget.GetComponent<LivingScript>().takeDamage(damage);
    }
}
