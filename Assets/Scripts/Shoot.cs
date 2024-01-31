using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Shoot : MonoBehaviour
{
    public float bulletSpeed;
    public GameObject bulletPrefab;
    public ObjectPool<GameObject> pool;

    Vector3 origin;
    Vector3 direction;
    Quaternion bulletRotation;
    // Start is called before the first frame update
    void Start()
    {
        pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(bulletPrefab), 
            actionOnGet: (obj) => obj.SetActive(true), 
            actionOnRelease: (obj) => obj.SetActive(false), 
            actionOnDestroy: (obj) => Destroy(obj), 
            collectionCheck: false, 
            defaultCapacity: 10, 
            maxSize: 50);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = pool.Get();
            origin = GameObject.Find("BulletOrigin").transform.position;
            direction = transform.GetChild(3).forward;
            bulletRotation = transform.GetChild(3).rotation;

            bullet.transform.position = origin;
            bullet.transform.rotation = bulletRotation;
            bullet.GetComponent<Rigidbody>().velocity = (direction * bulletSpeed);
        }
    }
}
