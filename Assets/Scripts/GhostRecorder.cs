using UnityEngine;
using System.Collections.Generic;

public class GhostRecorder : MonoBehaviour
{
    private bool buttonPressed = false;

    public void NotifyButtonPressed()
    {
        buttonPressed = true;
    }
    public float recordTime = 3f;
    private List<GhostRecorderData> records = new List<GhostRecorderData>();
    private float timer = 0f;
    private bool isRecording = true;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!isRecording) return;
        timer += Time.deltaTime;

        float speed = anim != null ? anim.GetFloat("speed") : 0f;
        bool jump = anim != null ? anim.GetBool("jump") : false;
        bool grounded = anim != null ? anim.GetBool("grounded") : false;
        bool flipX = spriteRenderer != null ? spriteRenderer.flipX : false;
        records.Add(new GhostRecorderData(transform.position, transform.rotation, speed, jump, grounded, flipX, buttonPressed));
        if (timer > recordTime)
        {
            records.RemoveAt(0);
        }
        buttonPressed = false;
    }

    public List<GhostRecorderData> GetLastRecords()
    {
        return new List<GhostRecorderData>(records);
    }

    public void StopRecording() { isRecording = false; }
    public void StartRecording() { isRecording = true; timer = 0f; records.Clear(); }
}
