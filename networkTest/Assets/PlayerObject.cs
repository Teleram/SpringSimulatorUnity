using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerObject : NetworkBehaviour {

    public GameObject playerUnitPrefab;

	// Use this for initialization
	void Start () {

        if(!isLocalPlayer)
        {
            return;
        }

        Debug.Log("Spawned my personal Unit!");

        //Instantiate(playerUnitPrefab);
        CmdSpawnMyUnit();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [Command]
    void CmdSpawnMyUnit()
    {
        GameObject tmp = Instantiate(playerUnitPrefab);

        NetworkServer.Spawn(tmp);
    }
}
