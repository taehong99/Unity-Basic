using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class TankMovement2 : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float tankRotateSpeed = 80f;
    public float turretRotateSpeed = 50f;
    Transform turret;
    Vector2 moveDir;
    Vector2 rotateDir;

    void Start()
    {
        turret = transform.GetChild(3);
    }

    void Update()
    {
        // Tank Movement
        transform.Translate(new Vector3(0, 0, moveDir.y * moveSpeed * Time.deltaTime));

        // Tank Rotation
        transform.Rotate(new Vector3(0, moveDir.x * tankRotateSpeed * Time.deltaTime, 0));

        // Turret Rotation
        turret.Rotate(new Vector3(0, rotateDir.x * turretRotateSpeed * Time.deltaTime, 0));
    }

    void OnMove(InputValue value)
    {
        moveDir = value.Get<Vector2>();

        // Handle reverse rotation when moving back
        if (moveDir.y < 0)
        {
            moveDir.x *= -1;
        }
    }
    void OnRotateTurret(InputValue value)
    {
        rotateDir = value.Get<Vector2>();
    }
}
