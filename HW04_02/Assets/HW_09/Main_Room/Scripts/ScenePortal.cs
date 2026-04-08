using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePortal : MonoBehaviour
{
    public string targetScene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.GetComponentInParent<OVRPlayerController>())
        {
            SceneManager.LoadScene(targetScene);
        }
    }
}