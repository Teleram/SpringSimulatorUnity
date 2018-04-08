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

   void FixedUpdate()
    {
        if (myObjectId == -1)
        {
            AssignMyObjectId();
        }
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void AssignMyObjectId()
    {
        myObjectId = gameMasterScript.GetIdForObject(gameObject);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
    }
}
