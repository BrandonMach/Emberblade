using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBossScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject bossToWakeUp;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Wake upp Boss");
            bossToWakeUp.SetActive(true);
            Destroy(gameObject);
        }
    }
}
