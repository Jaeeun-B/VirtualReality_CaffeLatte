using UnityEngine;

public class BouncinessController : MonoBehaviour
{
    public PhysicMaterial bouncyMat;
    public float step = 0.2f;

    public void IncreaseBounciness()
    {
        bouncyMat.bounciness = Mathf.Clamp(bouncyMat.bounciness + step, 0f, 1f);
        Debug.Log("Bounciness: " + bouncyMat.bounciness);
    }

    public void DecreaseBounciness()
    {
        bouncyMat.bounciness = Mathf.Clamp(bouncyMat.bounciness - step, 0f, 1f);
        Debug.Log("Bounciness: " + bouncyMat.bounciness);
    }
}