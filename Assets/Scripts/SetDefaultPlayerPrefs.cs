using UnityEngine;


// Sets default values of PlayerPrefs settings
public class SetDefaultPlayerPrefs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Movement
        PlayerPrefs.SetString("ForwardKey", KeyCode.W.ToString());
        PlayerPrefs.SetString("BackKey", KeyCode.S.ToString());
        PlayerPrefs.SetString("LeftKey", KeyCode.A.ToString());
        PlayerPrefs.SetString("RightKey", KeyCode.D.ToString());

        PlayerPrefs.SetString("JumpKey", KeyCode.Space.ToString());
        PlayerPrefs.SetString("CrouchKey", KeyCode.LeftControl.ToString());
        PlayerPrefs.SetString("SprintKey", KeyCode.LeftShift.ToString());

        // Mouse
        PlayerPrefs.SetFloat("MouseSensitivity", 120.5f);

    }
}
