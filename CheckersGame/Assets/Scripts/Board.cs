using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    public Piece[,] pieces = new Piece[8, 8];
    public GameObject redPiece;
    public GameObject blackPiece;

    private Vector3 boardOffset = new Vector3(-4f,0,-4f);
    private Vector3 pieceOffset = new Vector3(0.5f, 0, 0.5f);

    private Piece selectedPiece;
    private Vector2 mouseOver;
    private Vector2 startDrag;
    private Vector3 endDrag;

    // New game board function
    private void GenerateBoard()
    {
        // Black pieces
        for(int y = 0; y < 3; y++)
        {
            bool oddRow = (y % 2 == 0);
            for(int x = 0; x < 8; x += 2)
            {
                GeneratePiece((oddRow)?x:x+1, y);
            }
        }
        // Red pieces
        for(int y = 7; y > 4; y--)
        {
            bool oddRow = (y % 2 == 0);
            for (int x = 0; x < 8; x += 2)
            {
                GeneratePiece((oddRow) ? x : x + 1, y);
            }
        }
    }

    private void GeneratePiece(int x, int y)
    {
        bool isBlack = (y > 3) ? false : true;
        GameObject newpiece = Instantiate((isBlack)?blackPiece:redPiece) as GameObject;
        newpiece.transform.SetParent(transform);
        Piece p = newpiece.GetComponent<Piece>();
        pieces[x, y] = p;
        MovePiece(p, x, y);
    }

    private void MovePiece(Piece p, int x, int y)
    {
        p.transform.position = (Vector3.right * x) + (Vector3.forward * y) + boardOffset + pieceOffset;
    }

	// Use this for initialization
	void Start ()
    {
        GenerateBoard();
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateMouseOver();

        // check turn
        {
            int x = (int)mouseOver.x;
            int y = (int)mouseOver.y;

            if(Input.GetMouseButtonDown(0))
            {
                SelectPiece(x, y);
            }
            if(Input.GetMouseButtonUp(0))
            {
                TryMove((int)startDrag.x, (int)startDrag.y, x, y);
            }
        }
	}

    private void UpdateMouseOver()
    {
        if(!Camera.main)
        {
            Debug.Log("Unable to find main camera");
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("Board")))
        {
            // flipped for proper Array handle usage
            mouseOver.x = (int)(hit.point.x - boardOffset.x);
            mouseOver.y = (int)(hit.point.z - boardOffset.z);
        }
        else
        {
            mouseOver.x = -1;
            mouseOver.y = -1;
        }
    }

    private void SelectPiece(int x, int y)
    {
        // Bounds checking
        if(x < 0 || x >= pieces.Length || y < 0 || y >= pieces.Length)
        {
            return;
        }

        Piece p = pieces[x, y];
        if (p != null)
        {
            selectedPiece = p;
            startDrag = mouseOver;
            Debug.Log(selectedPiece.name + " " + x + " " + y);
        }
    }

    private void TryMove(int xstart, int ystart, int xend, int yend)
    {
        startDrag = new Vector2(xstart, ystart);
        endDrag = new Vector2(xend, yend);
        selectedPiece = pieces[xstart, ystart];

        MovePiece(selectedPiece, xend, yend);
    }
}
