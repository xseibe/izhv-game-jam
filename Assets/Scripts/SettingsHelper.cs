using System;
using UnityEngine;

public class SettingsHelper : MonoBehaviour
{
    private static SettingsHelper instance;

    public KeyCode ForwardKey;
    public KeyCode JumpKey;
    public KeyCode SprintKey;
    public KeyCode CrouchKey;

    public float MouseSensitivity;

    public SettingsHelper()
    {
        ForwardKey = Enum.TryParse<KeyCode>(PlayerPrefs.GetString("ForwardKey"), out ForwardKey) ? ForwardKey : KeyCode.W;
        JumpKey = Enum.TryParse<KeyCode>(PlayerPrefs.GetString("JumpKey"), out JumpKey) ? JumpKey : KeyCode.Space;
        SprintKey = Enum.TryParse<KeyCode>(PlayerPrefs.GetString("SprintKey"), out SprintKey) ? SprintKey : KeyCode.LeftShift;
        CrouchKey = Enum.TryParse<KeyCode>(PlayerPrefs.GetString("CrouchKey"), out CrouchKey) ? CrouchKey : KeyCode.LeftControl;
        MouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
    }

    public static SettingsHelper GetInstance()
    {
        if (instance == null)
        {
            instance = new SettingsHelper();
        }

        return instance;
    }
}
