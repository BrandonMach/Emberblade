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
    Vector2[] path;
    public Rigidbody2D rbody;
    public float speed = 0.03f;
    public float rotationSpeed = 2;
    public Transform target;
    public float aggroRange = 20;
    public EnemyType enemyType = EnemyType.walkTowards;
    public BoxCollider2D box;
    [SerializeField] private bool moveOutOfWay;


    [SerializeField] private float startShootTimer;
    private float currentShootTimer;

    int targetIndex;

    private void Start()
    {
        StartFollow();
    }
    void StartFollow()
    {
        path = Pathfinding.RequestPath(transform.position, target.position);
        StopCoroutine("FollowPath");
        StartCoroutine("FollowPath");
    }

    void LateUpdate()
    {
        if (Vector2.Distance(target.position, gameObject.transform.position) <= aggroRange)
            StartFollow();
        else
            StopCoroutine("FollowPath");
    }
    [SerializeField] Vector2 currentWaypoint;
    IEnumerator FollowPath()
    {
        currentWaypoint = path[0];

        while (true)
        {
            if ((Vector2)transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }
    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                //Gizmos.DrawCube((Vector3)path[i], Vector3.one *.5f);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }

}