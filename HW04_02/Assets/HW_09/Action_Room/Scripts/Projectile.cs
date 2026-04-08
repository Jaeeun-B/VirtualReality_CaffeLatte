using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("수명")]
    public float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}