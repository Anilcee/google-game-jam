using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{
    public static System.Action OnButtonPressed;

    private bool playerNearby = false;
    public DoorController doorController;

    void Update()
    {
        if(playerNearby && Input.GetKeyDown(KeyCode.B))
        {
            TriggerButton();
        }
    }

    public void TriggerButton()
    {
        Debug.Log("button pressed");
        doorController.OperateDoor();
        OnButtonPressed?.Invoke();
        
        GhostRecorder recorder = GameObject.FindGameObjectWithTag("Player")?.GetComponent<GhostRecorder>();
        recorder?.NotifyButtonPressed();
    }

    void OnTriggerEnter2D(Collider2D other) => playerNearby = other.CompareTag("Player");
    void OnTriggerExit2D(Collider2D other) => playerNearby = other.CompareTag("Player") ? false : playerNearby;
}