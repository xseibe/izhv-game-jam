using System;
using UnityEngine;

public class SettingsHelper
{
    private static SettingsHelper instance;

    public KeyCode ForwardKey;
    public KeyCode JumpKey;
    public KeyCode SprintKey;
    public KeyCode InteractionKey;

    public float MouseSensitivity;

    public SettingsHelper()
    {
        ForwardKey = Enum.TryParse<KeyCode>(PlayerPrefs.GetString("ForwardKey"), out ForwardKey) ? ForwardKey : KeyCode.W;
        JumpKey = Enum.TryParse<KeyCode>(PlayerPrefs.GetString("JumpKey"), out JumpKey) ? JumpKey : KeyCode.Space;
        SprintKey = Enum.TryParse<KeyCode>(PlayerPrefs.GetString("SprintKey"), out SprintKey) ? SprintKey : KeyCode.LeftShift;
        MouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
        InteractionKey = Enum.TryParse<KeyCode>(PlayerPrefs.GetString("InteractionKey"), out InteractionKey) ? InteractionKey : KeyCode.E;
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
