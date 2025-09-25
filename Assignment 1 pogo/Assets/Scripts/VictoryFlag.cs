using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryFlag : MonoBehaviour
{
    [Header("Victory Settings")]
    public GameObject victoryUI;
    public float delayBeforeAction = 2f;
    public string nextSceneName = "MainMenu";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowVictoryUI();
        }
    }

    void ShowVictoryUI()
    {
        if (victoryUI != null)
        {
            victoryUI.SetActive(true);
        }

        Invoke("LoadNextScene", delayBeforeAction);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}