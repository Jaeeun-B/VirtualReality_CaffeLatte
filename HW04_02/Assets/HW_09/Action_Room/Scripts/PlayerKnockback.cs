using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{
    [Header("감쇠 설정")]
    public float decayRate = 5f;

    private CharacterController cc;
    private Vector3 knockbackVelocity;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        // OVRPlayerController의 CharacterController를 자동으로 찾음
        if (cc == null)
            cc = GetComponentInChildren<CharacterController>();
    }

    void Update()
    {
        if (knockbackVelocity.magnitude > 0.1f)
        {
            if (cc != null)
            {
                cc.Move(knockbackVelocity * Time.deltaTime);
            }
            else
            {
                transform.position += knockbackVelocity * Time.deltaTime;
            }

            knockbackVelocity = Vector3.Lerp(
                knockbackVelocity, Vector3.zero, decayRate * Time.deltaTime
            );
        }
        else
        {
            knockbackVelocity = Vector3.zero;
        }
    }

    public void ApplyKnockback(Vector3 direction, float force)
    {
        knockbackVelocity = direction.normalized * force;
    }
}