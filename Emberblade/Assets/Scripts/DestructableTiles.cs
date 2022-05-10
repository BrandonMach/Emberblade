using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestructableTiles : MonoBehaviour
{
    // Start is called before the first frame update

    public Tilemap destructableTileMap;
    PlayerControll playerControllScript;
    void Start()
    {
        destructableTileMap = GetComponent<Tilemap>();
        playerControllScript = GameObject.Find("Player").GetComponent<PlayerControll>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {     
            if (collision.gameObject.CompareTag("Player"))
            {
                 if (playerControllScript.isDashing)
                 {
                    Debug.Log("Break block");
                    Vector3 hitPosition = Vector3.zero;
                    foreach (ContactPoint2D hit in collision.contacts)
                    {
                        hitPosition.x = hit.point.x - 0.01f * hit.normal.x; //Normal Surface, The position of the actual tile hit
                        hitPosition.y = hit.point.y - 0.01f * hit.normal.y; //Normal Surface

                        destructableTileMap.SetTile(destructableTileMap.WorldToCell(hitPosition), null);
                    } 
                 }
            }
        
    }
}
