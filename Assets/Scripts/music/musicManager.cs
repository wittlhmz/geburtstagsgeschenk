using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class musicManager : MonoBehaviour
{
    public int currentSongID;
    public int currentSongYear;
    public AudioSource audioSource;
    public GameObject winPanel;
    public TextMeshProUGUI songText;
    public TextMeshProUGUI evaluation;
    public Slot[] slots;
    public GameObject[] backgrounds;
    Color correctColor = Color.green;
    Color wrongColor = Color.red;
    void Start()
    {
        currentSongID = Random.Range(0, 5);
        switch (currentSongID)
        {
            case 0: currentSongYear = 2004; break;
            case 1: currentSongYear = 2009; break;
            case 2: currentSongYear = 2010; break;
            case 3: currentSongYear = 2011; break;
            case 4: currentSongYear = 2014; break;
        }
        songText.text = "Ordne den Song aus dem Jahr " + currentSongYear;
    }
    public void PlayAll()
    {
        StartCoroutine(PlaySequenceCoroutine());
    }
    void OnMouseDown()
    {
        PlayAll();
    }
    IEnumerator LoadWithDelay()
    {
        yield return new WaitForSeconds(5f); // ⏳ 2 Sekunden warten
        SceneManager.LoadScene("mainmenu");
    }

        void FinishGame()
    {
        audioSource.Play();
        GameManager.instance.musicGameWon = true;

        // 🪟 UI anzeigen
        winPanel.SetActive(true);

        // 🛑 Spiel pausieren
        //Time.timeScale = 0f;
    }

    /*public void CheckOrder()
    {
        int expectedOrder = 0;

        for (int i = 0; i < slots.Length; i++)
        {
            GameObject piece = slots[i].currentPiece;

            if (piece == null)
            {
                evaluation.text = "Hier sind noch Lücken!";
                return;
            }

            AudioPiece ap = piece.GetComponent<AudioPiece>();

            // ❗ Falsches Lied
            if (ap.songID != currentSongID)
            {
                evaluation.text = "Falsches Lied!";
                return;
            }

            // ❗ Falsche Reihenfolge
            if (ap.orderIndex != expectedOrder)
            {
                evaluation.text = "Falsche Reihenfolge!";
                return;
            }

            expectedOrder++;
        }

        evaluation.text ="Richtig!";
    }*/

    IEnumerator PlaySequenceCoroutine()
    {
        int expectedOrder = 0;
        string resultText = "Richtig!";
        string result = resultText;

        for (int i = 0; i < slots.Length; i++)
        {
            GameObject piece = slots[i].currentPiece;
            SpriteRenderer bg = backgrounds[i].GetComponent<SpriteRenderer>();
            // 🎧 Audio abspielen
            if (piece != null)
            {
                AudioSource audio = piece.GetComponent<AudioSource>();

                if (audio != null && audio.clip != null)
                {
                    audio.Play();
                    yield return new WaitForSeconds(audio.clip.length);
                }

                AudioPiece ap = piece.GetComponent<AudioPiece>();

                // ❗ Falsches Lied
                if (ap.songID != currentSongID)
                {
                    result = "Falsches Lied!";
                    bg.color = wrongColor;
                }

                // ❗ Falsche Reihenfolge
                else if (ap.orderIndex != expectedOrder)
                {
                    result = "Falsche Reihenfolge!";
                    bg.color = wrongColor;
                }

                else
                {
                    evaluation.text = result;
                    bg.color = correctColor;
                }
        
            }
            // ❗ Lücke
            else
            {
                result = "Hier sind noch Lücken!";
            }
            evaluation.text = result;


            expectedOrder++;
        }

        yield return new WaitForSeconds(0.5f);
        if(evaluation.text == resultText)
        {
            FinishGame();
            StartCoroutine(LoadWithDelay());
        }

    }
}
