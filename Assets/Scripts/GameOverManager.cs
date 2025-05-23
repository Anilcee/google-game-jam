using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void PlayAgain()
    {
        string lastLevel = PlayerPrefs.GetString("LastLevel", "Level1");
        SceneManager.LoadScene(lastLevel);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Oyun kapatılıyor...");
    }
}
