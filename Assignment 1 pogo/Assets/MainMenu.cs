using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void Play()
    {
      SceneManager.LoadScene("Gameplay");  
    }
    public void Exit()
    {
        Debug.Log("Game is exiting pls wait...");
        Application.Quit();
    }
}
