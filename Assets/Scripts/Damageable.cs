using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    Animator tankAnimator;

    private void Start()
    {
        tankAnimator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Bullet"))
        {
            Manager.Data.KillCount++;
            tankAnimator.SetTrigger("Die");
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            Destroy(this);
        }
    }
}
