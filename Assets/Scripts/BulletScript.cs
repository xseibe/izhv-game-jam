using System;
using System.Linq;
using UnityEngine;

public enum Colors { Red, Blue, Green, Yellow };

public class BulletScript : MonoBehaviour
{
    private float destroyTime = 5f;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the collision object is not a balloon, skip.
        string collisionTagName = collision.gameObject.tag;
        if (!collisionTagName.Contains("Balloon"))
        {
            Destroy(gameObject);
            return;
        }

        string bulletMaterialName = gameObject.GetComponent<MeshRenderer>().material.ToString();

        // If the colors are not matching
        if (Enum.GetNames(typeof(Colors))
            .Any(clr => bulletMaterialName.Contains(clr) && !collisionTagName.Contains(clr)))
        {
            // Destroy balloon platform
            Destroy(collision.gameObject.transform.parent.parent.gameObject);
        }

        // Destroy bullet
        Destroy(gameObject);
    }
}
