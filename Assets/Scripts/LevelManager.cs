using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject FirstDomino;
    [SerializeField] Rigidbody UpPlatformRb;

    private bool releaseBalloonPlatform = false;

    // Start is called before the first frame update
    void Start()
    {
        DominoAction();
    }

    public void RopeTrigger()
    {
        UpPlatformRb.AddForce(Vector3.up * 10f, ForceMode.Acceleration);
    }

    private void DominoAction()
    {
        FirstDomino.transform.Rotate(-11, 0, 0);
    }
}
