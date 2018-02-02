using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Spawn : NetworkBehaviour {

    public int hp;
    public GameObject me;
    public float counter;
    public GameObject unittype;
    public float spawnspeed;
    public Vector3 spawnpos;
    //public Transform[] spawnPoints;

    public CentralSpawnScript centralSpawnScript;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (!hasAuthority)
            return;

        //float hp_temp = hp - (10 * Time.deltaTime);
        //hp = (int) hp_temp;

        if (hp <= 0)
        {
            //me.active = false;
            Destroy(me);
        }

        if(counter >= 100 && spawnposisfree(spawnpos))
        {
            counter -= 100;
            centralSpawnScript.CmdSpawnUnit(unittype, spawnpos);
        }

        counter += spawnspeed * Time.deltaTime;
	}

    private bool spawnposisfree(Vector3 spawnposLocal)
    {
        return true;
    }

}
