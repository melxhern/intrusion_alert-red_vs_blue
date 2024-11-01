using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EControlType
{
    JoyStick,
    KeyboardMouse,
    Mouse
}

public class PlayerSettings
{
    public static EControlType controlType;
    public static string nickname;
}
