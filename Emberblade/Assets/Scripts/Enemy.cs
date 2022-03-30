using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    walkTowards,
    suicideEnemy,
    controlledWalk,
}
public class Enemy : MonoBehaviour
{
    public Vector2[] Points;
    public Rigidbody2D rbody;
    int currentMove = 0;
    public float speed = 0.03f;//do not go above 0.1, it will be too fast
    public float rotationSpeed = 2;
    public bool moveTowardsTarget = false;
    public Transform target;
    public float aggroRange = 20;
    public GameObject bullet;
    public EnemyType enemyType = EnemyType.walkTowards;
    public float explodeDistance;
    public float explosionCircleSize;
    public CircleCollider2D explosionCircle;
    public List<GameObject> nearbyExplodableObjects;
    [SerializeField] private float startShootTimer;
    private float currentShootTimer;
    public LayerMask explodableObjects;
    public GameObject explosionObject;


    void Update()
    {
        ShootingBehaviour();
        MoveBehaviours();
        ExplodeBehaviour();
    }
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
    public void ShootingBehaviour()
    {
        //here it should play an animation 

        // rotates the player towards a chosen target
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        // controls the range the enemy reacts to Player behaviour
        //also this just makes bullets
        if (aggroRange >= Vector3.Distance(transform.position, target.position))
        {
            if (currentShootTimer <= 0)
            {
                Instantiate(bullet, transform.position, gameObject.transform.rotation);
                currentShootTimer = startShootTimer;
            }
            else
            {
                currentShootTimer -= Time.deltaTime;
            }
        }
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
    public void MoveBehaviours()
    {
        // here it should also play an animation for moving
        if (moveTowardsTarget == false && Points.Length > 0)
        {
            if (new Vector2(transform.position.x, transform.position.y) == Points[currentMove])
            {
                currentMove++;
            }
            if (currentMove >= Points.Length)
                currentMove = 0;
            rbody.MovePosition(Vector2.MoveTowards(transform.position, Points[currentMove], speed));
        }
        else if (moveTowardsTarget == true)
        {
            // add a trigger range that makes the enemy move towards the player
            // TODO: add a way for enemies to move around walls and other objects
            rbody.MovePosition(Vector2.MoveTowards(transform.position, target.position, speed));
        }
    }
}