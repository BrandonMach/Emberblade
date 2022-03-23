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
    int currentMove = 0;
    public float speed = 0.03f;//do not go above 0.1, it will be too fast
    public bool moveTowardsTarget = false;
    public Transform target;
    public float activiationRange = 20;
    public GameObject bullet;
    public EnemyType enemyType = EnemyType.walkTowards;
    public float explosionCircleSize;
    public CircleCollider2D explosionCircle;
    public List<GameObject> explodableObjects;
    [SerializeField] private float startShootTimer;
    private float currentShootTimer;


    void DestroyObject() { Destroy(gameObject); }
    void Update()
    {
        Shooting();
        Move();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        explodableObjects.Add(collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        explodableObjects.Remove(collision.gameObject);
    }
    public void Shooting()
    {
        //here it should play an animation 
        // make it so that bullets shoots towards target. 
        //there should be an animation here
        if (currentShootTimer <= 0)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            currentShootTimer = startShootTimer;
        }
        else
        {
            currentShootTimer -= Time.deltaTime;
        }
    }
    public void Explode()
    {
        //this activates when the enemy is near a the target;
        foreach (GameObject gameObject in explodableObjects)
        {
            //TODO: make objects near this die or something like that, it depends
        }
        // there should be an animation here
        DestroyObject();
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