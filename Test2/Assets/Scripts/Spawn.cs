using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Spawn : NetworkBehaviour {

    private LivingScript livingScript;

    public float counter;
    public float spawnspeed;
    public Vector3 relativeSpawnpos;
    private Vector3 absoluteSpawnpos;

    private int indexOfNextUnit = 1;
    public CentralSpawnScript centralSpawnScript;


	// Use this for initialization
	void Start ()
    {
        //livingScript = this.GetComponentInParent<LivingScript>();
        livingScript = (LivingScript)this.GetComponent("LivingScript");

        absoluteSpawnpos = relativeSpawnpos;
        if(this.transform.position.x > 250)
        {
            absoluteSpawnpos.x = 500 - relativeSpawnpos.x;
        }
        if(this.transform.position.z > 250)
        {
            absoluteSpawnpos.z = 500 - relativeSpawnpos.z;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (hasAuthority)
        {
            //if(counter >= 100 && spawnposisfree(spawnpos))
            if (counter >= 100)
            {
                counter -= 100;
                centralSpawnScript.SpawnUnit(absoluteSpawnpos, indexOfNextUnit, livingScript.myPlayerId);
            }

            counter += spawnspeed * Time.deltaTime;
        }
	}

    //private bool spawnposisfree(Vector3 spawnposLocal)
    //{
    //    return true;
    //}

}
