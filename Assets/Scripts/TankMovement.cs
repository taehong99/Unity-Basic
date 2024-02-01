using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;
    Vector3 moveDirection;
    Transform turret;
    Rigidbody rb;

    void Start()
    {
        turret = transform.GetChild(3);
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Base Movement
        if(Input.GetKey(KeyCode.W))
        {
            Vector3 direction = transform.forward;
            rb.velocity = direction * moveSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 direction = -transform.forward;
            rb.velocity = direction * moveSpeed;
        }
        
        // Base Rotation
        if (Input.GetKey(KeyCode.D))
        {
            Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }

        // Turret Rotation
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            turret.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            turret.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
    void Rotate(Vector3 axis, float angle)
    {
        transform.Rotate(axis, angle);
    }
}
