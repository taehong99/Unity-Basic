using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankMovement2 : MonoBehaviour
{
    public enum Mode { Keyboard, Mouse }

    [Header("Rotation Mode")]
    public Mode rotationMode;

    [Header("Tank")]
    public float moveSpeed = 10f;
    public float tankRotateSpeed = 80f;
    Vector2 moveDir;
    Vector2 rotateDir;

    [Header("Turret")]
    public float turretXSensitivity = 50f;
    public float turretYSensitivity = 50f;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    float rotationX;
    float rotationY;
    Transform turret;

    [Header("Audio")]
    public AudioSource tankIdleAudio;
    public AudioSource tankMovingAudio;

    void Start()
    {
        turret = transform.GetChild(3);
        tankIdleAudio.Play();
    }

    void Update()
    {
        // Tank Movement
        transform.Translate(new Vector3(0, 0, moveDir.y * moveSpeed * Time.deltaTime));

        // Tank Rotation
        transform.Rotate(new Vector3(0, moveDir.x * tankRotateSpeed * Time.deltaTime, 0));

        // Turret Rotation (ArrowKeys)
        if (rotationMode == Mode.Keyboard)
        {
            // Left Right
            rotationX += rotateDir.x * turretXSensitivity / 5;
            rotationX = Mathf.Clamp(rotationX, minX, maxX);


            // Up Down
            rotationY += rotateDir.y * turretYSensitivity / 5;
            rotationY = Mathf.Clamp(rotationY, minY, maxY);

            turret.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        // Turret Rotation (Mouse)
        if (rotationMode == Mode.Mouse)
        {
            // Left Right
            rotationX += Input.GetAxis("Mouse X") * turretXSensitivity;
            rotationX = Mathf.Clamp(rotationX, minX, maxX);


            // Up Down
            rotationY += Input.GetAxis("Mouse Y") * turretYSensitivity;
            rotationY = Mathf.Clamp(rotationY, minY, maxY);

            turret.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }

        //Audio
        if (moveDir.sqrMagnitude > 0.1f * 0.1f)
        {
            tankMovingAudio.Play();
            tankIdleAudio.Stop();
        }
        else
        {
            tankIdleAudio.Play();
            tankMovingAudio.Stop();
        }
    }

    void OnCalibrate()
    {
        turret.rotation = transform.rotation;
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
