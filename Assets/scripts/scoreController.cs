using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scoreController : MonoBehaviour
{

    private int score = 0;

    [SerializeField]
    private int playerNumber;

    [SerializeField]
    private ballController ball;

    [SerializeField]
    private Text scoreText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        score++;
        ball.updateLastWinner(playerNumber);
        Debug.Log("Player " + playerNumber + "'s score is now: " + score);
        
        if (score == 10)
        {
            Debug.Log("Player " + playerNumber + "is the winner! The game will now reset.");
            SceneManager.LoadScene("pong");
        }
        else
        {
            scoreText.text = ""+score;
        }
    }
}
