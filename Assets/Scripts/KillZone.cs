using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.transform.parent = EnemyPool.Inst.gameObject.transform;
            EnemyPool.Inst.ReturnEnemy(collision.gameObject);
        }
    }
}
