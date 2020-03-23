using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreakoutBall : MonoBehaviour
{

    //Speed of the ball
    public float breakoutBallSpeed = 25.0f;
    //Boolean if the game is on or not
    private bool gameIsOn = false;
    //Paddles
    GameObject breakoutPaddle;
    //Placeholder for Starting location of the ball
    GameObject breakoutBall;
    //placeholders for the ball on the paddle
    GameObject paddleBallPlaceHolder;
    //Moving vector
    Vector2 moving;
    //Score text
    Text scoreText;
    //score
    int scorePoints;

    // Start is called before the first frame update
    void Start()
    {
        initGameobjects();
        initVectors();
        stickTheBall();
        scoreText = GameObject.Find("scoreText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (checkGameStart())
        {
            transform.Translate(moving * breakoutBallSpeed * Time.deltaTime);
        }
        else
        {
            breakoutBall.transform.position = paddleBallPlaceHolder.transform.position;
        }
    }
    void initGameobjects()
    {
        //paddle
        breakoutPaddle = GameObject.Find("BreakoutPaddle");

        //Paddle's placeholder for the ball
        paddleBallPlaceHolder = GameObject.Find("PaddleBallPlaceHolder");

        //Breakout Ball
        breakoutBall = GameObject.Find("BreakoutBall");
    }
        void initVectors()
    {
        //Set moving direction: x:1, y:1 and add the coordinates to current position with normalized
        moving = Vector2.one.normalized;
    }

    bool checkGameStart()
    {
        if (!gameIsOn)
        {


            if (Input.GetKey("space"))
            {
                gameIsOn = true;
                return gameIsOn;
            }
            else
            {
                gameIsOn = false;
                return gameIsOn;
            }
        }
        return gameIsOn;
    }
    void stickTheBall()
    {
        gameIsOn = false;
        breakoutBall.transform.position = paddleBallPlaceHolder.transform.position;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        string coll = collision.gameObject.name;
        switch (coll)
        {
            case "NorthWall":
                moving.y = -moving.y;
                break;
            case "WestWall":
                moving.x = -moving.x;
                break;
            case "EastWall":
                moving.x = -moving.x;
                break;
            case "BreakoutPaddle":
                moving.y = -moving.y;
                break;
            default:
                break;
        }
        if(collision.gameObject.tag == "BreakoutBlock")
        {
            moving.y = -moving.y;
            scorePoints += 1;
            scoreText.text = "Score: " + scorePoints.ToString();
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("YAY");
        if (collision.gameObject.name == "DeathZone")
        {
            Debug.Log("YAY");
            gameIsOn = false;
            breakoutBall.transform.position = paddleBallPlaceHolder.transform.position;
        }
    }
}
