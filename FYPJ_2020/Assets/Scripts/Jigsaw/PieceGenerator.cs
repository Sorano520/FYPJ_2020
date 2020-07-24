//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PieceGenerator : MonoBehaviour
//{
//    [SerializeField] private JigsawPieceLogic piece = null;
//    [SerializeField] private int pieceSize = 1;
//    [SerializeField] private int col = 4;
//    [SerializeField] private int row = 4;

//    private Mesh[,] pieceMeshes;

//    private bool[,] colConnector;
//    private bool[,] rowConnector;

//    #region Getters & Setters
//    public int PieceSize
//    {
//        get { return pieceSize; }
//        set { pieceSize = value; }
//    }
//    #endregion

//    private void Start()
//    {
//        // Get the image to use for the puzzle pieces
//        GameObject img = GameObject.FindGameObjectWithTag("Image");
//        if (img == null) Debug.LogError("JigsawPieceLogic - Image is missing");
//        piece.GetComponent<Renderer>().sharedMaterial.mainTexture = img.GetComponent<SpriteRenderer>().sprite.texture;
//        img.SetActive(false);

//        InventoryLogic.instance.Inventory = new SerializedDictionary();

//        // Get the distance between each puzzle piece (distance between each piece on the image)
//        float offset = pieceSize * 0.335f;
//        Vector3 start = transform.position;
//        start.x = -col * 0.5f;
//        start.y = -row * 0.5f + offset;
//        float startX = start.x;

//        float uvWidth = 1.0f / col;
//        float uvHeight = 1.0f / row;

//        pieceMeshes = new Mesh[row, col];
//        colConnector = new bool[row, col];
//        rowConnector = new bool[row, col];

//        // Setup the min/max camera distance
//        MouseLogic.instance.MIN = new Vector2(-offset * row, -offset * col);
//        MouseLogic.instance.MAX = new Vector2(offset * row, offset * col);
//        Vector2 size = MouseLogic.instance.MAX - MouseLogic.instance.MIN;
//        float meanScale = (size.x + size.y) * 0.5f;
//        MouseLogic.instance.MINMAXSize = new Vector2(meanScale, meanScale * 1.5f);

//        for (int j = 0; j < col; ++j)
//        {
//            for (int i = 0; i < row; ++i)
//            {
//                // Generates a piece
//                JigsawPieceLogic currPiece = Instantiate(piece);
//                // Moves it to the correct position
//                currPiece.transform.parent = null;
//                currPiece.transform.position = start;
//                Mesh mesh = currPiece.GetComponent<MeshFilter>().mesh;
//                currPiece.transform.localScale = new Vector3(1, 1, 1) * pieceSize;
//                currPiece.transform.parent = transform;
//                pieceMeshes[i, j] = mesh;
//                string name = i + "," + j;
//                currPiece.CreatePiece(name);

//                // Sets the position of the image on the puzzle properly (uses material)
//                //Store the original UVs to be used by the masks.
//                Vector2[] meshUV = mesh.uv;
//                mesh.uv2 = meshUV;
//                //Set the UVs to be encompass neighboring pieces which will be masked out in the shader.
//                meshUV[0] = new Vector2((i - 1) * uvWidth, (j - 1) * uvHeight);
//                meshUV[1] = new Vector2((i + 2) * uvWidth, (j - 1) * uvHeight);
//                meshUV[2] = new Vector2((i - 1) * uvWidth, (j + 2) * uvHeight);
//                meshUV[3] = new Vector2((i + 2) * uvWidth, (j + 2) * uvHeight);

//                mesh.uv = meshUV;

//                start.x += offset;
//            }
//            start.y += offset;
//            start.x = startX;
//        }

//        //The logic below is used to pass data through a mesh's vertex color array to use later in the shader.
//        //At the moment it randomly generates a valid jigsaw pattern.
//        for (int j = 0; j < col; j++)
//        {
//            for (int i = 0; i < row; i++)
//            {
//                Mesh mesh = pieceMeshes[i, j];

//                // 0 = connector
//                // 1 = inverted connector
//                // 2 = nil (edge piece)
//                int left = 2;
//                // if the piece is not the first in the row
//                // check if the right side of the previous piece was a connector or an inverted connector
//                if (i > 0) left = rowConnector[i - 1, j] ? 1 : 0;

//                int right = 2;
//                // if the piece is not the last in the row
//                if (i < row - 1)
//                {
//                    // randomly sets it as a connector or an inverted connector
//                    right = Random.Range(0, 2) == 0 ? 0 : 1;
//                    if (right == 0) rowConnector[i, j] = true;
//                }

//                int top = 2;
//                if (j < col - 1)
//                {
//                    top = Random.Range(0, 2) == 0 ? 0 : 1;
//                    if (top == 0) colConnector[i, j] = true;
//                }

//                int bottom = 2;
//                if (j > 0) bottom = colConnector[i, j - 1] ? 1 : 0;

