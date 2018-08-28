using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour {

    public Piece[,] pieces = new Piece[8, 8];
    public BoardLogic board_logic;
    public PieceLogic[] piece_logic = new PieceLogic[24];
    public GameController game;
    public GameObject redPiece;
    public GameObject blackPiece;

    private Vector3 boardOffset = new Vector3(-4f,0,-4f);
    private Vector3 pieceOffset = new Vector3(0.5f, 0, 0.5f);

    private Piece selectedPiece;
    private Vector2 mouseOver;
    private Vector2 startDrag;
    private Vector3 endDrag;

    public GameObject concedeGamePanel;
    public GameObject requestDrawGamePanel;
    public GameObject exitGamePanel;

    private void UpdateMouseOver()
    {
        if (!Camera.main)
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
        if (x < 0 || x >= pieces.Length || y < 0 || y >= pieces.Length)
        {
            return;
        }

        Piece p = pieces[x, y];
        if (p != null)
        {
            selectedPiece = p;
            startDrag = mouseOver;
            Debug.Log(selectedPiece.name + " " + x + " " + y);
            foreach (PieceLogic pl in piece_logic)
            {
                int[] location = pl.GetLocation();
                if(Enumerable.SequenceEqual(location, new int[2]{ x, y }))
                {
                    Debug.Log(pl + " " + location[0] + " " + location[1]);
                }
            }
        }
    }

    private void TryMove(int xstart, int ystart, int xend, int yend)
    {
        startDrag = new Vector2(xstart, ystart);
        endDrag = new Vector2(xend, yend);
        selectedPiece = pieces[xstart, ystart];

        MovePiece(selectedPiece, xend, yend);
        game.FindValidMoves();
        Hashtable table = game.GetValidMoves();
        foreach(int[] key in table.Keys)
        {
            Debug.Log("hello");
        }

        foreach (int[] k in table)
        {
            Debug.Log(k + ":" + table[k]);
        }
        if(!game.IsMoveValid(new int[,] { { xstart, ystart },{ xend, yend } }))
        {
            MovePiece(selectedPiece, xstart, ystart);
            Debug.Log("Invalid Move");
        }

        //MovePiece(selectedPiece, xend, yend);
    }

    private void MovePiece(Piece p, int x, int y)
    {
        p.transform.position = (Vector3.right * x) + (Vector3.forward * y) + boardOffset + pieceOffset;
    }

    // New game board function
    private void GenerateBoard()
    {
        // Black pieces UI
        for(int y = 0; y < 3; y++)
        {
            bool oddRow = (y % 2 == 0);
            for(int x = 0; x < 8; x += 2)
            {
                GeneratePiece((oddRow)?x:x+1, y);
            }
        }
        // Red pieces UI
        for(int y = 7; y > 4; y--)
        {
            bool oddRow = (y % 2 == 0);
            for (int x = 0; x < 8; x += 2)
            {
                GeneratePiece((oddRow) ? x : x + 1, y);
            }
        }

        board_logic = new BoardLogic(piece_logic);

        int iter = 0;
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                if (pieces[i,j] != null)
                {
                    piece_logic[iter] = new PieceLogic(iter, "red");
                    piece_logic[iter].SetLocation(new int[2] { i, j });
                    //Debug.Log(iter);
                    iter++;
                }
            }
        }


        // Need to check player's color here
        game = new GameController(1, "black", board_logic);
        //game.

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

	// Use this for initialization
	void Start ()
    {
        GenerateBoard();
        concedeGamePanel.SetActive(false);
        requestDrawGamePanel.SetActive(false);
        exitGamePanel.SetActive(false);
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



    //                  //
    //      BUTTONS     //
    //                  //

    //clicked the concede button
    public void ConcedeButton()
    {
        concedeGamePanel.SetActive(true);
    }

    //clicked the concede button's confirm
    public void ConcedeButtonConfirm()
    {
        Debug.Log("Conceded");
    }

    //clicked the concede button's cancel
    public void ConcedeButtonCancel()
    {
        concedeGamePanel.SetActive(false);
    }

    //clicked the request draw button
    public void RequestDrawButton()
    {
        requestDrawGamePanel.SetActive(true);
    }

    //clicked the request draw button's confirm
    public void RequestDrawButtonConfirm()
    {
        Debug.Log("requesting Draw");
    }

    //clicked the request draw button's cancel
    public void RequestDrawButtonCancel()
    {
        requestDrawGamePanel.SetActive(false);
    }

    //clicked the Exit To Main Menu button
    public void ExitToMainMenuButton()
    {
        exitGamePanel.SetActive(true);
    }

    //clicked the Exit To Main Menu button
    public void ExitToMainMenuButtonConfirm()
    {
        Debug.Log("Exiting");
    }

    //clicked the Exit To Main Menu button
    public void ExitToMainMenuButtonCancel()
    {
        exitGamePanel.SetActive(false);
    }

}
