using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public static event Action OnEnemyKill;
    Animator tankAnimator;

    private void Start()
    {
        tankAnimator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Bullet"))
        {
            OnEnemyKill?.Invoke();
            tankAnimator.SetTrigger("Die");
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            Destroy(this);
        }
    }
}
