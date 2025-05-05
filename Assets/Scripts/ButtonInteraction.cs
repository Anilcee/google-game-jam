using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{
    public static System.Action OnButtonPressed;

    private bool playerNearby = false;
    public DoorController doorController;

    public SpriteRenderer buttonRenderer; // Butonun görseli
    public Color defaultColor = Color.red;
    public Color pressedColor = Color.green;

    private void Start()
    {
        if (buttonRenderer != null)
        {
            buttonRenderer.color = defaultColor;
        }
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.B))
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

        if (buttonRenderer != null)
        {
            StopAllCoroutines(); // Önceki coroutine varsa durdur
            StartCoroutine(ChangeColorTemporarily());
        }
    }

    private System.Collections.IEnumerator ChangeColorTemporarily()
    {
        buttonRenderer.color = pressedColor;
        yield return new WaitForSeconds(2f);
        buttonRenderer.color = defaultColor;
    }

    void OnTriggerEnter2D(Collider2D other) => playerNearby = other.CompareTag("Player");
    void OnTriggerExit2D(Collider2D other) => playerNearby = other.CompareTag("Player") ? false : playerNearby;
}
