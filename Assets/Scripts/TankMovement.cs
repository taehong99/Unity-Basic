using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;
    Vector3 moveDirection;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
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
        if (Input.GetKey(KeyCode.D))
        {
            Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
    }
    void Rotate(Vector3 axis, float angle)
    {
        transform.Rotate(axis, angle);
    }
}
