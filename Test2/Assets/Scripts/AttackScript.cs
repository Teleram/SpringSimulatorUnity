using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class AttackScript : NetworkBehaviour {

    public SelectScript selectScript;

    public LivingScript livingScript;
    
    public GameObject target;

    public GameObject bullet;
    public float attackRange;
    public float attackCooldown;
    public int attackDamage;
    public float remainingAttackCooldown;

	// Use this for initialization
	void Start () 
    {
        remainingAttackCooldown = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (hasAuthority)
        {

            if (Input.GetMouseButtonDown(1) && selectScript.selected)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 1000) && hit.collider.tag == "Player")
                {
                    if (hit.collider.transform.parent == null)
                    {
                        target = hit.collider.transform.gameObject;
                    }
                    else
                    {
                        target = hit.collider.transform.parent.gameObject;
                    }
                }
                else
                {
                    target = null;
                }               
            }
        }

        if (hasTarget() && !targetIsFriendly())
        {
            float distanceToTarget = Vector3.Distance(this.transform.position, target.transform.position);

            if ((remainingAttackCooldown <= 0) && (distanceToTarget <= attackRange))
            {
                remainingAttackCooldown = attackCooldown;
                CmdSpawnBullet(target.transform.position, attackDamage);
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
            LivingScript targetScript = (LivingScript)target.GetComponent("LivingScript");

            if (livingScript.myPlayerId == targetScript.myPlayerId)
            {
                return true;
            }

            return false;
        }

        return true;
    }

    [Command]
    private void CmdSpawnBullet(Vector3 position, int damage)
    {
        Quaternion rot = new Quaternion();
        GameObject newBullet = Instantiate(bullet, position, rot);
        newBullet.GetComponent<BulletScript>().damage = damage;

        NetworkServer.Spawn(newBullet);
    }
}
