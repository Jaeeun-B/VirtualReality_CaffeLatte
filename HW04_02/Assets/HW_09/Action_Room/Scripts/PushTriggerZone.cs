using UnityEngine;

public class PushTriggerZone : MonoBehaviour
{
    [Header("연결")]
    public PushReaction pushWall;

    private bool playerInZone = false;
    private Transform playerTransform;

    void Update()
    {
        if (playerInZone && OVRInput.GetDown(OVRInput.RawButton.A))
        {
            pushWall.Push(playerTransform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.GetComponentInParent<EX_OVRInput_Combined_V2>())
        {
            playerInZone = true;
            var player = other.GetComponentInParent<EX_OVRInput_Combined_V2>();
            playerTransform = player != null ? player.transform : other.transform;
            Debug.Log("[작용/반작용] A 버튼을 눌러 블록을 밀어보세요!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.GetComponentInParent<EX_OVRInput_Combined_V2>())
        {
            playerInZone = false;
            playerTransform = null;
        }
    }
}