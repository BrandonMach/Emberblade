using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaviours : Enemy
{
    public float explodeDistance;
    public float explosionCircleSize;
    public CircleCollider2D explosionCircle;
    public List<GameObject> nearbyExplodableObjects;

    public LayerMask explodableObjects;
    public GameObject explosionObject;
    void Update()
    {
        
    }
    public void ExplodeBehaviour()
    {
        //this activates when the enemy is near a the target;
        if (Vector2.Distance(target.position, gameObject.transform.position) <= explodeDistance)
        {
            foreach (GameObject gameObject in nearbyExplodableObjects)
            {
                //TODO: make objects near this die or something like that, it depends on the health system
            }
            // there should be an animation here
            Destroy(gameObject);
        }
    }
    // this has to do with exploding
    private void OnTriggerEnter2D(Collider2D nearbyObject)
    {
        if (nearbyObject.tag == "Player") //TODO: make is so that this is multi choice
        {
            Debug.Log("activated");
            nearbyExplodableObjects.Add(nearbyObject.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D nearbyObject)
    {
        Debug.Log("deactivated");
        nearbyExplodableObjects.Remove(nearbyObject.gameObject);
    }
}
