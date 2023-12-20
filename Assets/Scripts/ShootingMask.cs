using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShootingMask : MonoBehaviour
{
    [Header("References")]
    [SerializeField] List<GameObject> Masks;
    [SerializeField] List<Material> BulletMaterials;
    [SerializeField] GameObject Bullet;

    [Header("Settings")]
    [SerializeField] float BulletSpeed = 10f;
    [SerializeField] float shootCooldown = 5f;

    private float shootTimer = 5f;
    private List<Transform> masksShootingPos = new List<Transform>();


    // Start is called before the first frame update
    void Start()
    {
        // Get shooting positions from all of the masks.
        masksShootingPos = Masks.Select(mask => mask.transform.Find("ShootingPos")).ToList();
    }

    private void OnTriggerEnter(Collider other)
    {
        // GreenBallon is the highest, but it does not really matter
        if (other.tag == "GreenBalloon")
        {
            Masks.ForEach(obj => obj.SetActive(true));
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Decreases by time
        shootTimer -= Time.deltaTime;

        // If it is ready to shoot
        if (shootTimer < 0)
        {
            GameObject bullet;
            int i = 0;
            foreach (Transform shootingPos in masksShootingPos)
            {
                bullet = Instantiate(Bullet, shootingPos.position, shootingPos.rotation);
                bullet.GetComponent<MeshRenderer>().material = BulletMaterials[i++];
                bullet.GetComponent<Rigidbody>().velocity = shootingPos.forward * BulletSpeed;
            }

            shootTimer = shootCooldown;
        }
    }
}
