using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class infoUI : MonoBehaviour
{
    public static bool isUseCheat;
    public static bool isPressESC;
    
    private void Update()
    {
        if (isUseCheat)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            SceneManager.LoadScene("Nightrol");
        }
        else
        {
            isUseCheat = false;
        }
    }
}
