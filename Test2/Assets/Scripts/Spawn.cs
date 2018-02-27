using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Spawn : NetworkBehaviour {

    private LivingScript livingScript;

    public float remainingSpawntime;
    public float spawntime;
    public Vector3 relativeSpawnpos;
    private Vector3 absoluteSpawnpos;

    private int indexOfNextUnit = 1;
    public CentralSpawnScript centralSpawnScript;

    private GameMasterScript gameMasterScript;

	// Use this for initialization
	void Start ()
    {
        GameObject gameMaster = GameObject.Find("GameMaster");
        gameMasterScript = gameMaster.GetComponent<GameMasterScript>();

        livingScript = GetComponent<LivingScript>();

        absoluteSpawnpos = relativeSpawnpos;
        if(transform.position.x > 250)
        {
            absoluteSpawnpos.x = 500 - relativeSpawnpos.x;
        }
        if(transform.position.z > 250)
        {
            absoluteSpawnpos.z = 500 - relativeSpawnpos.z;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (hasAuthority && gameMasterScript.gameIsRunning())
        {
            //if(counter >= 100 && spawnposisfree(spawnpos))
            if (remainingSpawntime <= 0)
            {
                remainingSpawntime += spawntime;
                centralSpawnScript.SpawnUnit(absoluteSpawnpos, indexOfNextUnit, livingScript.myPlayerId);
            }

            remainingSpawntime -= Time.deltaTime;
        }
	}

    //private bool spawnposisfree(Vector3 spawnposLocal)
    //{
    //    return true;
    //}

}
