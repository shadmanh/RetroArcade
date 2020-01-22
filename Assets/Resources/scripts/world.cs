using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class World
{
    public static int blockSize = 1;
    public static int freeSpaceLeft = 178;

    public static int[][] grid = new int[][] { 
            new int[] { 5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5 },
            new int[] { 5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5 },
            new int[] { 5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5 },
            new int[] { 5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5 },
            new int[] { 5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5 },
            new int[] { 5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,5 },
            new int[] { 5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5 },
            new int[] { 5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5 },
            new int[] { 5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5 },
            new int[] { 5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5 },
            new int[] { 5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5 },
            new int[] { 5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5 }
        };

    public static int FREE = 0;
    public static int FOOD = 1;
    public static int TAIL = 2;
    public static int BODY = 3;
    public static int HEAD = 4;
    public static int WALL = 4;

    public static void Reset()
    {
        grid = new int[][] { 
            new int[] { 5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5 },
            new int[] { 5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5 },
            new int[] { 5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5 },
            new int[] { 5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5 },
            new int[] { 5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5 },
            new int[] { 5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,5 },
            new int[] { 5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5 },
            new int[] { 5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5 },
            new int[] { 5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5 },
            new int[] { 5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5 },
            new int[] { 5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5 },
            new int[] { 5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5 }
        };
    }

    //Generates food on a random free spot
    public static void generateFood(headController head) {
        int counter = 0;
        int rightSpot = Random.Range(0, freeSpaceLeft);
        for (int y=1; y < 10; ++y)
        {
            for (int x=1; x < 18; ++x)
            {
                if (grid[y][x] == FREE)
                {
                    if (counter == rightSpot)
                    {
                        grid[y][x] = FOOD;
                        freeSpaceLeft--;
                        head.makeFood(x, y);
                        return;
                    }
                    counter++;
                }
            }
        }
    }
}
