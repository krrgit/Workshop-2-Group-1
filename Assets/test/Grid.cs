using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Grid
{

    private int width;
    private int height;
    private int[,] gridArray;//multidimensional array with 2 dimensions
    
    //area of grid
    public Grid(int width, int height)
    {
        this.width = width;
        this.height = height;

        //array
        gridArray = new int[width, height];
        
        //x axis
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            //y axis
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                
            }
        }
    }

    public static TextMesh CreateWorldTest(string text, Transform parent = null,
        Vector3 localPosition = default(Vector3),
        int fontSize = 40, Color color)
    {
        if (color == null)
        {
            color = Color.white;
        }
        return CreateWorldTest(parent, text, localPosition, fontSize, (Color)color, TextAnchor, TextAlignment, sortingOrder)
    }
}
