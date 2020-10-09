using AssemblyCSharp.Assets.Scripts.Models;
using AssemblyCSharp.Assets.Scripts.Data;
using AssemblyCSharp.Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPanel : MonoBehaviour
{
    public static bool IsPaused = false;

    public void RunEmulation()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        IsPaused = false;
    }

    public void PauseEmulation()
    {
        if (!IsPaused)
        {
            Time.timeScale = 0f;
            IsPaused = true;
        }
        else
        {
            Time.timeScale = 1f;
            IsPaused = false;
        }
    }
}
