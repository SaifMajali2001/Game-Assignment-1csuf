using UnityEngine;
using System.Collections;

public class ControlsUI : MonoBehaviour
{
    [Header("UI Settings")]
    public float displayDuration = 8f;

    void Start()
    {
        // Hide after delay
        StartCoroutine(HideControlsAfterDelay());
    }

    IEnumerator HideControlsAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        gameObject.SetActive(false);
    }
}