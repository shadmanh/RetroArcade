using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    private float bounds = 4.5f;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, Mathf.Min(Mathf.Max(-1*bounds, mouseVector.y), bounds), transform.position.z);
    }

    public void updateScore()
    {
        score++;
        Debug.Log(score);
    }
}
