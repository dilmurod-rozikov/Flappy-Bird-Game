using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;//control the speed
    private Rigidbody2D rb;
    public bool isDead = false;
    private AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip deathSound;
    private Animator animator;

    private SpriteRenderer spriteRenderer;

    public Sprite[] sprites;

    private int spriteIndex;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        //get rigibody2d Component of the BirdbBek
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        InvokeRepeating(nameof(AnimateSprite), 0.2f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        //GetMouseButtonDown(0) --> left side mouse button pressed down
        if (Input.GetMouseButtonDown(0) && !isDead)
        {
            if (audioSource != null && jumpSound != null)
                audioSource.PlayOneShot(jumpSound);

            rb.velocity = Vector2.up * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!isDead) // To ensure the sound plays only once
        {
            isDead = true;
            PlayDeathAnimation();

        }
    }

    private void PlayDeathAnimation()
    {
        // Play death sound
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        StartCoroutine(RotatePlayerSmoothly(180f, 2f));
    }

    private IEnumerator RotatePlayerSmoothly(float angle, float duration)
    {
        float timeElapsed = 0f;
        Quaternion startingRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        while (timeElapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(startingRotation, targetRotation, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure the final rotation is set
        transform.rotation = targetRotation;
    }

    private void AnimateSprite()
    {
        spriteIndex++;
        if (spriteIndex >= sprites.Length)
            spriteIndex = 0;

        spriteRenderer.sprite = sprites[spriteIndex];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "obstacles")
        {
            FindObjectOfType<GameManager>().GameOver();
        }
        else if (other.gameObject.tag == "scoring")
        {
            FindObjectOfType<GameManager>().IncreaseScore();
        }
    }
}
