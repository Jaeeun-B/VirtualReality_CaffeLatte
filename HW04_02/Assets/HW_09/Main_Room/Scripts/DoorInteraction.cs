using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour
{
    [Header("씬 전환")]
    public string targetScene;

    [Header("애니메이션")]
    public float openAngle = -90f;
    public float openSpeed = 3f;
    public float delayBeforeLoad = 1.2f;

    private bool isOpening = false;
    private float currentAngle = 0f;

    void Update()
    {
        if (isOpening)
        {
            currentAngle = Mathf.Lerp(currentAngle, openAngle, openSpeed * Time.deltaTime);
            transform.localRotation = Quaternion.Euler(0, currentAngle, 0);
        }
    }

    /// RayPointer에서 호출
    public void OpenDoor()
    {
        if (!isOpening)
        {
            isOpening = true;
            Invoke(nameof(LoadScene), delayBeforeLoad);
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(targetScene);
    }
}