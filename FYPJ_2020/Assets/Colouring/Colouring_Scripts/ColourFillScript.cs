 using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;
 
    public static class TextureExtension
    {
    public struct Point
    {
        public short x;
        public short y;
        public Point(short aX, short aY) { x = aX; y = aY; }
        public Point(int aX, int aY) : this((short)aX, (short)aY) { }
    }
    public static void FloodFillArea(this Texture2D aTex, int aX, int aY, Color aFillColor)
    {
        Debug.Log("aTex = " + aTex);
        int w = aTex.width;
        int h = aTex.height;
        Color[] colors = aTex.GetPixels();
        Color refCol = colors[aX + aY * w];
        Queue<Point> nodes = new Queue<Point>();
        nodes.Enqueue(new Point(aX, aY));
        while (nodes.Count > 0)
        {
            Point current = nodes.Dequeue();
            for (int i = current.x; i < w; i++)
            {
                Color C = colors[i + current.y * w];
                if (C != refCol || C == aFillColor)
                    break;
                colors[i + current.y * w] = aFillColor;
                if (current.y + 1 < h)
                {
                    C = colors[i + current.y * w + w];
                    if (C == refCol && C != aFillColor)
                        nodes.Enqueue(new Point(i, current.y + 1));
                }
                if (current.y - 1 >= 0)
                {
                    C = colors[i + current.y * w - w];
                    if (C == refCol && C != aFillColor)
                        nodes.Enqueue(new Point(i, current.y - 1));
                }
            }
            for (int i = current.x - 1; i >= 0; i--)
            {
                Color C = colors[i + current.y * w];
                if (C != refCol || C == aFillColor)
                    break;
                colors[i + current.y * w] = aFillColor;
                if (current.y + 1 < h)
                {
                    C = colors[i + current.y * w + w];
                    if (C == refCol && C != aFillColor)
                        nodes.Enqueue(new Point(i, current.y + 1));
                }
                if (current.y - 1 >= 0)
                {
                    C = colors[i + current.y * w - w];
                    if (C == refCol && C != aFillColor)
                        nodes.Enqueue(new Point(i, current.y - 1));
                }
            }
        }
        aTex.SetPixels(colors);
    }

    public static void FloodFillBorder(this Texture2D aTex, int aX, int aY, Color aFillColor, Color aBorderColor)
    {
        Debug.Log("aTex = " + aTex);
        int w = aTex.width;
        int h = aTex.height;
        Color[] colors = aTex.GetPixels();
        byte[] checkedPixels = new byte[colors.Length];
        Color refCol = aBorderColor;
        Debug.Log("old aX = " + aX);
        Debug.Log("old aY = " + aY);
        Queue<Point> nodes = new Queue<Point>();
        Debug.Log("new aX = " + aX);
        Debug.Log("new aY = " + aY);
        nodes.Enqueue(new Point(aX, aY));
        while (nodes.Count > 0)
        {
            Point current = nodes.Dequeue();

            for (int i = current.x; i < w; i++)
            {
                if (checkedPixels[i + current.y * w] > 0 || colors[i + current.y * w] == refCol)
                    break;
                colors[i + current.y * w] = aFillColor;
                checkedPixels[i + current.y * w] = 1;
                if (current.y + 1 < h)
                {
                    if (checkedPixels[i + current.y * w + w] == 0 && colors[i + current.y * w + w] != refCol)
                        nodes.Enqueue(new Point(i, current.y + 1));
                }
                if (current.y - 1 >= 0)
                {
                    if (checkedPixels[i + current.y * w - w] == 0 && colors[i + current.y * w - w] != refCol)
                        nodes.Enqueue(new Point(i, current.y - 1));
                }
            }
            for (int i = current.x - 1; i >= 0; i--)
            {
                if (checkedPixels[i + current.y * w] > 0 || colors[i + current.y * w] == refCol)
                    break;
                colors[i + current.y * w] = aFillColor;
                checkedPixels[i + current.y * w] = 1;
                if (current.y + 1 < h)
                {
                    if (checkedPixels[i + current.y * w + w] == 0 && colors[i + current.y * w + w] != refCol)
                        nodes.Enqueue(new Point(i, current.y + 1));
                }
                if (current.y - 1 >= 0)
                {
                    if (checkedPixels[i + current.y * w - w] == 0 && colors[i + current.y * w - w] != refCol)
                        nodes.Enqueue(new Point(i, current.y - 1));
                }
            }
        }
        aTex.SetPixels(colors);
    }
    /*
    /// <summary>
    /// This method will flood fill a texture with a color begining from given coordinates
    /// </summary>
    /// <param name="texture">The texture to apply the flood fill to</param>
    /// <param name="x">The starting pixel's x coordinate</param>
    /// <param name="y">The starting pixel's y coordinate</param>
    /// <param name="fillColor">The color to apply</param>
    /// <param name="borderColor">The border color of the flood fill</param>
    /// <param name="useBorder">Do we fill till borders or not</param>
    public static void FloodFill(this Texture2D texture, int x, int y, Color fillColor, Color borderColor, bool useBorder)
    {
        //Out of bounds
        if (x < 0 || x >= texture.width || y < 0 || y >= texture.height)
        {
            return;
        }

        //Set up shared info
        Color[] colors = texture.GetPixels();
        //Reference color changes depending if we want to use a border color or not
        Color refColor = useBorder ? borderColor : colors[x + y * texture.width];

        //Call the first iteration
        FloodFillRecursive(colors, texture.width, x, y, refColor, fillColor, useBorder);

        //Apply to texture
        texture.SetPixels(colors);
    }

    /// <summary>
    /// This methos will flood fill a table of colors recursively
    /// </summary>
    /// <param name="colors">The table containing the colors</param>
    /// <param name="w">The width of the texture</param>
    /// <param name="x">The x coordinate of the current pixel</param>
    /// <param name="y">The y coordinate of the current pixel</param>
    /// <param name="refColor">The reference color</param>
    /// <param name="fill">The color to set the pixel at</param>
    /// <param name="useBorder">Is refColor a border color or not</param>
    private static void FloodFillRecursive(Color[] colors, int w, int x, int y, Color refColor, Color fill, bool useBorder)
    {
        int index = x + y * w;

        //Out of range
        if (index < 0 || index > colors.Length)
        {
            return;
        }

        //Get current pixel color
        Color c = colors[index];

        //Nothing to do
        if (c == fill || (useBorder ? c == refColor : c != refColor))
        {
            return;
        }

        //Apply color
        colors[index] = fill;

        //Apply same to neighbour pixels
        //Left pixel
        FloodFillRecursive(colors, w, x - 1, y, refColor, fill, useBorder);
        //Right pixel
        FloodFillRecursive(colors, w, x + 1, y, refColor, fill, useBorder);
        //Down pixel
        FloodFillRecursive(colors, w, x, y - 1, refColor, fill, useBorder);
        //Up pixel
        FloodFillRecursive(colors, w, x, y + 1, refColor, fill, useBorder);

    }*/
}