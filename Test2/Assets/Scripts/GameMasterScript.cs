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

    // returns all Gameobjects that are in range to the Gameobject me
    // but me is NOT included in the return array
    public GameObject[] gameObjectsInRange(GameObject me, float range)
    {
        GameObject[] gos = new GameObject[0];
        foreach (GameObject go in allObjects)
        {
            if (go != null)
            {
                float distance = Vector3.Distance(me.transform.position, go.transform.position);
                if ((distance <= range) && (go != me))
                {
                    int oldLength = gos.Length;
                    GameObject[] tmp = gos;
                    gos = new GameObject[oldLength + 1];
                    for (int i = 0; i < oldLength; i++)
                    {
                        gos[i] = tmp[i];
                    }
                    gos[oldLength] = go;
                }
            }
        }
        return gos;
    }

    // returns all Gameobjects that are in range to the position
    public GameObject[] gameObjectsInRange(Vector3 position, float range)
    {
        GameObject[] gos = new GameObject[0];
        foreach(GameObject go in allObjects)
        {
            float distance = Vector3.Distance(position, go.transform.position);
            if (distance <= range)
            {
                int oldLength = gos.Length;
                GameObject[] tmp = gos;
                gos = new GameObject[oldLength + 1];
                for(int i = 0; i < oldLength; i++)
                {
                    gos[i] = tmp[i];
                }
                gos[oldLength] = go;
            }
        }
        return gos;
    }
}
