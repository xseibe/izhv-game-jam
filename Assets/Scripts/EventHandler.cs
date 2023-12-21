using UnityEngine;

public class EventHandler : MonoBehaviour
{
    private LevelManager levelManager;

    public void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (gameObject.tag)
        {
            case "HoldingRope":
                if (other.tag == "LastDomino")
                {
                    levelManager.RopeTrigger();
                    Destroy(gameObject);
                }
                break;
        }
    }
}
