using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpiderScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject bulletWeb;
    [SerializeField]
    GameObject hookWeb;

    WebHookScript webHookScript;

    float hookRateFireRate;
    float shootHook;
    float onRoofTimer;
    float jumpDownTime;
    float fireRate;
    float nextFire;
    Rigidbody2D rb;
    public bool onRoof;
    void Start()
    {
        onRoofTimer = 0;
        jumpDownTime = 15f;
        hookRateFireRate = 1f;
        fireRate = 5f;
        shootHook = Time.deltaTime;
        nextFire = Time.time;
        rb = GetComponent<Rigidbody2D>();
        webHookScript = GetComponent<WebHookScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onRoof)
        {
            onRoofTimer += Time.deltaTime;
            transform.localScale = new Vector3(transform.localScale.x, -1, transform.localScale.z);
            if(onRoofTimer >= jumpDownTime) 
            {
                onRoof = false;
                rb.gravityScale *= -1;
                onRoofTimer = 0;
            }
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
        }
        
        CheckIfTimeToFire();
        if (Input.GetKeyDown(KeyCode.B))
        {
            rb.gravityScale *= -1;      
            onRoof = true;
            webHookScript.ShootHookWeb();

        }
       
    }

    void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            Instantiate(bulletWeb, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Roof"))
        {
            webHookScript.StopShootHookWeb();
        }
        //if (collision.gameObject.CompareTag("Ground"))
        //{
        //    webHookScript.StopShootHookWeb();
        //}
    }

}
