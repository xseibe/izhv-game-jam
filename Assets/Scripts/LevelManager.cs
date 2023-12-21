using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject FirstDomino;
    [SerializeField] GameObject UpPlatform;
    [SerializeField] Animator CanvasAnimator;

    private Animator balloonPlatformAnimator;
    private float timeNeededToReset = 2f;
    private float rPressedTime = 0f;
    private bool playerOnBoard = false;

    // Start is called before the first frame update
    void Start()
    {
        balloonPlatformAnimator = UpPlatform.GetComponent<Animator>();

       // DominoAction();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            rPressedTime = Time.time;
        }
        else if (Input.GetKey(KeyCode.R))
        {
            if (Time.time - rPressedTime > timeNeededToReset)
            {
                rPressedTime = float.PositiveInfinity;

                ResetLevel();
            }
        }
    }

    public void RopeTrigger()
    {
        balloonPlatformAnimator.SetBool("ElevationTriggered", true);

        // If player is on the platform, show the popup text.
        if (playerOnBoard)
            CanvasAnimator.SetTrigger("FirstLevelP");
    }

    public void PlayerOnBoard(bool stat)
    {
        playerOnBoard = stat;
        if (stat && balloonPlatformAnimator.GetBool("ElevationTriggered"))
            CanvasAnimator.SetTrigger("FirstLevelP");
    }

    private void DominoAction()
    {
        FirstDomino.transform.Rotate(-11, 0, 0);
    }

    private void ResetLevel()
    {
        // Reloads the current scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
