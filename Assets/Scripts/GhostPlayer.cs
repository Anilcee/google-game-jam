using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GhostPlayer : MonoBehaviour
{
    public float playbackSpeed = 1f;
    private List<GhostRecorderData> records;
    private int currentIndex = 0;
    private bool isPlaying = false;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Collider'ı trigger yap
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.isTrigger = true;
    }

    private bool lastFlipX = false; // flipX'i LateUpdate'de uygula
    void LateUpdate()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = lastFlipX;
        }
    }

    public void Play(List<GhostRecorderData> data)
    {
        records = data;
        currentIndex = 0;
        isPlaying = true;
        StartCoroutine(Playback());
    }

    IEnumerator Playback()
    {
        // Animator'ı baştan al
        if (anim == null) anim = GetComponent<Animator>();
        while (isPlaying && currentIndex < records.Count)
        {
            var rec = records[currentIndex];
            transform.position = rec.position;
            transform.rotation = rec.rotation;
            // Animasyon parametrelerini uygula
            if (anim != null)
            {
                anim.SetFloat("speed", rec.speed);
                anim.SetBool("jump", rec.jump);
                anim.SetBool("grounded", rec.grounded);
            }
            // FlipX uygula (değeri LateUpdate'de uygula)
            lastFlipX = rec.flipX;

            // Eğer bu kayıtta buttonPressed true ise ghost butona basıyor gibi davran
            if (rec.buttonPressed)
            {
                Debug.Log("Ghost butona bastı!");
                // Butonun collider'ı ile overlap varsa, kapıyı aç
                Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.5f); // veya küçük bir radius
                foreach (var hit in hits)
                {
                    ButtonInteraction button = hit.GetComponent<ButtonInteraction>();
                    if (button != null)
                    {
                        button.doorController.OperateDoor();
                    }
                }
            }
            currentIndex++;
            yield return null;
        }
        Destroy(gameObject);
    }
}
