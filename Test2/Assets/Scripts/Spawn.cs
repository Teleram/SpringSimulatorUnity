using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Spawn : NetworkBehaviour {

    public int hp;
    public GameObject me;

    [SyncVar]
    public int myPlayerId;

    public float counter;
    public float spawnspeed;
    public Vector3 spawnpos;

    private int indexOfNextUnit = 1;
    public CentralSpawnScript centralSpawnScript;


	// Use this for initialization
	void Start ()
    {
        GameObject gameMaster = GameObject.Find("GameMaster");
        GameMasterScript gameMasterScript = (GameMasterScript)gameMaster.GetComponent("GameMasterScript");
        Material myMaterial = gameMasterScript.materials[myPlayerId];

        Renderer renderer = me.GetComponentInChildren<Renderer>();
        renderer.material = myMaterial;
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

        //if(counter >= 100 && spawnposisfree(spawnpos))
        if (counter >= 100)
        {
            counter -= 100;
            centralSpawnScript.SpawnUnit(spawnpos, indexOfNextUnit, myPlayerId);
            //CmdSpawnUnit();
        }

        counter += spawnspeed * Time.deltaTime;
	}

    //private bool spawnposisfree(Vector3 spawnposLocal)
    //{
    //    return true;
    //}

}
