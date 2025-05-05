using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadScene("Chapter_1_FirstLab");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Oyun kapatılıyor...");
    }
}

