using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;



public class PongBall : MonoBehaviour

{

    //place holder for points
    int pointsBlue = 0;
    int pointsRed = 0;

    //Speed of the ball
    public float pongBallSpeed = 25.0f;
    //Paddle 2xspeed timers
    float timerSpeedPaddle1;
    float timerSpeedPaddle2;
    //timer for clone paddles
    float timerPaddle1Clone;
    float timerPaddle2Clone;
    //timer for sticked ball
    float timerStickedBall1;
    float timerStickedBall2;

    //bool for checking if there is clone already
    bool paddle1CloneBool = false;
    bool paddle2CloneBool = false;
    //Boolean if the game is on or not
    private bool gameIsOn = false;
    //Boolean for ballstick
    bool ballWillStick = false;

    //Placeholder for Starting location of the ball
    PongBall pongBall;
    Vector3 pongBallStart;

    //placeholders for the sticked ball on the paddles
    GameObject stickPlaceHolder1;
    GameObject stickPlaceHolder2;
    //Paddles
    GameObject paddle1;
    GameObject paddle2;
    //additional paddles
    public GameObject paddle1Clone;
    public GameObject paddle2Clone;

    //Moving vector
    Vector2 moving;
    //text gameobjects
    Text bluePointText;
    Text redPointText;

    //include power up handler script
    GameObject powerUpHandler;
    PowerUpHandler powerUpHandlerScript;

    //last player the ball has hit
    string lastPlayerString;
    GameObject lastPlayer;
    //BasicAI script
    public BasicAI basicAi;

    // Start is called before the first frame update
    void Start()
    {
        initGameobjects();
        initVectors();
    }

    // Update is called once per frame
    void Update()
    {
        //Start game by pressing space
        if (checkGameStart())
        {
            transform.Translate(moving * pongBallSpeed * Time.deltaTime);
        
            if (pongBall.transform.position == stickPlaceHolder1.transform.position)
            {
                
                moving.x = 1;
                moving.y = 1;
                transform.Translate(moving * pongBallSpeed * Time.deltaTime);
            }
            if (pongBall.transform.position == stickPlaceHolder2.transform.position)
            {

                moving.x = -1;
                moving.y = -1;
                transform.Translate(moving * pongBallSpeed * Time.deltaTime);
            }
        }

        checkPaddleSpeedTurbo();
        checkPaddleSpeedFreeze();
        checkPaddleCloneTime();
        checkPaddleStickTime();
    }

    void checkPaddleStickTime()
    {

        if (ballWillStick && !gameIsOn && getLastPlayer() == "Paddle1")
        {
            timerStickedBall1 += Time.deltaTime;
            pongBall.transform.position = stickPlaceHolder1.transform.position;
            if(timerStickedBall1 >= 3.0f)
            {
                gameIsOn = true;
                ballWillStick = false;
                timerStickedBall1 = 0.0f;
            }
        }
        if (ballWillStick && !gameIsOn && getLastPlayer() == "Paddle2")
        {
            timerStickedBall2 += Time.deltaTime;
            pongBall.transform.position = stickPlaceHolder2.transform.position;
            if (timerStickedBall2 >= 3.0f)
            {
                gameIsOn = true;
                ballWillStick = false;
                timerStickedBall2 = 0.0f;
            }
        }
    }

    void checkPaddleCloneTime()
    {
        if(paddle1CloneBool)
        {
            timerPaddle1Clone += Time.deltaTime;
            
            if(timerPaddle1Clone >= 5.0f)
            {
                
                Destroy(GameObject.Find("Paddle1(Clone)"));
                paddle1CloneBool = false;
                timerPaddle1Clone = 0;
            }
        }
        if (paddle2CloneBool)
        {
            timerPaddle2Clone += Time.deltaTime;
            
            if (timerPaddle2Clone >= 5.0f)
            {
                
                Destroy(GameObject.Find("Paddle2(Clone)"));
                paddle2CloneBool = false;
                timerPaddle2Clone = 0;
            }
        }
    }

