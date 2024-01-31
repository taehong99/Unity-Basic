using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Mode { Delay, Triple, Hold }
public class Mover : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float jumpSpeed = 5f;
    public float jumpCooldown = 2;
    public Mode mode = Mode.Delay;
    Rigidbody rb;
    Coroutine holdJumpCoroutine;
    bool isJumping = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (mode == Mode.Delay)
            {
                StartCoroutine(DelayJump());
            }
            else if (mode == Mode.Triple)
            {
                StartCoroutine(TripleJump());
            }
            else if (mode == Mode.Hold)
            {
                holdJumpCoroutine = StartCoroutine(HoldJump());
            }
        }
        if (mode == Mode.Hold && Input.GetKeyUp(KeyCode.Space))
        {
            StopCoroutine(holdJumpCoroutine);
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
    }

    //�ڷ�ƾ�� �̿��Ͽ� ���� �Է½� 3�� �ڿ� ������ �ٴ� �ڷ�ƾ�� �����Ͻÿ�.
    IEnumerator DelayJump()
    {
        if (isJumping) { yield break; }

        isJumping = true;
        yield return new WaitForSeconds(3);
        Jump();
        isJumping = false;
    }

    // �ڷ�ƾ�� �̿��Ͽ� ���� �Է½� 1�� �������� 3�� �ٴ� �ڷ�ƾ�� �����Ͻÿ�.
    IEnumerator TripleJump()
    {
        if (isJumping) { yield break; }

        isJumping = true;
        for (int i = 0; i < 3; i++)
        {
            rb.velocity = Vector3.zero;
            Jump();
            yield return new WaitForSeconds(1);
        }
        isJumping = false;
    }

    // Ű�� ������ ������ 1�ʸ��� �ٰ� Ű�� ���� ���̻� ���� �ʴ� �ڷ�ƾ�� �����Ͻÿ�
    IEnumerator HoldJump()
    {
        while (true)
        {
            rb.velocity = Vector3.zero;
            Jump();
            yield return new WaitForSeconds(1);
        }
    }
}
