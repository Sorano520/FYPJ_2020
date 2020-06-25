using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceGenerator : MonoBehaviour
{

    [SerializeField] private JigsawPieceLogic piece;
    [SerializeField] private int pieceSize = 1;
    [SerializeField] private int col = 4;
    [SerializeField] private int row = 4;
    
    private Mesh[,] pieceMeshArray;

    private bool[,] topConnector;
    private bool[,] rightConnector;

    private void Start()
    {
        // Get the image to use for the puzzle pieces
        GameObject img = GameObject.FindGameObjectWithTag("Image");
        if (img == null) Debug.LogError("JigsawPieceLogic - Image is missing");
        piece.GetComponent<Renderer>().sharedMaterial.mainTexture = img.GetComponent<SpriteRenderer>().sprite.texture;
        img.SetActive(false);

        InventoryLogic.instance.Inventory = new SerializedDictionary();

        // Get the distance between each puzzle piece (distance between each piece on the image)
        float offset = pieceSize * 0.335f;
        Vector3 start = transform.position;
        start.x = -col * 0.5f;
        start.y = -row * 0.5f + offset;
        float startX = start.x;

        float uvWidth = 1.0f / col;
        float uvHeight = 1.0f / row;

        pieceMeshArray = new Mesh[row, col];
        topConnector = new bool[row, col];
        rightConnector = new bool[row, col];

        for (int j = 0; j < col; ++j)
        {
            for (int i = 0; i < row; ++i)
            {
                // Generates a piece
                JigsawPieceLogic currPiece = Instantiate(piece);
                // Moves it to the correct position
                currPiece.transform.parent = null;
                currPiece.transform.position = start;
                Mesh mesh = currPiece.GetComponent<MeshFilter>().mesh;
                currPiece.transform.localScale = new Vector3(1, 1, 1) * pieceSize;
                currPiece.transform.parent = transform;
                pieceMeshArray[i, j] = mesh;
                string name = i + "," + j;
                currPiece.CreatePiece(name);

                // Sets the position of the image on the puzzle properly (uses material)
                //Store the original UVs to be used by the masks.
                Vector2[] uvs = mesh.uv;
                mesh.uv2 = uvs;
                //Set the UVs to be encompass neighboring pieces which will be masked out in the shader.
                uvs[0] = new Vector2((i - 1) * uvWidth, (j - 1) * uvHeight);
                uvs[1] = new Vector2((i + 2) * uvWidth, (j - 1) * uvHeight);
                uvs[2] = new Vector2((i - 1) * uvWidth, (j + 2) * uvHeight);
                uvs[3] = new Vector2((i + 2) * uvWidth, (j + 2) * uvHeight);

                mesh.uv = uvs;

                start.x += offset;
            }
            start.y += offset;
            start.x = startX;
        }

        //The logic below is used to pass data through a mesh's vertex color array to use later in the shader.
        //At the moment it randomly generates a valid jigsaw pattern.
        for (int j = 0; j < col; j++)
        {
            for (int i = 0; i < row; i++)
            {
                Mesh mesh = pieceMeshArray[i, j];

                // 0 = connector
                // 1 = inverted connector
                // 2 = nil (edge piece)
                int leftMask = 2;
                // if the piece is not the first in the row
                // check if the right side of the previous piece was a connector or an inverted connector
                if (i > 0) leftMask = rightConnector[i - 1, j] ? 1 : 0;

                int rightMask = 2;
                // if the piece is not the last in the row
                if (i < row - 1)
                {
                    // randomly sets it as a connector or an inverted connector
                    rightMask = Random.Range(0, 2) == 0 ? 0 : 1;
                    if (rightMask == 0) rightConnector[i, j] = true;
                }

                int topMask = 2;
                if (j < col - 1)
                {
                    topMask = Random.Range(0, 2) == 0 ? 0 : 1;
                    if (topMask == 0) topConnector[i, j] = true;
                }

                int bottomMask = 2;
                if (j > 0) bottomMask = topConnector[i, j - 1] ? 1 : 0;

                Color combinationMask = new Color(leftMask, rightMask, topMask, bottomMask);
                mesh.SetColors(new List<Color>() { combinationMask, combinationMask, combinationMask, combinationMask });
            }
        }
        InventoryLogic.instance.SortInventory();
    }
}