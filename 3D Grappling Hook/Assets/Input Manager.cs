using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public static bool RButton()
    {
        return Input.GetButtonDown("R_Button");
    }

    public static bool FButton()
    {
        return Input.GetButtonDown("F_Button");
    }
}
