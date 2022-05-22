using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //closes the application on input of escape key
        {
#if UNITY_EDITOR //if in Editor
            UnityEditor.EditorApplication.isPlaying = false; //sets play mode to false
#endif
            Application.Quit();
        }
    }
}
