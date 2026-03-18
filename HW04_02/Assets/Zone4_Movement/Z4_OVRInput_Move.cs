using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Z4_OVRInput_Move : MonoBehaviour
{
    CharacterController Character;

    public Transform CenterEye;

    [Header("Move")]
    public float walkSpeed = 1.5f;
    public float runSpeed = 3.5f;

    [Header("Jump")]
    public float jumpHeight = 0.5f;
    public float gravity = -9.81f;

    Vector3 velocity;

    void Start()
    {
        Character = GetComponent<CharacterController>();
    }

    void Update()
    {
        WalkMove();
        
        Character.Move(velocity * Time.deltaTime);
    }

    void WalkMove()
    {
        Vector2 move = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);

        bool sprint = OVRInput.Get(OVRInput.RawButton.LThumbstick);

        bool jump = OVRInput.GetDown(OVRInput.RawButton.A);

        Vector3 forward = CenterEye.forward;
        Vector3 right = CenterEye.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 dir = forward * move.y + right * move.x;

        float speed = sprint ? runSpeed : walkSpeed;

        velocity.x = dir.x * speed;
        velocity.z = dir.z * speed;

        if (Character.isGrounded)
        {
            if (velocity.y < 0)
                velocity.y = -2f;

            if (jump)
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
    }


}