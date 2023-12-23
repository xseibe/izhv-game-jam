using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject PausePanel;

    [CanBeNull]
    [SerializeField] GameObject FirstDomino;
    [CanBeNull]
    [SerializeField] GameObject UpPlatform;
    [CanBeNull]
    [SerializeField] GameObject Lvl2IntroTextPanel;

    [SerializeField] Animator CanvasAnimator;

    [HideInInspector]
    public bool GamePaused { get; private set; } = false;

    private Animator balloonPlatformAnimator;
    private float timeNeededToReset = 2f;
    private float rPressedTime = 0f;
    private bool playerOnBoard = false;
    private bool IsCutscene = false;

    private GameObject cutscenePanel;

    // Start is called before the first frame update
    void Start()
    {
        // Assign current level's cutscene
        cutscenePanel = Lvl2IntroTextPanel ?? null;

        if (UpPlatform != null)
            balloonPlatformAnimator = UpPlatform.GetComponent<Animator>();
    }

    private void Update()
    {
        // Handle pausing the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused && IsCutscene && cutscenePanel != null) CutsceneEnd(cutscenePanel);
            else if (GamePaused) UnpauseGame();
            else PauseGame();
        }

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

    public void PauseGame()
    {
        if (!IsCutscene)
        PausePanel.SetActive(true);

        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void UnpauseGame()
    {
        if (!IsCutscene)
        PausePanel.SetActive(false);

        Time.timeScale = 1f;
        GamePaused = false;
    }

    public void CutsceneStart()
    {
        IsCutscene = true;
        PauseGame();
    }
    public void CutsceneEnd(GameObject cutscenePanel)
    {
        UnpauseGame();

        cutscenePanel.SetActive(false);
        IsCutscene = false;
    }
}
