using UnityEngine;

public class CannonLauncher : MonoBehaviour
{
    [Header("발사 설정")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float launchForce = 30f;

    [Header("반동 설정")]
    public Transform playerTransform;
    public float recoilForce = 12f;

    [Header("쿨다운")]
    public float cooldown = 1.5f;

    private bool canFire = true;
    private bool playerInZone = false;

    void Update()
    {
        if (playerInZone && OVRInput.GetDown(OVRInput.RawButton.B))
        {
            Fire();
        }
    }

    void Fire()
    {
        if (!canFire) return;

        GameObject proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody projRb = proj.GetComponent<Rigidbody>();
        if (projRb != null)
            projRb.AddForce(firePoint.forward * launchForce, ForceMode.Impulse);

        if (playerTransform != null)
        {
            Vector3 recoilDir = -firePoint.forward;
            recoilDir.y = 0;
            PlayerKnockback knockback = playerTransform.GetComponent<PlayerKnockback>();
            if (knockback != null)
                knockback.ApplyKnockback(recoilDir, recoilForce);
        }

        canFire = false;
        Invoke(nameof(ResetCannon), cooldown);
    }

    void ResetCannon() { canFire = true; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.GetComponentInParent<EX_OVRInput_Combined_V2>())
        {
            playerInZone = true;
            var player = other.GetComponentInParent<EX_OVRInput_Combined_V2>();
            playerTransform = player != null ? player.transform : other.transform;
            Debug.Log("[작용/반작용] B 버튼을 눌러 대포를 발사하세요!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.GetComponentInParent<EX_OVRInput_Combined_V2>())
        {
            playerInZone = false;
        }
    }
}