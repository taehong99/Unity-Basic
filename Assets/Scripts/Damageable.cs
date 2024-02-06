using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public static event Action OnEnemyKill;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Bullet"))
        {
            OnEnemyKill?.Invoke();
            Destroy(gameObject);
        }
    }
}
