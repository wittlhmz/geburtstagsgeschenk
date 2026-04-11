using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;
using UnityEngine.SceneManagement;

public class greenButton : MonoBehaviour
{
    public VideoPlayer videoPlayer; // das VideoPlayer-Objekt
    public TextMeshProUGUI congratsText;  // Dein TMP Text
    public float fadeDuration = 4f;       // Dauer des Einblendens
    void OnMouseDown()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Play();
            StartCoroutine(ShowTextAfterDelay(7f));
            StartCoroutine(ReturnToMenuAfterDelay(19f));
        }
    }

    IEnumerator ShowTextAfterDelay(float delay)
    {
        // Warten bis Video 7 Sekunden gelaufen ist
        yield return new WaitForSeconds(delay);

        // Text aktivieren
        congratsText.gameObject.SetActive(true);

        // TMP Alpha auf 0
        Color c = congratsText.color;
        c.a = 0;
        congratsText.color = c;

        // Einblenden
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Clamp01(t / fadeDuration);
            congratsText.color = c;
            yield return null;
        }

        // Sicherstellen, dass Alpha auf 1 ist
        c.a = 1f;
        congratsText.color = c;
    }
    IEnumerator ReturnToMenuAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); // 🔥 wichtig!

        SceneManager.LoadScene("mainmenu");
    }
}
