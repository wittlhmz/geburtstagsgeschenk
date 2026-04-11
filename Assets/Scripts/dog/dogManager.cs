using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class dogManager : MonoBehaviour
{
    public static dogManager instance;
    public AudioSource audioSource;

    public int sheepCount = 6;
    public GameObject winPanel;
    public TextMeshProUGUI sheepText;

    void Awake()
    {
        instance = this;
    }

    public void SheepArrived()
    {
        sheepCount--;
        sheepText.text = "Noch " + sheepCount + " Schafe übrig";

        Debug.Log("Schafe übrig: " + sheepCount);

        if (sheepCount <= 0)
        {
            StartCoroutine(FinishWithDelay());
            StartCoroutine(LoadWithDelay());
        }
    }

    IEnumerator FinishWithDelay()
    {
        yield return new WaitForSeconds(2f); // ⏳ 2 Sekunden warten
        FinishGame();
    }

        void FinishGame()
    {
        audioSource.Play();
        GameManager.instance.dogGameWon = true;

        // 🪟 UI anzeigen
        winPanel.SetActive(true);

        // 🛑 Spiel pausieren
        //Time.timeScale = 0f;
    }
    IEnumerator LoadWithDelay()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("mainmenu");
    }
}
