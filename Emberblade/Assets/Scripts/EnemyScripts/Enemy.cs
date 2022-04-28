using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class Enemy : MonoBehaviour
{
    public Rigidbody2D rbody;
    public float speed = 200;
    public float rotationSpeed = 2;
    public float nextWaypointDistance = 3f;
    public float targetDistanceStop = 0.5f;
    public Transform target;
    // this one is used when the enemy walks between two objects or hunts the target
    public bool willHunt = false;
    public Vector2[] PositionsToWalkBetween;
    [SerializeField] int currentWalkBetween;
    Path path;
    public GameObject gfxObj; // this is for the graphics as a child to the object
    int currentWaypoint = 0;
    public bool reachedEndPath = false;
    Seeker seeker;
    public float aggroRange = 20;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rbody.GetComponent<Rigidbody2D>();
        if (target == null)
            target = FindObjectOfType<TargetEmpty>().transform;
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }
    void UpdatePath()
    {
        if (willHunt == true)
        {
            if (seeker.IsDone() && Vector2.Distance(rbody.position, target.position) > targetDistanceStop)
            {
                seeker.StartPath(rbody.position, target.position, OnPathComplete);
            }
        }
        else if (willHunt == false)
        {
            if (seeker.IsDone() && Vector2.Distance(rbody.position, PositionsToWalkBetween[currentWalkBetween]) <= targetDistanceStop)
            {
                if (currentWalkBetween == PositionsToWalkBetween.Length - 1)
                    currentWalkBetween = 0;
                else
                    currentWalkBetween++;
                seeker.StartPath(rbody.position, PositionsToWalkBetween[currentWalkBetween], OnPathComplete);
            }
        }
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    private void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndPath = true;
            return;
        }
        else
        {
            reachedEndPath = false;
        }
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rbody.position).normalized;

        Vector2 force = direction * speed * Time.deltaTime;
        rbody.AddForce(force);

        float distance = Vector2.Distance(rbody.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        if (force.x > 0.01f)
            gfxObj.transform.localScale = new Vector3(1f, 1f, 1f);
        else
            gfxObj.transform.localScale = new Vector3(-1f, 1f, 1f);
    }
}