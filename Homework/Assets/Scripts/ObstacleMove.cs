using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;



public class ObstacleMove : MonoBehaviour
{

    //Speed of the ball
    public float obstacleSpeed = 2.0f;
   
    //Moving vector
    Vector2 moving;
 

    // Start is called before the first frame update
    void Start()
    {
        //Set moving direction: x:1, y:1 and add the coordinates to current position with normalized
        moving = transform.up.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moving * obstacleSpeed * Time.deltaTime);
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
            default:
                break;
        }

    }
    }
