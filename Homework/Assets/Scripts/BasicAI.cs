using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI : MonoBehaviour
{

    GameObject pongBall;
    float moveTimer;
    public float moveTime;
    public bool moveAiBool;


    // Start is called before the first frame update
    void Start()
    {
        pongBall = GameObject.Find("PongBall");
        moveTime = 0.60f;
        moveAiBool = true;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(moveAiBool);
        if (moveAiBool)
        {
            moveTimer += Time.deltaTime;
            if (moveTimer >= moveTime)
            {
                transform.position = new Vector2(-28, pongBall.transform.position.y);
                moveTimer = 0.0f;
            }
        }
    }
}
