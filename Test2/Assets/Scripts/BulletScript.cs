using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletScript : NetworkBehaviour {

    [SyncVar]
    public int damage;

    public void OnTriggerEnter(Collider other)
    {
        GameObject target = other.transform.gameObject;
        LivingScript targetLive = target.GetComponent<LivingScript>();
        targetLive.takeDamage(damage);
        Destroy(this.transform.gameObject);
    }
}
