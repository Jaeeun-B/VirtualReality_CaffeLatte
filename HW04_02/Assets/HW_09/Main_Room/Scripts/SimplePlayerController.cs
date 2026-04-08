using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SimplePlayerController : MonoBehaviour
{
    [Header("이동")]
    public float moveSpeed = 5f;
    public float gravity = -20f;

    [Header("마우스 회전")]
    public float mouseSensitivity = 2f;

    private CharacterController cc;
    private Transform cameraTransform;
    private float verticalVelocity;
    private float xRotation = 0f;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        cameraTransform = GetComponentInChildren<Camera>().transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 마우스 회전
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // WASD 이동
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = transform.right * h + transform.forward * v;

        // 중력
        if (cc.isGrounded && verticalVelocity < 0)
            verticalVelocity = -2f;

        verticalVelocity += gravity * Time.deltaTime;
        move.y = verticalVelocity;

        cc.Move(move * moveSpeed * Time.deltaTime);
    }
}