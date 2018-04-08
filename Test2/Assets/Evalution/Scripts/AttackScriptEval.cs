using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScriptEval : MonoBehaviour
{

    private TargetingScriptEval targetingScript;

    public float attackRange;
    public float attackCooldown;
    public int attackDamage;
    private float remainingAttackCooldown;

    private GameMasterScript_Singleplayer gameMasterScript;

    // Use this for initialization
    void Start()
    {
        targetingScript = GetComponent<TargetingScriptEval>();

        GameObject gameMaster = GameObject.FindGameObjectWithTag("GameMaster");
        gameMasterScript = gameMaster.GetComponent<GameMasterScript_Singleplayer>();

        remainingAttackCooldown = 0;
        //Debug.Log(transform.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameMasterScript.GameIsRunning())
        {
            if (targetingScript.HasTarget() && !targetingScript.TargetIsFriendly())
            {
                float distanceToTarget = targetingScript.DistanceToTarget();

                if ((remainingAttackCooldown <= 0) && (distanceToTarget <= attackRange))
                {
                    remainingAttackCooldown = attackCooldown;

                    int targetId = targetingScript.target.GetComponent<LivingScript_Singleplayer>().myObjectId;

                    GameObject myTarget = gameMasterScript.GetObjectById(targetId);
                    myTarget.GetComponent<LivingScript_Singleplayer>().TakeDamage(attackDamage);
                }
            }

            if (remainingAttackCooldown > 0)
            {
                remainingAttackCooldown -= Time.deltaTime;
            }
            //Debug.Log(transform.position);
        }
    }
}