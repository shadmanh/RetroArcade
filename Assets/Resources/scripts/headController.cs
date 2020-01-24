using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class headController : MonoBehaviour
{
    private int vx = 1;
    private int vy = 0;
    private int oldVx;
    private int oldVy;

    private Vector2 pos;
    private int posx;
    private int posy;

    private GameObject nextFood;

    public enum dirs {
        LEFT = 0,
        RIGHT = 1,
        UP = 2,
        DOWN = 3,
    };

    private dirs dir = dirs.RIGHT;

    private bool oneMoveDelayDone = false;

    private bool digesting = false;
    private bool finishedDigesting = false;

    [SerializeField]
    private bodyController next;

    // Start is called before the first frame update
    void Start()
    {
        World.Reset();
        pos = transform.position;
        posx = (int)transform.position.x / World.blockSize;
        posy = -1 * (int)transform.position.y / World.blockSize;

        oldVx = vx;
        oldVy = vy;

        World.grid[posy][posx] = World.HEAD;

        nextFood = (GameObject)Instantiate(Resources.Load("prefabs/food"), new Vector3(15, -5, 0), Quaternion.identity);

        InvokeRepeating("Move", 0f, 0.5f);
    }

    // Update is called once per frame. Mainly input is checked here
    void Update()
    {
        posx = (int)transform.position.x / World.blockSize;
        posy = -1 * (int)transform.position.y / World.blockSize;

        if (vx == 0)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                dir = dirs.LEFT;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                dir = dirs.RIGHT;
            }
        }
        else if (vy == 0)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                dir = dirs.UP;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                dir = dirs.DOWN;
            }
        }

        if (Input.GetKey("escape"))
        {
            SceneManager.LoadScene("menu");
        }
    }

    //The head moves if there is nothing blocking its current direction in the world.
    virtual public void Move()
    {
        PrintGrid();
        if (oneMoveDelayDone)
        {
            oldVx = vx;
            oldVy = vy;
        }

        if (dir == dirs.LEFT)
        {
            vx = -1;
            vy = 0;
        }
        else if (dir == dirs.RIGHT)
        {
            vx = 1;
            vy = 0;
        }
        else if (dir == dirs.UP)
        {
            vx = 0;
            vy = 1;
        }
        else if (dir == dirs.DOWN)
        {
            vx = 0;
            vy = -1;
        }

        if (World.grid[posy - vy][posx + vx] <= 2)
        {
            if (digesting)
            {
                bodyController oldNext = next;
                next = ((GameObject) Instantiate(Resources.Load("prefabs/body"), new Vector3(posx*World.blockSize, -1*posy*World.blockSize, 0), Quaternion.identity)).GetComponent<bodyController>();
                next.setV(oldVx, oldVy);
                if (oldNext != null)
                {
                    next.setNext(oldNext);
                }
                digesting = false;
                finishedDigesting = true;
            }

            if (World.grid[posy - vy][posx + vx] == World.FOOD)
            {
                Destroy(nextFood);
                World.generateFood(this);
                digesting = true;
            }

            if (!finishedDigesting)
            {
                World.grid[posy][posx] = World.FREE;
            }
            posy = posy - vy;
            posx = posx + vx;
            World.grid[posy][posx] = World.HEAD;
            transform.position = new Vector3(posx * World.blockSize, -1 * posy * World.blockSize, 0);

            if (!finishedDigesting && next != null)
            {
                next.Move(oldVx, oldVy);
            }

            oneMoveDelayDone = true;
            finishedDigesting = false;
        } 
        else
        {
            Debug.Log("Game Over!");
            World.Reset();
            SceneManager.LoadScene("snake");
        }
    }

    public void makeFood(int x, int y)
    {
        nextFood = (GameObject) Instantiate(Resources.Load("prefabs/food"), new Vector3(x, -1 * y, 0), Quaternion.identity);
    }

    public void PrintGrid()
    {
        string line = "";
        for (int y = 0; y < World.grid.Length; ++y)
        {
            for (int x = 0; x < World.grid[y].Length; ++x)
            {
                line += World.grid[y][x] + " ";
            }
            line += "\n";
        }
        Debug.Log(line);
    }
}

