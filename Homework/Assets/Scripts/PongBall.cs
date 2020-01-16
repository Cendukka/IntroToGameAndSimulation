using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PongBall : MonoBehaviour

{
  
    //Speed of the ball
    public float pongBallSpeed = 10.0f;
    //Starting location of the ball
    PongBall pongBall;
    Vector2 pongBallStart;
    //Boolean if the game is on or not
    private bool gameIsOn = false;
    //Moving vector
    Vector2 moving;
    //place holder for points
    int pointsBlue = 0;
    int pointsRed = 0;
    //text gameobjects
    Text bluePointText;
    Text redPointText;
    
    

   // Start is called before the first frame update
    void Start()
    {
        //Set moving direction: x:1, y:1 and add the coordinates to current position with normalized
        moving = Vector2.one.normalized;
        //set gameobjects
        pongBall = gameObject.GetComponent<PongBall>();
        
        bluePointText = GameObject.Find("PlayerBluePoints").GetComponent<Text>();
        redPointText = GameObject.Find("PlayerRedPoints").GetComponent<Text>();
        //attach component
        
        //save start position
        pongBallStart = pongBall.transform.position;
        //Debug.Log(pongBallStart);
    }

    // Update is called once per frame
    void Update()
    {
        //Start game by pressing space
        if (Input.GetKey("space"))
        {
            gameIsOn = true;
        }

        if (gameIsOn)
        {
            transform.Translate(moving * pongBallSpeed * Time.deltaTime);
        }
        

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        
     
        if (collision.gameObject.name == "NorthWall" || collision.gameObject.name == "SouthWall")
        {
            moving.y = -moving.y;
        }
     
        if (collision.gameObject.name == "Paddle1" || collision.gameObject.name == "Paddle2")
        {
            moving.x = -moving.x;
        }
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Paddle1Goal")
        {
            pongBall.transform.position = pongBallStart;
            pointsBlue += 1;
            gameIsOn = false;
            bluePointText.text = "Blue Points: " + pointsBlue.ToString();
        }
        if (collision.gameObject.name == "Paddle2Goal")
        {
            pongBall.transform.position = pongBallStart;
            pointsRed += 1;
            gameIsOn = false;
            redPointText.text = "Red Points: " + pointsRed.ToString();
        }
    }
}
