using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTowardsTarget : Enemy
{
    GameObject bullet;
    float aggroRange = 20;
    float currentShootTimer;
    float startShootTimer = 1;
    float rotationSpeed = 2;

    public void ShootingBehaviour()
    {
        //here it should play an animation 

        // rotates the player towards a chosen target
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        // controls the range the enemy reacts to Player behaviour
        //also this just makes bullets
        if (aggroRange >= Vector3.Distance(transform.position, target.position))
        {
            if (currentShootTimer <= 0)
            {
                Instantiate(bullet, transform.position, gameObject.transform.rotation);
                currentShootTimer = startShootTimer;
            }
            else
            {
                currentShootTimer -= Time.deltaTime;
            }
        }
    }
}