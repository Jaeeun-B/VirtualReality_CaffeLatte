using UnityEngine;

public class PushTriggerZone : MonoBehaviour
{
    [Header("연결")]
    public PushReaction pushWall;

    private bool playerInZone = false;
    private Transform playerTransform;

    void Update()
    {
        // Quest 3 오른손 검지 트리거
        if (playerInZone && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            pushWall.Push(playerTransform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // OVRPlayerController의 Tag 또는 컴포넌트로 감지
        if (other.CompareTag("Player") || other.GetComponentInParent<OVRPlayerController>())
        {
            playerInZone = true;
            var ovr = other.GetComponentInParent<OVRPlayerController>();
            playerTransform = ovr != null ? ovr.transform : other.transform;
            Debug.Log("[작용/반작용] 오른손 트리거를 당겨 블록을 밀어보세요!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.GetComponentInParent<OVRPlayerController>())
        {
            playerInZone = false;
            playerTransform = null;
        }
    }
}