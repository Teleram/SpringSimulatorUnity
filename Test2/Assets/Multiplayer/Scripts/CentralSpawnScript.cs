﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class CentralSpawnScript : NetworkBehaviour {
    
    // 0th object is mainunit
    public GameObject[] unitArray;
    
    public GameMasterScript gameMasterScript;

    void Start()
    {
        //GameObject gameMaster = GameObject.Find("GameMaster");
        //gameMasterScript = gameMaster.GetComponent<GameMasterScript>();
    }

    // SpawnScript must be set locally and on server
    public void SpawnMainUnit(int playerId)
    {
        GameObject mainUnitPrefab = unitArray[0];

        Spawn localSpawnScript = mainUnitPrefab.GetComponent<Spawn>();
        localSpawnScript.centralSpawnScript = this;

        LivingScript livingScript = mainUnitPrefab.GetComponent<LivingScript>();
        livingScript.myPlayerId = playerId;

        CmdSpawnMainUnit(playerId);
    }

    // Spawns the Main Unit
    [Command]
    private void CmdSpawnMainUnit(int playerId)
    {
        GameObject mainUnitPrefab = unitArray[0];

        Spawn localSpawnScript = (Spawn)mainUnitPrefab.GetComponent("Spawn");
        localSpawnScript.centralSpawnScript = this;

        LivingScript livingScript = (LivingScript)mainUnitPrefab.GetComponent("LivingScript");
        livingScript.myPlayerId = playerId;

        GameObject tmp = Instantiate(mainUnitPrefab);

        Vector3 spawnpos = gameMasterScript.spawnpositions[playerId];
        tmp.transform.position = spawnpos;

        NetworkServer.SpawnWithClientAuthority(tmp, connectionToClient);
    }
    
    

    public void SpawnUnit(Vector3 spawnpos, int unitIndex, int playerId)
    {
        CmdSpawnUnit(spawnpos, unitIndex, playerId);
    }

    // Spawns a normal Unit
    [Command]
    private void CmdSpawnUnit(Vector3 spawnpos, int unitIndex, int playerId)
    {
        GameObject nextUnit = unitArray[unitIndex];
        GameObject tmp = Instantiate(nextUnit);

        tmp.transform.position = spawnpos;

        LivingScript livingScript = (LivingScript)tmp.GetComponent("LivingScript");
        livingScript.myPlayerId = playerId;

        NetworkServer.SpawnWithClientAuthority(tmp, connectionToClient);
    }
}
