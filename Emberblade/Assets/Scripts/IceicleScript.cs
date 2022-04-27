using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceicleScript : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Transform hitPos;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D playerDetectionLaser = Physics2D.Raycast(transform.position, -transform.up);
        Debug.DrawLine(transform.position, playerDetectionLaser.point);
        hitPos.position = playerDetectionLaser.point;
        lineRenderer.SetPosition(0, hitPos.position);
        lineRenderer.SetPosition(1, transform.position);
        lineRenderer.enabled = true;
        //Om player är i laser ramla
    }
}
