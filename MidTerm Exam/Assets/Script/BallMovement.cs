using System;
using UnityEngine;
using UnityEngine.Rendering;

public class BallMovement : MonoBehaviour
{
    // Ball Movement Script will trigger Scoring functions as it hits a goal

    // Const Strings
    private const string PaddleTag = "Paddle";
    private const string BoundarySide = "BoundarySide";
    private const string BoundaryTop = "BoundaryTop";
    private const string BoundaryBottom = "BoundaryBottom";
    private int playerlife = 3;
    private int targetcounter = 9;

    // Private Variables
    private Rigidbody2D rBody;

    private void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag(PaddleTag))
        {
            //Ball hits the paddle
            HandlePaddleCollision(other);
        }
        else if(other.gameObject.CompareTag(BoundarySide))
        {
            //Ball Hits the sides
            HandleBoundarySideCollision();
        }
        else if(other.gameObject.CompareTag(BoundaryTop))
        {
            //Ball Hits the top
            HandleCeilingTopCollision();
        }
        else if(other.gameObject.CompareTag(BoundaryBottom)){
            //Ball Hits the floor
            HandleCeilingFloorCollision();
        }
        else{
            //Ball Hits the target
            HandleTargetCollision(other);
        }
    }

    private void HandlePaddleCollision(Collision2D other)
    {
        Vector2 currentVelocity = GameManager.Instance.GetCurrentVelocity();
        // Calculate bounce angle
        float x = CalculateBounceAngle(other.transform.position, transform.position, other.collider.bounds.size.x);
        currentVelocity = new Vector2(x, currentVelocity.y * -1).normalized * GameManager.Instance.ballSpeed;
        GameManager.Instance.SetCurrentVelocity(currentVelocity);
    }

    private void HandleCeilingTopCollision()
    {
        Vector2 currentVelocity = GameManager.Instance.GetCurrentVelocity();
        currentVelocity = new Vector2(currentVelocity.x, currentVelocity.y * -1);
        GameManager.Instance.SetCurrentVelocity(currentVelocity);
    }
    private void HandleBoundarySideCollision()
    {
        Vector2 currentVelocity = GameManager.Instance.GetCurrentVelocity();
        currentVelocity = new Vector2(currentVelocity.x * -1, currentVelocity.y);
        GameManager.Instance.SetCurrentVelocity(currentVelocity);
    }

    private void HandleCeilingFloorCollision()
    {
        if(playerlife == 3){
            playerlife--;
            GameManager.Instance.ResetBall();
            Destroy (GameObject.FindWithTag("c3"));
        }
        else if(playerlife == 2){
            playerlife--;
            GameManager.Instance.ResetBall();
            Destroy (GameObject.FindWithTag("c2"));
        }
        else if(playerlife == 1){
            playerlife--;
            GameManager.Instance.ResetBall();
            Destroy (GameObject.FindWithTag("c1"));
        }
        else{
            GameManager.Instance.StopGame();
            Debug.Log("You Lost");
        }
    }

    private void HandleTargetCollision(Collision2D other){
        
        Vector2 currentVelocity = GameManager.Instance.GetCurrentVelocity();
         // Calculate bounce angle
        float x = CalculateBounceAngle(other.transform.position, transform.position, other.collider.bounds.size.x);
        currentVelocity = new Vector2(x, currentVelocity.y * -1).normalized * GameManager.Instance.ballSpeed;
        GameManager.Instance.SetCurrentVelocity(currentVelocity);

        if (other.gameObject.GetComponent<SpriteRenderer>().color == new Color(0f, 0.1966419f, 1f, 1f)){
            //Changing to Green
            other.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.05490196f, 1f, 0f, 1f);
        }
        else if (other.gameObject.GetComponent<SpriteRenderer>().color == new Color(0.05490196f, 1f, 0f, 1f)){
            //Changing to White
            other.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
        else if (other.gameObject.GetComponent<SpriteRenderer>().color == new Color(1f, 1f, 1f, 1f)){
            //Enimes are Destroyed
            Destroy(other.gameObject);
            targetcounter--;
            if(targetcounter == 0){
                GameManager.Instance.StopGame();
                Debug.Log("You Won the Game");
            }
        }


    }

    private float CalculateBounceAngle(Vector2 ballPos, Vector2 paddlePos, float paddleHeight)
    {
        return (ballPos.x - paddlePos.x) / paddleHeight * 7f;
    }
}
