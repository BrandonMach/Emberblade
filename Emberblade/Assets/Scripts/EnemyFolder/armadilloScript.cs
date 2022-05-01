using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armadilloScript : MonoBehaviour
{
    // Start is called before the first frame update

    public bool playerInRange;
    public Vector2 playerDetection;
    PlayerInfo playerInfoController;
    public bool isOnGround;
    bool facingLeft;
    void Start()
    {
        playerInfoController = GameObject.Find("Player").GetComponent<PlayerInfo>();
        facingLeft = false;
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
    }

    void DetectPlayer()
    {
        Vector3 characterScale = transform.localScale;
        playerInRange = false;

        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, playerDetection, 0);

        foreach (var colliderHit in hitColliders)
        {
            if (colliderHit.gameObject.CompareTag("Player"))
            {
                playerInRange = true;
                if (playerInRange)
                {
                    if (this.transform.position.x > playerInfoController.transform.position.x && !facingLeft) // Armadillo på höger sida av spelare
                    {
                        characterScale.x *= -1;
                        facingLeft = true;
                        this.transform.localScale = characterScale;
                        Debug.Log("Armadillo look left");
                    }
                    if (this.transform.position.x < playerInfoController.transform.position.x && facingLeft) // Armadillo på vänster sida av spelaren
                    {
                        characterScale.x *= -1;
                        facingLeft = false;
                        this.transform.localScale = characterScale;
                    }
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, playerDetection);
    }

}
