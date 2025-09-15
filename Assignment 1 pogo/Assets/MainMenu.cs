using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void Play()
    {
      SceneManager.LoadScene("Gameplay");  
    }

    // Update is called once per frame
    public void Exit()
    {
        Debug.Log("Game is exiting...");
        Application.Quit();
    }
}
