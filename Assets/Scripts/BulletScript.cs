using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private float destroyTime = 5f;

    void Update()
    {
        Destroy(gameObject, destroyTime);
    }
}
