using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Transform target;
    public float speed;

    Vector2[] path;
    int targetIndex;

    void Start()
    {
        path = Pathfinding.RequestPath(transform.position, target.position);
        StopCoroutine("FollowPath");
        StartCoroutine("FollowPath");
        Debug.Log("started");

    }
    public void Update()
    {
        StartCoroutine("FollowPath");
    }
    IEnumerator FollowPath()
    {
        Vector2 currentWaypoint = path[0];

        while (true)
        {
            if((Vector2)transform.position == currentWaypoint)
            {
                targetIndex++;
                if(targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }
}