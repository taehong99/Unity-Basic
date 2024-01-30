using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnContact : MonoBehaviour
{
    ParticleSystem explosion;
    private void Start()
    {
        explosion = transform.GetChild(0).GetComponent<ParticleSystem>();
        explosion.Stop();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with " +  collision.gameObject.name);
        explosion.Play();
        //Destroy(gameObject);
    }
}
