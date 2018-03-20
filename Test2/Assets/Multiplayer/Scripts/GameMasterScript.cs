using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.IO;

public class GameMasterScript : NetworkBehaviour
{

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

    public bool resultsLogged;

    // Use this for initialization
    void Start()
    {
        idForNextPlayer = 0;
        allObjects = new GameObject[1];
        gameTimer = maxGameTime;
        resultsLogged = false;

        // for ai training, deactivate if you want to play
        Time.timeScale = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameIsRunning())
        {
            gameTimer -= Time.deltaTime;
        }

        if (GameTimerOver() && isServer && !resultsLogged)
        {
            resultsLogged = true;
            RpcSetResultsLogged();
            WriteResults();
        }

        if (resultsLogged)
        {
            Application.Quit();
        }
    }

    [ClientRpc]
    private void RpcSetResultsLogged()
    {
        resultsLogged = true;
    }

    private void WriteResults()
    {
        StreamWriter writer = new StreamWriter("Results.txt");
        int unitsOfPlayer0 = 0;
        int unitsOfPlayer1 = 0;
        foreach (GameObject go in allObjects)
        {
            if (go != null)
            {
                if (go.GetComponent<LivingScript>().myPlayerId == 0)
                {
                    unitsOfPlayer0++;
                }
                else
                {
                    unitsOfPlayer1++;
                }
            }
        }
        writer.WriteLine(unitsOfPlayer0);
        writer.WriteLine(unitsOfPlayer1);
        writer.Close();
    }

    public int GetIdForNewPlayer()
    {
        return idForNextPlayer++;
    }

    public int GetIdForObject(GameObject go)
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

    public GameObject GetObjectById(int id)
    {
        return allObjects[id];
    }

    public bool GameIsRunning()
    {
        return GameHasStarted() && !GameTimerOver() && HaveAllDecidedOnAi();
    }

    public bool GameHasStarted()
    {
        return idForNextPlayer == 2;
    }

    public bool GameTimerOver()
    {
        return gameTimer <= 0.0f;
    }

    public bool HaveAllDecidedOnAi()
    {
        bool allDecided = true;
        foreach (bool decided in allDecidedOnAi)
        {
            allDecided = allDecided && decided;
        }
        return allDecided;
    }

    // returns all Gameobjects that are in range to the Gameobject me
    // but me is NOT included in the return array
    public List<GameObject> GameObjectsInRange(GameObject me, float range)
    {
        List<GameObject> gos = new List<GameObject>();
        foreach (GameObject go in allObjects)
        {
            if (go != null)
            {
                float distance = Vector3.Distance(me.transform.position, go.transform.position);
                if ((distance <= range) && (go != me))
                {
                    gos.Add(go);
                }
            }
        }
        return gos;
    }

    // returns all Gameobjects that are in range to the position
    public List<GameObject> GameObjectsInRange(Vector3 position, float range)
    {
        List<GameObject> gos = new List<GameObject>();
        foreach (GameObject go in allObjects)
        {
            float distance = Vector3.Distance(position, go.transform.position);
            if (distance <= range)
            {
                gos.Add(go);
            }
        }
        return gos;
    }
}
