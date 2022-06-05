using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebHookScript : MonoBehaviour //Detta �r skrivet av: Brandon
{
    // Start is called before the first frame update
    private LineRenderer lineRenderer;
    public Transform webHitpoint;
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.useWorldSpace = true; 
    }
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up); //Skapar en synlig raycast som ska se ut som at spindeln spindeln�t
        Debug.DrawLine(transform.position, hit.point); //Ritar ut spindeln�tet
        webHitpoint.position = hit.point;

        lineRenderer.SetPosition(0, webHitpoint.position);
        lineRenderer.SetPosition(1, transform.position + new Vector3(0,70,0));
       
    }
    public void ShootHookWeb() 
    {
        lineRenderer.enabled = true; //G�r den synlig
    }
    public void StopShootHookWeb()
    {
        lineRenderer.enabled = false; //G�r den osyndlig
    }
}
