using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class Enemy : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rbody;
    [SerializeField] protected float speed = 200;
    [SerializeField] protected float rotationSpeed = 2;
    [SerializeField] protected float nextWaypointDistance = 3f;
    [SerializeField] protected float targetDistanceStop = 0.5f;
    [SerializeField] protected Transform target;
    [SerializeField] protected Vector2 vecTarget;
    // this one is used when the enemy walks between two objects or hunts the target
    [SerializeField] protected bool willHunt = false;
    [SerializeField] protected Vector2[] PositionsToWalkBetween;
    [SerializeField] protected int currentWalkBetween;
    protected Path path;
    [SerializeField] protected GameObject gfxObj; // this is for the graphics as a child to the object
    [SerializeField] protected int currentWaypoint = 0;
    [SerializeField] protected bool reachedEndPath = false;
    protected Seeker seeker;
    [SerializeField] protected float aggroRange = 20;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rbody.GetComponent<Rigidbody2D>();
        if (target == null)
            target = FindObjectOfType<TargetEmpty>().transform;
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }
    protected virtual void UpdatePath() // man kan tro att denna inte har några referenser, men det har den, unity ljuger
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
    protected void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    public virtual void FixedUpdate()
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