//                Color combinedMask = new Color(left, right, top, bottom);
//                mesh.SetColors(new List<Color>() { combinedMask, combinedMask, combinedMask, combinedMask });
//            }
//        }
//        InventoryLogic.instance.SortInventory();
//        float newScale = (MouseLogic.instance.MINMAXSize.y + MouseLogic.instance.MINMAXSize.x) * 0.5f;
//        transform.localScale = new Vector3(newScale, newScale, newScale);
//    }
//}






using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceGenerator : MonoBehaviour
{
    GameObject image;
    [SerializeField] private JigsawPieceLogic piece = null;
    [SerializeField] private int pieceSize = 1;
    [SerializeField] private int col = 4;
    [SerializeField] private int row = 4;
    public GameObject slotHolder, pieceHolder;

    private Mesh[,] pieceMeshes;

    private bool[,] colConnector;
    private bool[,] rowConnector;

    #region Getters & Setters
    public int PieceSize
    {
        get { return pieceSize; }
        set { pieceSize = value; }
    }
    #endregion
    
    private void Start()
    {
        //pieceSize = GameManager.instance.JigsawDifficulties[GameManager.instance.chosenDifficulty];

        // Get the image to use for the puzzle pieces
        image = GameObject.FindGameObjectWithTag("Image");
        if (image == null) Debug.LogError("JigsawPieceLogic - Image is missing");
        piece.GetComponent<Renderer>().sharedMaterial.mainTexture = image.GetComponent<Image>().sprite.texture;

        InventoryLogic.instance.Inventory = new SerializedDictionary();
        slotHolder = new GameObject("Slot Holder");
        pieceHolder = new GameObject("Piece Holder");
        slotHolder.transform.parent = pieceHolder.transform.parent = transform;

        // Get the distance between each puzzle piece (distance between each piece on the image)
        float offset = (float)pieceSize / 3;
        Vector3 start = transform.position;
        start.x = (-row + 1) * 0.5f * offset;
        start.y = (-col + 1) * 0.5f * offset;
        float startX = start.x;

        float uvWidth = 1.0f / row;
        float uvHeight = 1.0f / col;

        pieceMeshes = new Mesh[row, col];
        colConnector = new bool[row, col];
        rowConnector = new bool[row, col];

        // Setup the min/max camera distance
        MouseLogic.instance.MinMax = new Vector2(-0.4f + 0.3f * (pieceSize - 1), -0.4f + 0.3f * (pieceSize - 1));
        if (col > row) MouseLogic.instance.MinMaxSize = new Vector2(0.7f * col, 0.2f + 1.2f * col);
        else MouseLogic.instance.MinMaxSize = new Vector2(0.7f * row, 0.2f + 1.2f * row);

        // Set the spaces the pieces can spawn in
        MouseLogic.instance.SpawnZone = new Vector3(3.2f + (0.85f * (row - 2)), 3.25f + (2.15f * (row - 2)), start.y + offset * (col - 1));

        // Set scale of board
        Camera.main.orthographicSize = MouseLogic.instance.MinMaxSize.y;
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, 0.05f + (0.36f * col), Camera.main.transform.position.z);

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
                currPiece.transform.parent = pieceHolder.transform;
                pieceMeshes[i, j] = mesh;
                string name = i + "," + j;
                currPiece.CreatePiece(name);

                // Sets the position of the image on the puzzle properly (uses material)
                //Store the original UVs to be used by the masks.
                Vector2[] meshUV = mesh.uv;
                mesh.uv2 = meshUV;
                //Set the UVs to be encompass neighboring pieces which will be masked out in the shader.
                meshUV[0] = new Vector2((i - 1) * uvWidth, (j - 1) * uvHeight);
                meshUV[1] = new Vector2((i + 2) * uvWidth, (j - 1) * uvHeight);
                meshUV[2] = new Vector2((i - 1) * uvWidth, (j + 2) * uvHeight);
                meshUV[3] = new Vector2((i + 2) * uvWidth, (j + 2) * uvHeight);

                mesh.uv = meshUV;

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
                Mesh mesh = pieceMeshes[i, j];

                // 0 = connector
                // 1 = inverted connector
                // 2 = nil (edge piece)
                int left = 2;
                // if the piece is not the first in the row
                // check if the right side of the previous piece was a connector or an inverted connector
                if (i > 0) left = rowConnector[i - 1, j] ? 1 : 0;

                int right = 2;
                // if the piece is not the last in the row
                if (i < row - 1)
                {
                    // randomly sets it as a connector or an inverted connector
                    right = Random.Range(0, 2) == 0 ? 0 : 1;
                    if (right == 0) rowConnector[i, j] = true;
                }

                int top = 2;
                if (j < col - 1)
                {
                    top = Random.Range(0, 2) == 0 ? 0 : 1;
                    if (top == 0) colConnector[i, j] = true;
                }

                int bottom = 2;
                if (j > 0) bottom = colConnector[i, j - 1] ? 1 : 0;

                Color combinedMask = new Color(left, right, top, bottom);
                mesh.SetColors(new List<Color>() { combinedMask, combinedMask, combinedMask, combinedMask });
            }
        }

        InventoryLogic.instance.UpdateSortingOrder();
        slotHolder.transform.position = transform.position;
        pieceHolder.transform.position = transform.position;
    }
}