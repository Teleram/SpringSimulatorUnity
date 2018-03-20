using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript_Singleplayer : MonoBehaviour {

    private TargetingScript_Singleplayer targetingScript;

    public float attackRange;
    public float attackCooldown;
    public int attackDamage;
    private float remainingAttackCooldown;

    private GameMasterScript_Singleplayer gameMasterScript;

    // Use this for initialization
    void Start()
    {
        targetingScript = GetComponent<TargetingScript_Singleplayer>();

        GameObject gameMaster = GameObject.FindGameObjectWithTag("GameMaster");
        gameMasterScript = gameMaster.GetComponent<GameMasterScript_Singleplayer>();

        remainingAttackCooldown = 0;
        //Debug.Log(transform.position);
    }

    // Update is called once per frame
    void Update()
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
                    myTarget.GetComponent<LivingScript_Singleplayer>().takeDamage(attackDamage);
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
