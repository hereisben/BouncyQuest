using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float bounceForce = 5.5f;

    private Animator animator;
    private Rigidbody2D rb;

    // Limits for camera bounds
    public float minX = -9f;
    public float maxX = 11.5f;
    public float minY = -10f;
    public float maxY = 15f;

    private bool onMovingPlatform = false;
    private Transform platformTransform;
    private Vector3 lastPlatformPosition;

    public int starCount = 0;
    public int totalStars = 5;
    public Animator[] starUIAnimators;

    public AudioClip starCollectSound;
    public AudioClip gameOverSound;
    public AudioClip bounceSound;
    public AudioClip winningSound;
    private AudioSource audioSource;
    public AudioSource backgroundMusicSource; // Reference to the background music source

    public GameOverScreen gameOverScreen; // Reference to the GameOverScreen script

    void Start()
    {   
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {   
        UpdateAnimation();
        Move();

        float clampedX = Mathf.Clamp(rb.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(rb.position.y, minY, maxY);

        rb.position = new Vector2(clampedX, clampedY);
    
        if (onMovingPlatform) {
            Vector3 platformMovement = platformTransform.position - lastPlatformPosition;
            transform.position += platformMovement;
            lastPlatformPosition = platformTransform.position;
        }

        if (transform.position.y < -7f) {
            Die();
        }
    }

    void UpdateAnimation() {
        if (isDead) return;
        if (Mathf.Abs(rb.linearVelocity.y) > 0.1f) { // FIXED velocity check
            if (Mathf.Abs(rb.linearVelocity.x) > 0.1f) {
                animator.Play("jumpwalkAnimation");
            } else {
                animator.Play("jumpAnimation");
            }
        } else {
            animator.Play("idleAnimation");
        }
    }

    void Move()
    {
        if (isDead) return;
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y); // FIXED velocity assignment

        if (moveInput > 0) {
            transform.localScale = new Vector3(3, 3, 1);
        } else if (moveInput < 0) {
            transform.localScale = new Vector3(-3, 3, 1);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Bounce();
        } 
        else if (collision.gameObject.CompareTag("MovingPlatform")) {
            ContactPoint2D contact = collision.GetContact(0);

            // FIXED condition: Check if landing on top
            if (contact.normal.y > 0.5f)  
            {
                Bounce();
                onMovingPlatform = true;
                platformTransform = collision.transform;
                lastPlatformPosition = platformTransform.position;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("MovingPlatform")) {
            onMovingPlatform = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Spike") || collision.CompareTag("Thorn")) {
            Die();
        } else if (collision.CompareTag("Star")) {
            CollectStar(collision.gameObject);
        }
    }
    
    private bool isDead = false;

    void Die() {
        if (gameOverSound != null && audioSource != null) {
            audioSource.PlayOneShot(gameOverSound, 0.8f);
        }
        Debug.Log("🔥 Die() function triggered!"); // Check if this prints in the Console
        isDead = true; // Mark player as dead
        rb.linearVelocity = Vector2.zero; // Stop movement
        rb.bodyType = RigidbodyType2D.Kinematic; // Disable physics
        GetComponent<Collider2D>().enabled = false; // Disable collisions
        animator.Play("deathAnimation");
        
        Invoke("ShowGameOverScreen", 1f); // Wait 1 sec, then show Game Over screen
    }

    void ShowGameOverScreen() {
        if (gameOverScreen != null) {
            gameOverScreen.showGameOverScreen(); // Show the Game Over screen
        } else {
            Debug.LogError("GameOverScreen reference is not set in PlayerMovement script.");
        }

        if (backgroundMusicSource != null) {
            backgroundMusicSource.Stop(); // Stop the background music
        } else {
            Debug.LogError("Background music source is not set in PlayerMovement script.");
        }
    }

    void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Bounce() {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, bounceForce); // FIXED velocity assignment
        if (bounceSound != null && audioSource != null) {
            audioSource.PlayOneShot(bounceSound, 0.2f);
        }
    }

    void CollectStar(GameObject star) {
        starCount++;
        Destroy(star);
        if (starCollectSound != null && audioSource != null) {
            audioSource.PlayOneShot(starCollectSound, 0.8f);
        }

        if (starCount <= starUIAnimators.Length) {
            starUIAnimators[starCount - 1].SetTrigger("Collect");
        }

        if (starCount >= totalStars) {
            Debug.Log("All stars collected!");
            if (winningSound != null && audioSource != null) {
                audioSource.PlayOneShot(winningSound, 0.4f);
            }
            Invoke("LoadNextScene", 1f); // Wait 1 sec, then load next scene
        } 
    }

    void LoadNextScene() {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene(nextSceneIndex);
        } else {
            Debug.Log("No more level!");
        }
    }
}
