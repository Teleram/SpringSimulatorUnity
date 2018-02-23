using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class AttackScript : NetworkBehaviour {

    public SelectScript selectScript;

    public LivingScript livingScript;
    
    public GameObject target;

    public float attackRange;
    public float attackCooldown;
    public int attackDamage;
    public float remainingAttackCooldown;

    private GameMasterScript gameMasterScript;

	// Use this for initialization
	void Start () 
    {
        GameObject gameMaster = GameObject.Find("GameMaster");
        gameMasterScript = gameMaster.GetComponent<GameMasterScript>();

        remainingAttackCooldown = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (hasAuthority && gameMasterScript.gameIsRunning())
        {

            if (Input.GetMouseButtonDown(1) && selectScript.selected)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 1000) && hit.collider.tag == "Player")
                {
                    target = hit.collider.transform.gameObject;                    
                }
                else
                {
                    target = null;
                }               
            }
        }

        if (hasTarget() && !targetIsFriendly())
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

            if ((remainingAttackCooldown <= 0) && (distanceToTarget <= attackRange))
            {
                remainingAttackCooldown = attackCooldown;

                int targetId = target.GetComponent<LivingScript>().myObjectId;
                CmdAttack(attackDamage, targetId);
            }
        }

        if(remainingAttackCooldown > 0)
        {
            remainingAttackCooldown -= Time.deltaTime;
        }
	}

    // check if target is null
    public bool hasTarget()
    {
        if(target == null)
        {
            return false;
        }
        return true;
    }

    // if there is no target, the target is friendly
    public bool targetIsFriendly()
    {
        if (hasTarget())
        {
            LivingScript targetScript = target.GetComponent<LivingScript>();

            if (livingScript.myPlayerId == targetScript.myPlayerId)
            {
                return true;
            }

            return false;
        }

        return true;
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
