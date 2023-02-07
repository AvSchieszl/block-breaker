using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] PaddleController paddle = default;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;

    Vector2 paddleBallOffset;

    bool hasStarted = false;

    AudioSource audioSource;
    Rigidbody2D rigidBody2D;

    void Start()
    {
        paddleBallOffset = transform.position - paddle.transform.position;
        audioSource = GetComponent<AudioSource>();
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasStarted)
        {
            LockToPaddle();
            LaunchOnMouseClick();
        }
    }
    private void LockToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        transform.position = paddlePos + paddleBallOffset;
    }
    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            rigidBody2D.velocity = new Vector2 (xPush, yPush);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2
            (Random.Range(0f, randomFactor),
             Random.Range(0f, randomFactor));

        if (hasStarted)
        {
            AudioClip clip = ballSounds[Random.Range(0,ballSounds.Length)];
            audioSource.PlayOneShot(clip);
            rigidBody2D.velocity += velocityTweak;
        }
    }
}