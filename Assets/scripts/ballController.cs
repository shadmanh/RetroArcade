using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float speed = 4f;

    private float vx;
    private float vy;

    private int lastWinner;

    public const string BOUNDARY ="boundary";
    public const string PLAYER_LEFT = "playerLeft";
    public const string PLAYER_RIGHT = "playerRight";
    public const string SCORE_ZONE = "scoreZone";

    // Start is called before the first frame update
    void Start()
    {
        lastWinner = Random.Range(1, 2);
        reset();
    }

    // Update is called once per frame
    void Update()
    {
       transform.Translate(new Vector3(vx, vy, 0) * Time.deltaTime);

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void FixedUpdate()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        string colTag = col.gameObject.tag;

        //All boundaries flip the y velocity of the ball
        if (colTag.Equals(BOUNDARY))
        {
            vy *= -1;
        }

        //Paddle collsions fli the x velocity of the ball
        else if (colTag.Equals(PLAYER_LEFT) || colTag.Equals(PLAYER_RIGHT))
        {
            vx *= -1;
        }

        //Reaching either end of the screen results in the ball being reset
        else if (colTag.Equals(SCORE_ZONE))
        {
            reset();
        }
    }

    //Resets the ball in the center and gives it a random angle to go at next
    void reset()
    {
        transform.position = new Vector3(0, 0, 0);
        float angle = Random.Range(0f, Mathf.PI/3);
        vx = Mathf.Cos(angle) * speed;
        vx = lastWinner == 2 ? vx*(-1) : vx;
        vy = Mathf.Sin(angle) * speed;
        vy = Random.Range(0,1) == 1 ? vy*(-1) : vy;
    }

    //Updates who won the last round
    public void updateLastWinner(int playerNum)
    {
        lastWinner = playerNum;
    }
}
