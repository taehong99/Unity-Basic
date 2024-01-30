using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnContact : MonoBehaviour
{
    public ParticleSystem explosionPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with " +  collision.gameObject.name);
        var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
