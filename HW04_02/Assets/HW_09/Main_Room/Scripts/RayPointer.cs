using UnityEngine;

public class RayPointer : MonoBehaviour
{
    [Header("레이 설정")]
    public float maxDistance = 10f;
    public LineRenderer lineRenderer;
    public Material highlightMat;
    
    private Material originalMat;
    private MeshRenderer lastHitRenderer;
    private DoorInteraction lastDoor;

    void Start()
    {
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        // 레이 시작점과 방향
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        // Line Renderer 업데이트
        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, origin);
            lineRenderer.SetPosition(1, origin + direction * maxDistance);
        }

        // Raycast
        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, maxDistance))
        {
            // Line Renderer를 충돌 지점까지만
            if (lineRenderer != null)
                lineRenderer.SetPosition(1, hit.point);

            // DoorInteraction 감지
            DoorInteraction door = hit.collider.GetComponent<DoorInteraction>();
            
            if (door != null)
            {
                // 새로운 문에 레이가 닿음
                if (lastDoor != door)
                {
                    ResetLastHighlight();
                    lastDoor = door;
                    lastHitRenderer = hit.collider.GetComponent<MeshRenderer>();
                    if (lastHitRenderer != null && highlightMat != null)
                    {
                        originalMat = lastHitRenderer.material;
                        lastHitRenderer.material = highlightMat;
                    }
                }

                // 트리거 당기면 문 열기
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
                {
                    door.OpenDoor();
                }
            }
            else
            {
                ResetLastHighlight();
            }
        }
        else
        {
            ResetLastHighlight();
        }
    }

    void ResetLastHighlight()
    {
        if (lastHitRenderer != null && originalMat != null)
        {
            lastHitRenderer.material = originalMat;
        }
        lastDoor = null;
        lastHitRenderer = null;
        originalMat = null;
    }
}