using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    public float paddleSpeed = 15.0f;

    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "Paddle1" || gameObject.name == "Paddle1(Clone)")
        {
            if (Input.GetKey("w"))
            {
                transform.Translate(0, paddleSpeed * Time.deltaTime, 0);
            }
            if (Input.GetKey("s"))
            {
                transform.Translate(0, -paddleSpeed * Time.deltaTime, 0);
            }
        }
        if(gameObject.name == "Paddle2" || gameObject.name == "Paddle2(Clone)") 
        {
            if (Input.GetKey("up"))
            {
                transform.Translate(0, paddleSpeed * Time.deltaTime, 0);
            }
            if (Input.GetKey("down"))
            {
                transform.Translate(0, -paddleSpeed * Time.deltaTime, 0);
            }
        }
        if (gameObject.name == "BreakoutPaddle" || gameObject.name == "BreakoutPaddle(Clone)")
        {
            if (Input.GetKey("d")|| Input.GetKey("right"))
            {
                transform.Translate(paddleSpeed * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey("a")|| Input.GetKey("left"))
            {
                transform.Translate(-paddleSpeed * Time.deltaTime, 0, 0);
            }
        }
    }
}
