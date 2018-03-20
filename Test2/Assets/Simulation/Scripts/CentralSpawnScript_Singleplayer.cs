using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CentralSpawnScript_Singleplayer : MonoBehaviour
{

    // 0th object is mainunit
    public GameObject[] unitArray;

    public GameMasterScript_Singleplayer gameMasterScript;

    void Start()
    {
        //GameObject gameMaster = GameObject.Find("GameMaster");
        //gameMasterScript = gameMaster.GetComponent<GameMasterScript_Singleplayer>();
    }

    // SpawnScript must be set locally and on server
    public void SpawnMainUnit(int playerId)
    {
        GameObject mainUnitPrefab = unitArray[0];
        GameObject tmp = Instantiate(mainUnitPrefab);

        LocalSpawnScript_Singleplayer localSpawnScript = tmp.GetComponent<LocalSpawnScript_Singleplayer>();
        localSpawnScript.centralSpawnScript = this;

        LivingScript_Singleplayer livingScript = tmp.GetComponent<LivingScript_Singleplayer>();
        livingScript.myPlayerId = playerId;

        Vector3 spawnpos = gameMasterScript.spawnpositions[playerId];
        tmp.transform.position = spawnpos;
    }

    public void SpawnUnit(Vector3 spawnpos, int unitIndex, int playerId)
    {
        GameObject nextUnit = unitArray[unitIndex];
        nextUnit.transform.position = spawnpos;

        GameObject tmp = Instantiate(nextUnit);


        LivingScript_Singleplayer livingScript = tmp.GetComponent<LivingScript_Singleplayer>();
        livingScript.myPlayerId = playerId;
    }

 }
