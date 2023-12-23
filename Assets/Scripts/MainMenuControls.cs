using UnityEngine;

public class MainMenuControls : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject Player;
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject MainMenuCanvas;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        // If the level is only restarted.
        MainMenuControls[] mmControls = FindObjectsOfType<MainMenuControls>();
        if (mmControls.Length != 1 )
        {
            PlayGame();
            Destroy(mmControls[0].gameObject);
            return;
        }

        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void PlayGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Destroy(MainMenu);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Player.SetActive(true);
        Destroy(MainMenuCanvas);
    }

    public void QuitGame()
    {
        #if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
        #elif (UNITY_WEBPLAYER || UNITY_WEBGL)
            Application.ExternalEval("document.location.reload(true)");
        #else // !UNITY_WEBPLAYER
            Application.Quit();
        #endif
    }
}
