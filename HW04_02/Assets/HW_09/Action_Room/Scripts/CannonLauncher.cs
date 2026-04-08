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
        // Quest 3 오른손 검지 트리거
        if (playerInZone && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            Fire();
        }
    }

    void Fire()
    {
        if (!canFire) return;

        // 작용: 발사체 생성 + 발사
        GameObject proj = Instantiate(
            projectilePrefab,
            firePoint.position,
            firePoint.rotation
        );

        Rigidbody projRb = proj.GetComponent<Rigidbody>();
        if (projRb != null)
        {
            projRb.AddForce(firePoint.forward * launchForce, ForceMode.Impulse);
        }

        // 반작용: 플레이어 뒤로 밀림
        if (playerTransform != null)
        {
            Vector3 recoilDir = -firePoint.forward;
            recoilDir.y = 0;

            PlayerKnockback knockback = playerTransform.GetComponent<PlayerKnockback>();
            if (knockback != null)
            {
                knockback.ApplyKnockback(recoilDir, recoilForce);
            }
        }

        canFire = false;
        Invoke(nameof(ResetCannon), cooldown);
    }

    void ResetCannon()
    {
        canFire = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.GetComponentInParent<OVRPlayerController>())
        {
            playerInZone = true;
            var ovr = other.GetComponentInParent<OVRPlayerController>();
            playerTransform = ovr != null ? ovr.transform : other.transform;
            Debug.Log("[작용/반작용] 오른손 트리거를 당겨 대포를 발사하세요!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.GetComponentInParent<OVRPlayerController>())
        {
            playerInZone = false;
        }
    }
}