    void checkPaddleSpeedTurbo()
    {
        if (paddle1.GetComponent<Paddle>().paddleSpeed >= 30)
        {

            timerSpeedPaddle1 += Time.deltaTime;
            //Debug.Log("power upin timer:"+timerSpeedPaddle1);
            if (timerSpeedPaddle1 >= 5.0f)
            {
                paddle1.GetComponent<Paddle>().paddleSpeed = 15;
                if (paddle1CloneBool)
                {
                    paddle1Clone.GetComponent<Paddle>().paddleSpeed = 15;
                }
                timerSpeedPaddle1 = 0.0f;
            }
        }
        if (paddle2.GetComponent<Paddle>().paddleSpeed >= 30)
        {

            timerSpeedPaddle2 += Time.deltaTime;
            if (timerSpeedPaddle2 >= 5.0f)
            {
                paddle2.GetComponent<Paddle>().paddleSpeed = 15;
                paddle2Clone.GetComponent<Paddle>().paddleSpeed = 15;
                //if (paddle2CloneBool)
                //{
                //paddle2Clone.GetComponent<Paddle>().paddleSpeed = 15;
                //}

                timerSpeedPaddle2 = 0.0f;
            }
        }
        if (basicAi.moveTime == 0.55f)
        {
            timerSpeedPaddle1 += Time.deltaTime;
            if (timerSpeedPaddle1 >= 5.0f)
            {
                basicAi.moveTime = 0.60f;
                timerSpeedPaddle1 = 0;
            }
        }
    }
    void checkPaddleSpeedFreeze()
    {
        if (paddle1.GetComponent<Paddle>().paddleSpeed <= 0)
        {

            timerSpeedPaddle1 += Time.deltaTime;
            //Debug.Log("power upin timer:"+timerSpeedPaddle1);
            if (timerSpeedPaddle1 >= 2.0f)
            {
                paddle1.GetComponent<Paddle>().paddleSpeed = 10;
                timerSpeedPaddle1 = 0.0f;
            }
        }
        if (basicAi.moveAiBool == false)
        {
            Debug.Log("TÄÄLLÄ OLLAA:" + basicAi.moveAiBool);
            timerSpeedPaddle1 += Time.deltaTime;
            if (timerSpeedPaddle1 >= 2.0f)
            {
                basicAi.moveAiBool = true;
                timerSpeedPaddle1 = 0.0f;
            }
        }
        if (paddle2.GetComponent<Paddle>().paddleSpeed <= 0) { 

            timerSpeedPaddle2 += Time.deltaTime;
            if (timerSpeedPaddle2 >= 2.0f)
            {
                paddle2.GetComponent<Paddle>().paddleSpeed = 10;
                timerSpeedPaddle2 = 0.0f;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        string coll = collision.gameObject.name;
        switch (coll)
        {
            case "NorthWall":
                moving.y = -moving.y;
                break;
            case "SouthWall":
                moving.y = -moving.y;
                break;
            case "WestWall":
                moving.x = -moving.x;
                break;
            case "EastWall":
                moving.x = -moving.x;
                break;
            case "Paddle1":
                moving.x = -moving.x;
                setLastPlayer("Paddle1");
                if (ballWillStick)
                {
                    stickTheBall("Paddle1");
                }
                break;
            case "Paddle1(Clone)":
                moving.x = -moving.x;
                setLastPlayer("Paddle1");
                if (ballWillStick)
                {
                    stickTheBall("Paddle1");
                }
                break; 
            case "Paddle2":
                moving.x = -moving.x;
                setLastPlayer("Paddle2");
                if(ballWillStick)
                {
                    stickTheBall("Paddle2");
                }
                
                break;
            case "Paddle2(Clone)":
                moving.x = -moving.x;
                setLastPlayer("Paddle1");
                if (ballWillStick)
                {
                    stickTheBall("Paddle1");
                }
                break; 
            case "Obstacle":
                moving.x = -moving.x;
                break;
            case "Obstacle1":
                moving.x = -moving.x;
                break;
            case "Obstacle2":
                moving.x = -moving.x;
                break;
            case "Obstacle3":
                moving.x = -moving.x;
                break;
            case "Obstacle4":
                moving.x = -moving.x;
                break;
            default:
                break;



        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            if (collision.gameObject.name == "Paddle1Goal")
            {
                pongBall.transform.position = pongBallStart;
                pointsBlue += 1;
                gameIsOn = false;
                bluePointText.text = "Blue Points: " + pointsBlue.ToString();
            }
            else
            {
                pongBall.transform.position = pongBallStart;
                pointsRed += 1;
                gameIsOn = false;
                redPointText.text = "Red Points: " + pointsRed.ToString();
            }
        }
        else if (collision.gameObject.tag == "Freeze")
        {
            Destroy(collision.gameObject);
            powerUpHandlerScript.powerupOnField -= 1;
            if (getLastPlayer() == "Paddle1")
            {
                paddle2.GetComponent<Paddle>().paddleSpeed = 0;

            }
            else if (getLastPlayer() == "Paddle2")
            {
                //paddle1.GetComponent<Paddle>().paddleSpeed = 0;
                basicAi.moveAiBool = false;
            }
            else
            {
                Debug.Log("None player hit yet!");
            }
        }
        else if (collision.gameObject.tag == "BallStick")
        {
            Destroy(collision.gameObject);
            powerUpHandlerScript.powerupOnField -= 1;
            ballWillStick = true;
        }
        else if (collision.gameObject.tag == "PaddlePlus")
        {
            Destroy(collision.gameObject);
            powerUpHandlerScript.powerupOnField -= 1;
            makeNewPaddle();
            

        }
        else if (collision.gameObject.tag == "SpeedPlus")
        {
            Destroy(collision.gameObject);
            powerUpHandlerScript.powerupOnField -= 1;

            if(getLastPlayer() == "Paddle1")
            {
                paddle2.GetComponent<Paddle>().paddleSpeed = 30;
                paddle2Clone.GetComponent<Paddle>().paddleSpeed = 30;

            }
            else if(getLastPlayer() == "Paddle2")
            {
                //paddle1.GetComponent<Paddle>().paddleSpeed = 30;
                basicAi.moveTime = 0.55f;
                paddle1Clone.GetComponent<Paddle>().paddleSpeed = 30;
            }
            else
            {
                Debug.Log("None player hit yet!");
            }
        }
        else 
        { 
            Debug.Log("No colliding matches");
        };
    }
    void initGameobjects()
    {
        //paddles
        paddle1 = GameObject.Find("Paddle1");
        paddle2 = GameObject.Find("Paddle2");

        //placeholders
        stickPlaceHolder1 = GameObject.Find("Stick Placeholder 1");
        stickPlaceHolder2 = GameObject.Find("Stick Placeholder 2");
        // powerup script
        powerUpHandler = GameObject.Find("PowerUps");
        powerUpHandlerScript = powerUpHandler.GetComponent<PowerUpHandler>();

       
        //set gameobjects
        pongBall = gameObject.GetComponent<PongBall>();

        bluePointText = GameObject.Find("PlayerBluePoints").GetComponent<Text>();
        redPointText = GameObject.Find("PlayerRedPoints").GetComponent<Text>();
        //attach component

        //save start position
        pongBallStart = pongBall.transform.position;
        //Debug.Log(pongBallStart);


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
                ballWillStick = false;
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

    void setLastPlayer(string lastPlayer)
    {
        lastPlayerString = lastPlayer;
    }
    string getLastPlayer()
    {
        return lastPlayerString;
    }
    void stickTheBall(string thePaddle)
    {
        if (ballWillStick)
        {
            gameIsOn = false;
  
            if (thePaddle == "Paddle1")
            {
                pongBall.transform.position = stickPlaceHolder1.transform.position;
            }
            else
            {
                pongBall.transform.position = stickPlaceHolder2.transform.position;
            }
        }
    }
    void makeNewPaddle()
    {
        
        if(getLastPlayer() == "Paddle1")
        {
            if (!paddle1CloneBool)
            {
                paddle1CloneBool = true;
                if (paddle1.transform.position.y < -5.40)
                {
                    Instantiate(paddle1Clone, new Vector3(paddle1.transform.position.x, paddle1.transform.position.y + 6.20f, -9), Quaternion.identity);
                }
                else
                {
                    Instantiate(paddle1Clone, new Vector3(paddle1.transform.position.x, paddle1.transform.position.y - 6.20f, -9), Quaternion.identity);
                }
            }
        }
        if (getLastPlayer() == "Paddle2")
        {
            if (!paddle2CloneBool)
            {
                paddle2CloneBool = true;
                if (paddle1.transform.position.y < -5.40)
                {
                    Instantiate(paddle1Clone, new Vector3(paddle1.transform.position.x, paddle1.transform.position.y + 6.20f, -9), Quaternion.identity);
                }
                else
                {
                    Instantiate(paddle2Clone, new Vector3(paddle2.transform.position.x, paddle2.transform.position.y - 6.20f, -9), Quaternion.identity);
                }
            }
        }


    }
}
