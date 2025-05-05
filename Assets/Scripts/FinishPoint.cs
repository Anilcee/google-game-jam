using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Bitiş noktasına ulaşıldı. Oyun bitiyor.");
            SceneManager.LoadScene("VictoryScene");
        }
    }
}
