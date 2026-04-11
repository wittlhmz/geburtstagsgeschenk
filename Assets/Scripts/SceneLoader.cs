using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadSheepGame()
    {
        SceneManager.LoadScene("SheepGame");
    }

    public void LoadMusicGame()
    {
        SceneManager.LoadScene("MusicGame");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
