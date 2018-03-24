using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalSpawnScript_Singleplayer : MonoBehaviour {

    private LivingScript_Singleplayer livingScript;

    public float remainingSpawntime;
    public float spawntime;
    public Vector3 relativeSpawnpos;
    private Vector3 absoluteSpawnpos;

    private int indexOfNextUnit = 1;
    public CentralSpawnScript_Singleplayer centralSpawnScript;

    private GameMasterScript_Singleplayer gameMasterScript;

    // Use this for initialization
    void Start()
    {
        GameObject gameMaster = GameObject.FindGameObjectWithTag("GameMaster");
        gameMasterScript = gameMaster.GetComponent<GameMasterScript_Singleplayer>();

        livingScript = GetComponent<LivingScript_Singleplayer>();

        absoluteSpawnpos = relativeSpawnpos;
        if (transform.position.x > 250)
        {
            absoluteSpawnpos.x = 500 - relativeSpawnpos.x;
        }
        if (transform.position.z > 250)
        {
            absoluteSpawnpos.z = 500 - relativeSpawnpos.z;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameMasterScript.GameIsRunning())
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

}