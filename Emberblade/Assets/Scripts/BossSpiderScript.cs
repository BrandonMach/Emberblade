using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpiderScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject bulletWeb;

    float fireRate;
    float nextFire;
    void Start()
    {
        fireRate = 5f;
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfTimeToFire();
    }

    void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            Instantiate(bulletWeb, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }
}
