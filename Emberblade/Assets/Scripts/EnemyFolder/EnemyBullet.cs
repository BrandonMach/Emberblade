using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rbody;
    [SerializeField] private float speed;
    public float lifespan = 10;
    private void Start()
    {
    }
    void Update()
    {
        rbody.AddForce(transform.right * speed, ForceMode2D.Impulse);
        if (lifespan <= 0)
            Destroy(gameObject);
        else
            lifespan -= Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}