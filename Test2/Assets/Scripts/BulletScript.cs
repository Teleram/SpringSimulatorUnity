using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletScript : NetworkBehaviour {

    [SyncVar]
    public int damage;

    private void OnTriggerStay(Collider other)
    {
        GameObject target = other.transform.gameObject;
        LivingScript targetLive = target.GetComponent<LivingScript>();
        targetLive.takeDamage(damage);
        Destroy(transform.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject target = other.transform.gameObject;
        LivingScript targetLive = target.GetComponent<LivingScript>();
        targetLive.takeDamage(damage);
        Destroy(transform.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject target = collision.transform.gameObject;
        LivingScript targetLive = target.GetComponent<LivingScript>();
        targetLive.takeDamage(damage);
        Destroy(transform.gameObject);
    }

    private void OnCollisionStay(Collision collision)
    {
        GameObject target = collision.transform.gameObject;
        LivingScript targetLive = target.GetComponent<LivingScript>();
        targetLive.takeDamage(damage);
        Destroy(transform.gameObject);
    }
}
