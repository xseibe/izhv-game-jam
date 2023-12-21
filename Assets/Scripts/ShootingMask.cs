using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShootingMask : MonoBehaviour
{
    [Header("References")]
    [SerializeField] List<GameObject> Masks;
    [SerializeField] GameObject Bullet;

    [Header("Settings")]
    [SerializeField] float BulletSpeed = 10f;
    [SerializeField] float shootCooldown = 5f;

    private float shootTimer = 5f;
    private List<Transform> masksShootingPos = new List<Transform>();

    // Is the circle active?
    private bool circleActive = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get neccessary resources
        masksShootingPos = Masks.Select(mask => mask.transform.Find("ShootingPos")).ToList();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Spawn if the player is in the collider.
        if (other.tag == "Player")
        {
            Masks.ForEach(obj => obj.SetActive(true));
            circleActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Despawn
        if (other.tag == "Player")
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // If the masks are not active yet.
        if (!circleActive)
            return;

        // Decreases by time
        shootTimer -= Time.deltaTime;

        // If it is ready to shoot
        if (shootTimer < 0)
        {
            GameObject bullet;
            int i = 0;
            foreach (Transform shootingPos in masksShootingPos)
            {
                // Spawn bullet
                bullet = Instantiate(Bullet, shootingPos.position, shootingPos.rotation);
                // Match the bullet color to eye color
                bullet.GetComponent<MeshRenderer>().material = Masks[i++].transform.Find("Eyes/REye").GetComponent<MeshRenderer>().material;
                // Movement of the bullet
                bullet.GetComponent<Rigidbody>().velocity = shootingPos.forward * BulletSpeed;
            }

            shootTimer = shootCooldown;
        }
    }
}
