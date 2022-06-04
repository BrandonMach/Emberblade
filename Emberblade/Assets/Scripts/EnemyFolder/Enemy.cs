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
public class Enemy : MonoBehaviour //Detta är skrivet av: Serena
{
    public Vector2[] Points;
    int currentMove = 0;
    [SerializeField] float speed = 0.03f;//do not go above 0.1, it will be too fast
    [SerializeField] float rotationSpeed = 2;
    [SerializeField] bool moveTowardsTarget = false;
    [SerializeField] Transform target;
    [SerializeField] float activiationRange = 20;
    [SerializeField] GameObject bullet;
    [SerializeField] EnemyType enemyType = EnemyType.walkTowards;
    [SerializeField] float explosionCircleSize;
    [SerializeField] CircleCollider2D explosionCircle;
    [SerializeField] List<GameObject> nearbyExplodableObjects;
    [SerializeField] private float startShootTimer;
    private float currentShootTimer;


    void Update()
    {
        Shooting();
        Move();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        nearbyExplodableObjects.Add(collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        nearbyExplodableObjects.Remove(collision.gameObject);
    }
    public void Shooting()
    {
        //here it should play an animation 
        //there should be an animation here

        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        if (activiationRange >= Vector3.Distance(transform.position, target.position))
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
    public void Explode()
    {
        //this activates when the enemy is near a the target;
        foreach (GameObject gameObject in nearbyExplodableObjects)
        {
            //TODO: make objects near this die or something like that, it depends
        }
        // there should be an animation here
        Destroy(gameObject);
    }
    public void Move()
    {
        // here it should also play an animation
        if (moveTowardsTarget == false)
        {
            if (new Vector2(transform.position.x, transform.position.y) == Points[currentMove])
            {
                currentMove++;
            }
            if (currentMove >= Points.Length)
                currentMove = 0;
            transform.position = Vector2.MoveTowards(transform.position, Points[currentMove], speed);
        }
        else if (moveTowardsTarget == true)
        {
            // add a trigger range that makes the enemy move towards the player
            // TODO: add a way for enemies to move around walls and other objects
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed);
        }
    }
}