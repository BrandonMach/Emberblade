using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHookScript : MonoBehaviour //Detta är skrivet av: Brandon
{
    LineRenderer line;

    [SerializeField] LayerMask grappleLayer;
    [SerializeField] float maxDistance = 10f; //Shoot lenght
    [SerializeField] float grappleSpeed = 10f;
    [SerializeField] float grappleShootSpeed = 20f;
    [SerializeField] GameObject effect;
    private SFXPlaying soundEffect;

    bool isGrappling = false;
    [HideInInspector] public bool retracting = false;

    Vector2 target;
    public Transform hookTransform;


    [HideInInspector]  public Vector2 direction;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        soundEffect = GameObject.Find("SoundManager").GetComponent<SFXPlaying>();
       

    }

    // Update is called once per frame
    void Update()
    {
        line.useWorldSpace = true;
        if (Input.GetAxisRaw("Horizontal") < 0) //Left
        {
            direction = new Vector2(-100, 0);

        }
        else if (Input.GetAxisRaw("Horizontal") > 0) //Right
        {
            direction = new Vector2(100, 0);

        }
        RaycastHit2D seek = Physics2D.Raycast(hookTransform.position, direction, maxDistance, grappleLayer);    //Skapar en raycast
        if (seek.collider !=null)                                                                               //Om raycasten inte blockeras av något gör den synlig
        {
            
            Debug.Log("Can grapple");
            effect.SetActive(true);
        }
        else
        {
            effect.SetActive(false);
        }
       
        if (Input.GetKeyDown(KeyCode.K) && !isGrappling)
        {
            StartGrapple();
           
        }

        if (retracting)
        {
            Vector2 grapplePos = Vector2.Lerp(transform.position, target, grappleShootSpeed * Time.deltaTime); //Drar in spelaren mot objektet

            transform.position = grapplePos;
            line.SetPosition(0, hookTransform.position);

            if(Vector2.Distance(transform.position,target) < 5f)                    //Slutar dra in spelaren när man är tillräcklitg nära
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

        if(hit.collider != null)                    //Om man har prickat ett grapple objekt
        {
            isGrappling = true;
            target = hit.point;
            line.enabled = true;
            line.positionCount = 2;                 //Linje med två positioner

            StartCoroutine(Grapple());
            soundEffect.PlayTounge();
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
            line.SetPosition(0, hookTransform.position);                 //Första positionen är spelaren
            line.SetPosition(1, newPos);                                 // andra positionen åker mot objektets position

            yield return null;

        }

        line.SetPosition(1, target);                                     // andra positionen är objektets position
        retracting = true;
    }
}
