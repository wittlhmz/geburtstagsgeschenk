using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RightButton : MonoBehaviour
{
    public Transform maskTransform;
    private Vector3 originalScale;
    public float scaleMultiplier = 1.1f;

    void Start()
    {
        originalScale = maskTransform.localScale;
    }

    void OnMouseEnter()
    {
        maskTransform.localScale = new Vector3(
            originalScale.x * scaleMultiplier,
            originalScale.y * scaleMultiplier,
            originalScale.z
        );
    }

    void OnMouseExit()
    {
        maskTransform.localScale = originalScale;
    }

    void OnMouseDown()
    {
        SceneManager.LoadScene("musicGame");
    }
}
