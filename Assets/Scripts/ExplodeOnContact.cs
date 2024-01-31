using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnContact : MonoBehaviour
{
    public ParticleSystem explosionPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }
}
