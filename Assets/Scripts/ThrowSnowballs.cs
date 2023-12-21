using UnityEngine;

public class ThrowSnowballs : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform Camera;
    [SerializeField] GameObject SnowballPrefab;

    [Header("Settings")]
    [SerializeField] float ThrowCooldown;
    [SerializeField] float ThrowForce;
    [SerializeField] float ThrowUpForce;
    [SerializeField] float despawnTime = 5f;

    private float throwTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        throwTimer -= Time.deltaTime;

        // If the cooldown passed
        if (throwTimer < 0f && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Throw();
        }
    }

    private void Throw()
    {
        // Spawn the snowball
        GameObject snowball = Instantiate(SnowballPrefab, Camera.position, Camera.rotation);

        Rigidbody snowballRb = snowball.GetComponent<Rigidbody>();

        // Adding force to the snowball
        snowballRb.AddForce(Camera.forward * ThrowForce + transform.up * ThrowUpForce, ForceMode.Impulse);

        Destroy(snowball, despawnTime);

        throwTimer = ThrowCooldown;
    }    
}
