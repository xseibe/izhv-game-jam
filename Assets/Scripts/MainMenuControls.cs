using UnityEngine;

public class MainMenuControls : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject Player;
    [SerializeField] GameObject MainMenu;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        Cursor.visible = true;
    }

    public void PlayGame()
    {
        Destroy(MainMenu);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Player.SetActive(true);
        Destroy(gameObject);
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
