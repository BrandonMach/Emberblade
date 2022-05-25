using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHookScript : MonoBehaviour
{
    LineRenderer line;

    [SerializeField] LayerMask grappleLayer;
    [SerializeField] float maxDistance = 10f; //Shoot lenght
    [SerializeField] float grappleSpeed = 10f;
    [SerializeField] float grappleShootSpeed = 20f;

    bool isGrappling = false;
    [HideInInspector] public bool retracting = false;

    Vector2 target;
    public Transform hookTransform;


    [HideInInspector]  public Vector2 direction;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        
       

    }

    // Update is called once per frame
    void Update()
    {
        line.useWorldSpace = true;
        if (Input.GetAxisRaw("Horizontal") < 0) //Left
        {
            direction = new Vector2(-100, 0);

        }
        else if (Input.GetAxisRaw("Horizontal") > 0) //Left
        {
            direction = new Vector2(100, 0);

        }
        if (Input.GetKeyDown(KeyCode.G) && !isGrappling)
        {
            StartGrapple();
        }

        if (retracting)
        {
            Vector2 grapplePos = Vector2.Lerp(transform.position, target, grappleShootSpeed * Time.deltaTime);

            transform.position = grapplePos;
            line.SetPosition(0, hookTransform.position);

            if(Vector2.Distance(transform.position,target) < 5f)
            {
                retracting = false;
                isGrappling = false;
                line.enabled = false;
            }
        }
       


    }

    private void StartGrapple()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(hookTransform.position , direction, maxDistance, grappleLayer);

        if(hit.collider != null)
        {
            isGrappling = true;
            target = hit.point;
            line.enabled = true;
            line.positionCount = 2;

            StartCoroutine(Grapple());
        }
    }

    IEnumerator Grapple()
    {
        float t = 0;
        float time = 10f;
        line.SetPosition(0, hookTransform.position);
        line.SetPosition(1, hookTransform.position);

        Vector2 newPos;
        for (; t < time; t+= grappleShootSpeed * Time.deltaTime)
        {
            newPos = Vector2.Lerp(hookTransform.position, target, t / time);
            line.SetPosition(0, hookTransform.position);
            line.SetPosition(1, newPos);

            yield return null;

        }

        line.SetPosition(1, target);
        retracting = true;
    }
}
