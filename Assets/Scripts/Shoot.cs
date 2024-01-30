using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float bulletSpeed;
    public GameObject bulletPrefab;

    Vector3 origin;
    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            origin = GameObject.Find("BulletOrigin").transform.position;
            direction = transform.forward;

            GameObject bullet = Instantiate(bulletPrefab, origin, transform.rotation);
            bullet.GetComponent<Rigidbody>().velocity = (direction * bulletSpeed);
        }
    }
}
