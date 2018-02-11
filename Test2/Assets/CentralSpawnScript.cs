using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CentralSpawnScript : NetworkBehaviour {

    public GameObject mainUnitPrefab;
    //private GameObject mainUnit;

    //public GameObject unit1;
    //public Vector3 spawnpos1;
    private GameObject nextUnit;
    private Vector3 nextSpawnpos;

    private Spawn localSpawnScript;


    public void SpawnMainUnit()
    {
        //mainUnit = Instantiate(mainUnitPrefab);

        localSpawnScript = (Spawn)mainUnitPrefab.GetComponent("Spawn");
        localSpawnScript.centralSpawnScript = this;

        CmdSpawnMainUnit();
        //Destroy(mainUnit);
    }

    // Spawns the Main Unit
    [Command]
    private void CmdSpawnMainUnit()
    {
        GameObject tmp = Instantiate(mainUnitPrefab);
        NetworkServer.SpawnWithClientAuthority(tmp, connectionToClient);
        //NetworkServer.Spawn(tmp);

        //RpcSyncSpawnScript();
    }

    //[ClientRpc]
    //private void RpcSyncSpawnScript()
    //{
    //    localSpawnScript.centralSpawnScript = this;
    //}

    // Puts Parameters into Variables, because CmdSpawnUnit doesn´t take the Parameters directly
    public void SpawnUnit(Vector3 spawnpos, GameObject unittype)
    {
        nextSpawnpos = spawnpos;
        nextUnit = unittype;
        CmdSpawnUnit();
    }

    // Spawns a normal Unit
    [Command]
    private void CmdSpawnUnit()
    {
        GameObject tmp = Instantiate(nextUnit);

        NetworkServer.SpawnWithClientAuthority(tmp, connectionToClient);
        //NetworkServer.Spawn(tmp);

        tmp.transform.position = nextSpawnpos;
    }
}
