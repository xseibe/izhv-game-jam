using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("References")]
    [CanBeNull]
    [SerializeField] GameObject FirstDomino;
    [CanBeNull]
    [SerializeField] GameObject UpPlatform;
    [CanBeNull]

    [SerializeField] Animator CanvasAnimator;

    private Animator balloonPlatformAnimator;
    private float timeNeededToReset = 2f;
    private float rPressedTime = 0f;
    private bool playerOnBoard = false;

    // Start is called before the first frame update
    void Start()
    {
        if (UpPlatform != null)
            balloonPlatformAnimator = UpPlatform.GetComponent<Animator>();
    }

    private void Update()
    {
        // Reset level
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

    public void ResetLevel()
    {
        // Reloads the current scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
