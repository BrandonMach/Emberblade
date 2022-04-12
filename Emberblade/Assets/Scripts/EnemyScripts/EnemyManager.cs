using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyManager : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();

    private void Start()
    {
        SearchMapForEnemies();
    }
    public void SearchMapForEnemies()
    {
        Enemy[] g = FindObjectsOfType<Enemy>();
        enemies = g.ToList();
    }
    void Update()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                SearchMapForEnemies();
            }
        }
    }
}