using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;


public class Shoot : MonoBehaviour
{
    // Events
    public static event Action OnBulletShoot;

    public float bulletForceMultiplier;
    public float baseBulletForce;
    public float maxChargeTime;
    public GameObject bulletPrefab;
    public ObjectPool<GameObject> pool;
    public Transform spawnPoint;
    public Transform turret;
    public AudioSource shotChargingAudio;
    public AudioSource shotFiringAudio;

    Vector3 direction;
    Quaternion bulletRotation;
    TrajectoryPredictor trajectoryPredictor;
    ProjectileProperties projectileData;

    Animator tankAnimator;
    Coroutine chargeCoroutine;
    float chargeTime;

    void Start()
    {
        tankAnimator = GetComponent<Animator>();

        // Bullet Trajectory Setup
        projectileData = new ProjectileProperties();
        Rigidbody r = bulletPrefab.GetComponent<Rigidbody>();
        trajectoryPredictor = GetComponent<TrajectoryPredictor>();
        projectileData.mass = r.mass;
        projectileData.drag = r.drag;
        projectileData.initialSpeed = baseBulletForce;

        // Bullet Object Pooling
        pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(bulletPrefab), 
            actionOnGet: (obj) => obj.SetActive(true), 
            actionOnRelease: (obj) => obj.SetActive(false), 
            actionOnDestroy: (obj) => Destroy(obj), 
            collectionCheck: false, 
            defaultCapacity: 10, 
            maxSize: 50);
    }

    void LateUpdate()
    {
        // Only show trajectory in TPS Mode
        /*if(viewMode == ViewMode.FPS) {
            return; 
        }*/

        projectileData.direction = spawnPoint.forward;
        projectileData.initialPosition = spawnPoint.position;
        Predict();
    }
        
    
    void Predict()
    {
        trajectoryPredictor.PredictTrajectory(projectileData);
    }


    void OnShoot(InputValue value)
    {
        if (value.isPressed)
        {
            shotChargingAudio.Play();
            chargeTime = 0;
            chargeCoroutine = StartCoroutine(ChargeBullet());
        }
        else
        {
            StopCoroutine(chargeCoroutine);
            shotChargingAudio.Stop();
            shotFiringAudio.Play();
            OnBulletShoot?.Invoke();
            tankAnimator.SetTrigger("Shoot");
            Fire();
            projectileData.initialSpeed = baseBulletForce;
        }
    }

    IEnumerator ChargeBullet()
    {
        while (true)
        {
            chargeTime += Time.deltaTime;
            chargeTime = Mathf.Min(maxChargeTime, chargeTime);

            // Update Trajectory
            projectileData.initialSpeed = baseBulletForce + (chargeTime * bulletForceMultiplier);

            yield return null;
        }
    }

    public void Fire()
    {
        GameObject bullet = pool.Get();
        bulletRotation = spawnPoint.rotation;

        bullet.transform.position = spawnPoint.position;
        bullet.transform.rotation = bulletRotation;
        bullet.GetComponent<Bullet>().force = baseBulletForce + (chargeTime * bulletForceMultiplier);
    }
}
