using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Board : MonoBehaviour
{
    // Pieces for handling the UI side
    private Piece selectedPiece;
    public Piece[,] pieces = new Piece[8, 8];

    // Red and Black piece textures
    public GameObject redPiece;
    public GameObject blackPiece;

    // Vectors for determining mouse position and movement
    private Vector2 mouseOver;
    private Vector2 startDrag;
    // Vectors for placing pieces in correct spot
    private Vector3 boardOffset = new Vector3(-4f,0,-4f);
    private Vector3 pieceOffset = new Vector3(0.5f, 0, 0.5f);

    // checker for seeing if a piece jumped
    private bool jumped;
    // Game Logic Controller for handling
    // Backend pieces
    private GameController black_game;

    // GameObjects for use in Unity
    public GameObject concedeGamePanel;
    public GameObject requestDrawGamePanel;
    public GameObject exitGamePanel;
    public GameObject redWonPanel;
    public GameObject blackWonPanel;
    public GameObject drawPanel;
    public GameObject redPanel;
    public GameObject blackPanel;

    // Use this for initialization
    void Start()
    {
        // Generate board with pieces
        GenerateBoard();
        // Ensure all UI panels are on or off
        concedeGamePanel.SetActive(false);
        requestDrawGamePanel.SetActive(false);
        exitGamePanel.SetActive(false);
        redWonPanel.SetActive(false);
        blackWonPanel.SetActive(false);
        drawPanel.SetActive(false);
        blackPanel.SetActive(true);
        redPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseOver();

        // check turn
        {
            int x = (int)mouseOver.x;
            int y = (int)mouseOver.y;

            // check if piece is valid
            if (selectedPiece != null)
            {
                // used for moving the piece around the screen
                UpdatePieceDrag(selectedPiece);
            }
            if (Input.GetMouseButtonDown(0))
            {
                // pressing down selects the piece at x,y
                SelectPiece(x, y);
            }
            if (Input.GetMouseButtonUp(0))
            {
                // releasing mouse button attempts to move to given location
                TryMove((int)startDrag.x, (int)startDrag.y, x, y);
            }
        }
    }

    // New game board function
    private void GenerateBoard()
    {
        // Black pieces UI
        for (int y = 0; y < 3; y++)
        {
            bool oddRow = (y % 2 == 0);
            for (int x = 0; x < 8; x += 2)
            {
                GeneratePiece((oddRow) ? x : x + 1, y);
            }
        }
        // Red pieces UI
        for (int y = 7; y > 4; y--)
        {
            bool oddRow = (y % 2 == 0);
            for (int x = 0; x < 8; x += 2)
            {
                GeneratePiece((oddRow) ? x : x + 1, y);
            }
        }
        // have to initialize the game controller to handle move validation
        black_game = new GameController(1, "Black");
    }

    // Helper function to generate pieces for board
    private void GeneratePiece(int x, int y)
    {
        bool isBlack = (y > 3) ? false : true;
        GameObject newpiece = Instantiate((isBlack) ? blackPiece : redPiece) as GameObject;
        newpiece.transform.SetParent(transform);
        Piece p = newpiece.GetComponent<Piece>();

        pieces[x, y] = p;
        MovePiece(p, x, y);
    }

    // given x and y find the UI piece at the given
    // x y coordinate 
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
            //Debug.Log(selectedPiece.name + " " + x + " " + y);
        }
    }

    // Determines where the mouse is on the board
    private void UpdateMouseOver()
    {
        if (!Camera.main)
        {
            Debug.Log("Unable to find main camera");
            return;
        }

        // using raycasthits to determine position with respect to Board mask collider
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

    // Picks up a piece and sticks to the mouse until the button
    // is released. (Can grab any piece)
    private void UpdatePieceDrag(Piece p)
    {
        if (!Camera.main)
        {
            Debug.Log("Unable to find main camera");
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("Board")))
        {
            // Moves the piece
            p.transform.position = hit.point + new Vector3(0, 0.1f, 0);
        }
    }

    // given an x, y and x, y attempts to move the piece to the given location
    // uses move validation to determine if move is valid and if the turn should change
    // also determines win condition
    private void TryMove(int xstart, int ystart, int xend, int yend)
    {
        // location of the starting piece
        startDrag = new Vector2(xstart, ystart);
        // the UI piece at the starting position
        selectedPiece = pieces[xstart, ystart];

        //Debug.Log("xstart: " + xstart + " ystart: " + ystart + " xend: " + xend + " yend: " + yend);

        // Check if the piece is null
        if (selectedPiece != null)
        {
            // move the piece to the given end location
            // this provides the visual of the user placing
            // the piece and it *slingshotting* back to it original
            // location if the move is invalid
            MovePiece(selectedPiece, xend, yend);

            // confirm move using logic controller
            if (black_game.IsMoveValid(new int[2,2] { { ystart, xstart }, { yend, xend } }))
            {
                // set the end location to the given UI piece
                pieces[xend, yend] = selectedPiece;
                // null the given start location
                pieces[xstart, ystart] = null;
                // clear the drag vector
                startDrag = Vector2.down;
                // check if the move was a jump
                if (Math.Abs(xstart - xend) == 2)
                {
                    // create a temp piece
                    Piece p = pieces[((xstart + xend) / 2), ((ystart + yend) / 2)];
                    if (p != null)
                    {
                        // set jump checker to true for use in double jumps
                        jumped = true;
                        // remove the jumped piece from the UI array
                        pieces[((xstart + xend) / 2), ((ystart + yend) / 2)] = null;
                        // remove the piece from the actual UI
                        DestroyImmediate(p.gameObject);
                    }
                }

                // perform the move on the logic side
                black_game.MakeMove(new int[2, 2] { { ystart, xstart }, { yend, xend } });
                // get the logical piece at the end location
                PieceLogic pl = black_game.FindPiece(yend, xend);
                // check if the piece is a king (logical) and isn't flipped (UI king)
                if(pl.IsKing() && !selectedPiece.isFlipped)
                {
                    //Debug.Log("King ME");
                    // logical side gives a piece a king after moving
                    // if the piece is a king and isn't flipped in the UI
                    // it needs to be flipped
                    selectedPiece.isFlipped = true;
                    selectedPiece.transform.Rotate(Vector3.right * 180);
                }

                // generate the next table of valid moves for checking for second jumps
                black_game.FindValidMoves();
                //Debug.Log("contined" + black_game.HasContinuedTurn());
                //Debug.Log("jumped: " + jumped);
                // continued turn is a logical indicator the pieces can make another move
                // jumped checks that the piece that moved made a jump (so this move is a second jump)
                if (black_game.HasContinuedTurn() && jumped)
                {
                    // keeps the player the same and they have to make the next jump
                    return;
                }

                // no second turn
                else
                {
                    // reset jumped
                    jumped = false;
                    //Debug.Log("Swapping");
                    // Change the UI indicator that shows the player Turn
                    if (black_game.GetPlayerColor() == "Black")
                    {
                        blackPanel.SetActive(false);
                        redPanel.SetActive(true);
                    }
                    else
                    {
                        blackPanel.SetActive(true);
                        redPanel.SetActive(false);
                    }
                    // Change the logical turn
                    black_game.SwitchTurn();
                }
                // null the piece (reset)
                selectedPiece = null;
                // check if there is a winner
                string winner = black_game.WhoWon();
                if(winner == "NoOne")
                {
                    // continue if no one has won
                    return;
                }
                else if(winner == "Red")
                {
                    // show winner panel for Red if red won
                    redWonPanel.SetActive(true);
                }
                else
                {
                    // show winner panel for Black if black won
                    blackWonPanel.SetActive(true);
                }
            }
            // if piece is null then reset the piece, the vector, and the UI piece global
            else
            {
                MovePiece(selectedPiece, xstart, ystart);
                startDrag = Vector2.zero;
                selectedPiece = null;
                //Debug.Log("Invalid Move");
                return;

            }
        }

    }

    // Performs the moving of the piece
    private void MovePiece(Piece p, int x, int y)
    {
        // uses UI element transform to move position
        p.transform.position = (Vector3.right * x) + (Vector3.forward * y) + boardOffset + pieceOffset;
    }


    //                  //
    //      BUTTONS     //
    //                  //
    // clicked the concede button
    public void ConcedeButton()
    {
        concedeGamePanel.SetActive(true);
        requestDrawGamePanel.SetActive(false);
        exitGamePanel.SetActive(false);
        redWonPanel.SetActive(false);
        blackWonPanel.SetActive(false);
        drawPanel.SetActive(false);
    }

    // licked the concede button's confirm
    public void ConcedeButtonConfirm()
    {
        if(black_game.GetPlayerColor() == "Black")
        {
            concedeGamePanel.SetActive(false);
            requestDrawGamePanel.SetActive(false);
            exitGamePanel.SetActive(false);
            redWonPanel.SetActive(true);
            blackWonPanel.SetActive(false);
            drawPanel.SetActive(false);
        }
        else
        {
            concedeGamePanel.SetActive(false);
            requestDrawGamePanel.SetActive(false);
            exitGamePanel.SetActive(false);
            redWonPanel.SetActive(false);
            blackWonPanel.SetActive(true);
            drawPanel.SetActive(false);
        }
    }

    // clicked the concede button's cancel
    public void ConcedeButtonCancel()
    {
        concedeGamePanel.SetActive(false);
    }

    // clicked the  draw button
    public void RequestDrawButton()
    {
        requestDrawGamePanel.SetActive(true);
        concedeGamePanel.SetActive(false);
        exitGamePanel.SetActive(false);
        redWonPanel.SetActive(false);
        blackWonPanel.SetActive(false);
    }

    // clicked the  draw button's confirm
    public void RequestDrawButtonConfirm()
    {
        drawPanel.SetActive(true);
    }

    // clicked the  draw button's cancel
    public void RequestDrawButtonCancel()
    {
        requestDrawGamePanel.SetActive(false);
    }

    // clicked the Exit To Main Menu button
    public void ExitToMainMenuButton()
    {
        exitGamePanel.SetActive(true);
        concedeGamePanel.SetActive(false);
        requestDrawGamePanel.SetActive(false);
        redWonPanel.SetActive(false);
        blackWonPanel.SetActive(false);
        drawPanel.SetActive(false);
    }

    // clicked the Exit To Main Menu button confirm
    public void ExitToMainMenuButtonConfirm()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // clicked the Exit To Main Menu button cancel
    public void ExitToMainMenuButtonCancel()
    {
        exitGamePanel.SetActive(false);
    }

    // clicked the rematch button
    public void RematchButton()
    {
        // we want to remove all the UI pieces
        foreach (Piece p in pieces)
        {

            if (p != null)
            {
                // destroy removes the texture
                DestroyImmediate(p.gameObject);
            }
        }
        // reinitialize the UI pieces array
        pieces = new Piece[8, 8];

        // re-call the generateBoard function to populate the new
        // board with fresh pieces
        GenerateBoard();
        concedeGamePanel.SetActive(false);
        requestDrawGamePanel.SetActive(false);
        exitGamePanel.SetActive(false);
        redWonPanel.SetActive(false);
        blackWonPanel.SetActive(false);
        drawPanel.SetActive(false);
        blackPanel.SetActive(true);
        redPanel.SetActive(false);
    }

}
