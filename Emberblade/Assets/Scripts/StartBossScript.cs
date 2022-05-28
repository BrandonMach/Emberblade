using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBossScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bossToWakeUp;
    private CombatScript player;
    public bool bossAwoke;
    void Start()
    {
        bossAwoke = false;
        player = GameObject.Find("Player").GetComponent<CombatScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Wake upp Boss");
            bossToWakeUp.SetActive(true);
            bossAwoke = true;
            player.isInBossBattle = true;
            Destroy(gameObject);
        }
    }
}
