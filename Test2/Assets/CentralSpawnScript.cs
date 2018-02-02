using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CentralSpawnScript : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [Command]
    public void CmdSpawnUnit(GameObject unittype, Vector3 spawnpos)
    {
        Quaternion rot = new Quaternion();
        GameObject tmp = Instantiate(unittype, spawnpos, rot);
        NetworkServer.SpawnWithClientAuthority(tmp, connectionToClient);
    }

    [Command]
    public void CmdSpawnMainUnit(GameObject mainUnitPrefab)
    {
        GameObject tmp = Instantiate(mainUnitPrefab);
        Spawn localSpawnScript = (Spawn) tmp.GetComponent("Spawn");
        localSpawnScript.centralSpawnScript = this;
        NetworkServer.SpawnWithClientAuthority(tmp, connectionToClient);
    }
}
