using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PushReaction : MonoBehaviour
{
    [Header("힘 설정")]
    public float pushForce = 800f;
    public float playerKnockbackForce = 15f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// TriggerZone에서 호출
    public void Push(Transform playerTransform) {
    // VR에서는 카메라(머리) 방향이 "정면"
        Camera cam = Camera.main;
        Vector3 pushDir = cam != null ? cam.transform.forward : playerTransform.forward;
        pushDir.y = 0;
        rb.AddForce(pushDir.normalized * pushForce, ForceMode.Impulse);

        // 반작용
        PlayerKnockback knockback = playerTransform.GetComponent<PlayerKnockback>();
        if (knockback != null)
        {
            knockback.ApplyKnockback(-pushDir, playerKnockbackForce);
        }
    }
}