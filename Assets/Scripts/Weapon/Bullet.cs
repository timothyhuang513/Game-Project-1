using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        
        Destroy(gameObject);
    }
}