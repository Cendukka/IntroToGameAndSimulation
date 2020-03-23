using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHandler : MonoBehaviour
{
    //the powerups
    public GameObject freeze;
    public GameObject capture;
    public GameObject paddleDoubler;
    public GameObject turbo;
    //array that contains powerups
    string[] powerupArray;
    
    //walls
    GameObject northWall;
    GameObject southWall;
    GameObject paddleLeft;
    GameObject paddleRight;
    //placeholder for selected powerup
    GameObject selectedPowerUp;

    Vector2 paddleLeftpos;
    Vector2 paddleRightpos;
    Vector2 northWallPos;
    Vector2 southWallPos;

    float powerupPopDelay;
    public int powerupOnField;


    // Start is called before the first frame update
    void Start()
    {
        
        northWall = GameObject.Find("NorthWall");
        southWall = GameObject.Find("SouthWall");
        paddleLeft = GameObject.Find("Paddle1");
        paddleRight = GameObject.Find("Paddle2");

        paddleLeftpos = paddleLeft.transform.position;
        paddleRightpos = paddleRight.transform.position;
        northWallPos = northWall.transform.position;
        southWallPos = southWall.transform.position;

        powerupOnField = 0;

        powerupArray = new string[] { "Freeze", "Capture", "PaddleDoubler", "Turbo" };


    }

    // Update is called once per frame
    void Update()
    {
        //time running
        powerupPopDelay += Time.deltaTime;
        //Debug.Log("Powerup pop delay: " + powerupPopDelay);

        //when 5 sec has passed, pop new powerup
        if(powerupPopDelay >= 5.0f)
        {
            //set the timer 0
            powerupPopDelay = 0.0f;
            //check, if there is space for the powerup
            if (powerupOnField < 2)
            {
                //select random powerup string from the array
                string powerUpselected = powerupArray[Random.Range(0, powerupArray.Length)];
                //Debug.Log(powerUpselected);
                switch (powerUpselected)
                {
                    case "Freeze":
                        selectedPowerUp = freeze;
                        if (selectedPowerUp == null)
                        {
                            Debug.Log("IT'S NULL!!!");
                        }
                        else
                        {
                            //Debug.Log(selectedPowerUp);
                            //Position it in the scene
                            Instantiate(selectedPowerUp, new Vector3(Random.Range(paddleLeftpos.x, paddleRightpos.x), Random.Range(northWallPos.y, southWallPos.y), -9), Quaternion.identity);
                            powerupOnField += 1;
                        }
                        break;
                    case "Capture":
                        selectedPowerUp = capture;
                        if (selectedPowerUp == null)
                        {
                            Debug.Log("IT'S NULL!!!");
                        }
                        else
                        {
                            //Position it in the scene
                            Instantiate(selectedPowerUp, new Vector3(Random.Range(paddleLeftpos.x, paddleRightpos.x), Random.Range(northWallPos.y, southWallPos.y), -9), Quaternion.identity);
                            powerupOnField += 1;
                        }
                        break;
                    //case "PaddleDoubler":
                    //    selectedPowerUp = paddleDoubler;
                    //    if (selectedPowerUp == null)
                    //    {
                    //        Debug.Log("IT'S NULL!!!");
                    //    }
                    //    else
                    //    {
                    //        //Position it in the scene
                    //        Instantiate(selectedPowerUp, new Vector3(Random.Range(paddleLeftpos.x, paddleRightpos.x), Random.Range(northWallPos.y, southWallPos.y), -9), Quaternion.identity);
                    //        powerupOnField += 1;
                    //    }
                    //    break;
                    case "Turbo":
                        selectedPowerUp = turbo;
                        if (selectedPowerUp == null)
                        {
                            Debug.Log("IT'S NULL!!!");
                        }
                        else
                        {
                            //Position it in the scene
                            Instantiate(selectedPowerUp, new Vector3(Random.Range(paddleLeftpos.x, paddleRightpos.x), Random.Range(northWallPos.y, southWallPos.y), -9), Quaternion.identity);
                            powerupOnField += 1;
                        }
                        break;
                    default:
                        Debug.Log("No powerups matched");
                        break;
                }

            }
            else
            {
                Debug.Log("Too many powerups!");
            }
        }
    }
}

