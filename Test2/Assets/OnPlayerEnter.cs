using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OnPlayerEnter : NetworkBehaviour {

    //public GameObject mainUnitPrefab;

    public CentralSpawnScript centralSpawnScript;

    // Use this for initialization
    void Start () {
        // is this me?
        if (!isLocalPlayer)
        {
            return;
        }

        Debug.Log("I spawned!");

        centralSpawnScript.SpawnMainUnit();
        //CmdSpawnMainUnit();
        //Instantiate(mainUnitPrefab);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    

}
