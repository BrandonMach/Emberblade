using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BeeScript : MonoBehaviour
{

    [Header("General")]
    Seeker seeker;
    Path path;
    Rigidbody2D rbody;
    [SerializeField] GameObject gfxObj;

    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] float chargeSpeed;

    [Header("Patrolling")]
    [SerializeField] bool willHunt = false; // this bool controls if the enemy will move towards the player if its in range
    [SerializeField] Vector2[] PositionsToWalkBetween;
    [SerializeField] int currentWalkBetween;
    [SerializeField] int currentWaypoint = 0;
    [SerializeField] Transform target;
    [SerializeField] float targetDistanceStop;
    [SerializeField] float nextWaypointDistance = 3f;

    [Header("Attack")]
    [SerializeField] float reactRange;
    [SerializeField] float startAttackRange;
    float chargeTime;
    [SerializeField] float startChargeTime;

    void Start()
    {

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

    private void UpdatePath()
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
}
