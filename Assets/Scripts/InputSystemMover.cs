using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemMover : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpSpeed = 5f;
    public float jumpCooldown = 2;
    Rigidbody rb;
    bool isJumping = false;
    Vector3 moveDir;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.Translate(moveDir * moveSpeed * Time.deltaTime);
    }

    void OnMove(InputValue value)
    {
        Vector2 inputDir = value.Get<Vector2>();
        moveDir = new Vector3(inputDir.x, 0, inputDir.y);
        Debug.Log(inputDir);
    }

    void OnJump(InputValue value)
    {
        bool inputButton = value.isPressed;
        rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        Debug.Log(inputButton);
    }
}
