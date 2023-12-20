using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject FirstDomino;
    [SerializeField] GameObject UpPlatformRb;

    private Animator balloonPlatformAnimator;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("ere");
        balloonPlatformAnimator = UpPlatformRb.GetComponent<Animator>();

        DominoAction();
    }

    private void Update()
    {

    }

    public void RopeTrigger()
    {
        balloonPlatformAnimator.SetBool("ElevationTriggered", true);
    }

    private void DominoAction()
    {
        FirstDomino.transform.Rotate(-11, 0, 0);
    }
}
