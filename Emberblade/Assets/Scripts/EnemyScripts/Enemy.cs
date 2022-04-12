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
    public bool moveTowardsTarget = false;
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
       
        if(Pathfinding.checkPath(transform.position,transform.position + new Vector3(0,-8)) == false)
        {
            Debug.Log("bom bom bom");
        }
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


            if (moveOutOfWay == false)
                transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            else if (moveOutOfWay == true)
            {
                Debug.Log("trying to move out of way");
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(-4,0), speed * Time.deltaTime);
            }
            yield return null;
        }
    }
    public Vector2 newMoveTowards()
    {
        List<Vector2[]> vList =  new List<Vector2[]>();
        Vector2[] pathClosestToTarget = vList[0];
        //checks every side of the object
        for (int i = 0; i < 3; i++)
        {
            if (i == 0) // check left
            {
                if (Pathfinding.checkPath(transform.position, transform.position + new Vector3(0, -4)) == true)
                {
                    Vector2[] path = Pathfinding.RequestPath(transform.position, transform.position + new Vector3(0, -4));
                    vList.Add(path);
                    Debug.Log("leftCheck");
                }
            }
            else if (i == 1) // check right
            {
                if (Pathfinding.checkPath(transform.position, transform.position + new Vector3(0, 4)) == true)
                {
                    Vector2[] path = Pathfinding.RequestPath(transform.position, transform.position + new Vector3(0, 4));
                    vList.Add(path);
                    Debug.Log("rightCheck");

                }
            }
            else if (i == 2) // check up
            {
                if (Pathfinding.checkPath(transform.position, transform.position + new Vector3(4, 0)) == true)
                {
                    Vector2[] path = Pathfinding.RequestPath(transform.position, transform.position + new Vector3(4, 0));
                    vList.Add(path);
                    Debug.Log("upCheck");

                }
            }
            else if (i == 3) // check down
            {
                if (Pathfinding.checkPath(transform.position, transform.position + new Vector3(-4, 0)) == true)
                {
                    Vector2[] path = Pathfinding.RequestPath(transform.position, transform.position + new Vector3(-4, 0));
                    vList.Add(path);
                    Debug.Log("downCheck");

                }

            }
        }
        // selects all avaible sides and then returns a the path that is the closest to the target
        foreach (Vector2[] path in vList)
        {
            if(Vector2.Distance(path[path.Length],target.position) < Vector2.Distance(pathClosestToTarget[pathClosestToTarget.Length], target.position))
            {
                pathClosestToTarget = path;
            }
        }

        return pathClosestToTarget[0];
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //check if it has collided with anything, if it has, it will cancel its move towards and instead force move out of a wall until it can move again. 
        if (collision.collider.tag == "Respawn")
        {
            moveOutOfWay = true;
            Debug.Log("moving out of the way");
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