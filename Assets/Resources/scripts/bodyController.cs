using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyController : MonoBehaviour
{

    SpriteRenderer sr;

    private int vx = 1;
    private int vy = 0;

    private Vector2 pos;
    private int posx;
    private int posy;

    [SerializeField]
    private bodyController next = null;

    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();

        if (next == null)
        {
            sr.color = new Color(0f, 233f/255f, 1f);
        }

        pos = transform.position;
        posx = (int)transform.position.x / World.blockSize;
        posy = -1 * (int)transform.position.y / World.blockSize;

        World.grid[posy][posx] = World.BODY;
    }

    // Update is called once per frame. Mainly input is checked here
    void Update()
    {
    }

    //The head moves if there is nothing blocking its current direction in the world.
    public void Move(int vx, int vy)
    {

        World.grid[posy][posx] = World.FREE;
        posy = posy - vy;
        posx = posx + vx;
        World.grid[posy][posx] = World.BODY;
        transform.position = new Vector3(posx * World.blockSize, -1 * posy * World.blockSize, 0);

        if (next != null)
        {
            next.Move(this.vx, this.vy);
        }

        this.vx = vx;
        this.vy = vy;
    }

    public void setNext(bodyController bc)
    {
        next = bc;
    }

    public void setV(int x, int y)
    {
        vx = x;
        vy = y;
    }
}
