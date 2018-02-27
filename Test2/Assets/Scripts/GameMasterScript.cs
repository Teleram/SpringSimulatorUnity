using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameMasterScript : NetworkBehaviour {

    public Vector3[] spawnpositions;

    public Material[] materials;

    //[SyncVar]
    private int idForNextPlayer;

    public bool[] allDecidedOnAi;

    //public for debug
    public float gameTimer;

    public float maxGameTime;

    // saves all objects to reference them by id
    public GameObject[] allObjects;

    // Use this for initialization
    void Start () {
        idForNextPlayer = 0;
        allObjects = new GameObject[1];
        gameTimer = maxGameTime;
    }
	
	// Update is called once per frame
	void Update () {
		if(gameIsRunning())
        {
            gameTimer -= Time.deltaTime;
        }
	}

    public int getIdForNewPlayer()
    {
        return idForNextPlayer++;
    }

    public int getIdForObject(GameObject go)
    {
        int i;
        for (i = 0; i < allObjects.Length; i++)
        {
            if (allObjects[i] == null)
            {
                allObjects[i] = go;
                return i;
            }
        }

        GameObject[] newArray = new GameObject[allObjects.Length * 2];
        for (int j = 0; j < allObjects.Length; j++)
        {
            newArray[j] = allObjects[j];
        }
        newArray[i] = go;
        allObjects = newArray;
        return i;
    }

    public GameObject getObjectById(int id)
    {
        return allObjects[id];
    }

    public bool gameIsRunning()
    {
        return gameHasStarted() && !gameTimerOver() && haveAllDecidedOnAi();
    }

    public bool gameHasStarted()
    {
        return idForNextPlayer == 2;
    }

    public bool gameTimerOver()
    {
        return gameTimer <= 0.0f;
    }

    public bool haveAllDecidedOnAi()
    {
        bool allDecided = true;
        foreach(bool decided in allDecidedOnAi)
        {
            allDecided = allDecided && decided;
        }
        return allDecided;
    }
}
