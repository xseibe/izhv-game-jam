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
            case "UpPlatform":
                if (other.gameObject.tag == "Player")
                {
                    levelManager.PlayerOnBoard(true);

                }
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (gameObject.tag)
        {
            case "UpPlatform":
                if (other.gameObject.tag == "Player")
                    levelManager.PlayerOnBoard(false);
                break;
        }
    }
}
