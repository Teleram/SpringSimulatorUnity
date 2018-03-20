using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingScript_Singleplayer : MonoBehaviour {
    
    public int hp;

    public int myPlayerId;

    // public for debug
    public int myObjectId;

    private GameMasterScript_Singleplayer gameMasterScript;

    void Start()
    {
        GameObject gameMaster = GameObject.FindGameObjectWithTag("GameMaster");
        gameMasterScript = gameMaster.GetComponent<GameMasterScript_Singleplayer>();
        Material myMaterial = gameMasterScript.materials[myPlayerId];

        Renderer renderer = gameObject.GetComponent<Renderer>();
        renderer.material = myMaterial;

        //assignMyObjectId();
        myObjectId = -1;
    }

    void Update()
    {
        if (myObjectId == -1)
        {
            assignMyObjectId();
        }
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void assignMyObjectId()
    {
        myObjectId = gameMasterScript.GetIdForObject(gameObject);
    }

    public void takeDamage(int damage)
    {
        hp -= damage;
    }
}
