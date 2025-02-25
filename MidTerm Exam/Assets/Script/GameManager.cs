using UnityEngine;

public enum Player
{
    Paddle
}

public class GameManager : MonoBehaviour
{
    // Single Reference Static Variables
    public static GameManager Instance { get; private set;}

    // Ball Variables
    private Vector2 currentVelocity;
    public Rigidbody2D ballRigidbody;
    public float ballSpeed = 5f;
    private int reset;


    // Unity Function
    private void Awake()
    {
        // Ensure that there is only one instance of GameManager
        if(Instance == null)
        {
            Instance = this;
            // Intermediate Unity Tip and Trick
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
    //Method Created to Reset the Game
        reset = Input.GetKey(KeyCode.R) ? 1 : 0;
        if(reset != 0)
        {
            Time.timeScale = 1;
            ResetBall();
        }
    }

    private void Start()
    {
        ResetBall();
    }

    public void ResetBall()
    {
        ballRigidbody.transform.position = new  (-0.03144836f,-2.609512f);
        
        float randY = Random.Range(0, 2) == 0 ? -1 : 1;
        float randX = Random.Range(-0.5f, 0.5f);

        // Set the ball's velocity
        Vector2 direction = new Vector2(randX, randY).normalized;
        ballRigidbody.linearVelocity = direction * ballSpeed;
        SetCurrentVelocity(ballRigidbody.linearVelocity);
    }

    public Vector2 GetCurrentVelocity()
    {
        return currentVelocity;
    }

    public void SetCurrentVelocity(Vector2 velocity)
    {
        currentVelocity = velocity;
        ballRigidbody.linearVelocity = currentVelocity;
    }

    public void StopGame()
    {
        Time.timeScale = 0;
    }
}