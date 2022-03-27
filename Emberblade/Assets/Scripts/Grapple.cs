using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{

    private Vector3 mousePos;
    public Camera camera;
  


    //Grappeling
    private bool isGrappling;

    private DistanceJoint2D distanceJoint;
    private LineRenderer lineRenderer;

    private Vector3 temporaryPos;

    // Start is called before the first frame update
    void Start()
    {
        
        camera = Camera.main; // Inte dynamisk kamera kanske är problem
        distanceJoint = GetComponent<DistanceJoint2D>();
        lineRenderer = GetComponent<LineRenderer>();
        

        distanceJoint.enabled = false;
        isGrappling = true;
        lineRenderer.positionCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetMousePos();

        if(Input.GetMouseButtonDown(0) && isGrappling)
        {
            distanceJoint.enabled = true;
            distanceJoint.connectedAnchor = mousePos;
            lineRenderer.positionCount = 2;
            temporaryPos = mousePos;
            isGrappling = false;
            
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            distanceJoint.enabled = false;
            isGrappling = true;
            lineRenderer.positionCount = 0;
        }
        DrawLine();
    }

    private void DrawLine()
    {
        if (lineRenderer.positionCount <= 0) return;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, temporaryPos);
    }

    private void GetMousePos() // Hittar mouse pos när man klickar
    {
        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
    }
}
