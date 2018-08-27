using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/// <summary>
/// Controls the logic of a checkers game.
/// </summary>    
public class GameController
{
    ///////////////////////////////////////////////////////////////////
    //////////////////////////// Attributes ///////////////////////////
    ///////////////////////////////////////////////////////////////////

    /// <summary>
    /// The entity though which GameController communicates with the GearHost Database.
    /// </summary>    
    private Client client;

    /// <summary>
    /// The checkerboard on which the game is played.  Contains the pieces.
    /// </summary>    
    private BoardLogic board;

    /// <summary>
    /// The status of the game.
    /// Can be: “Ongoing”, “Waiting4Player2Join”, “WaitingForRequest”, “BlackWon”, or “RedWon”.
    /// </summary>    
    private string gameStatus;

    /// <summary>
    /// The request of a status.
    /// Can be: “None”, “DrawRequestBlack”, “DrawRequestRed”, “RematchRequestBlack”, or “RematchRequestRed”.
    /// </summary>    
    private string requestStatus;

    /// <summary>
    /// The player whose turn it is to move.  Can be "Red" or "Black".
    /// </summary>    
    private string playerTurn;

    /// <summary>
    /// The player's color.  Can be "Red" or "Black".
    /// </summary>    
    private string playerColor;

    /// <summary>
    /// A Hashtable of available moves.  
    /// </summary>    
    private Hashtable validMoves;

    ///////////////////////////////////////////////////////////////////
    //////////////////////////// Constructor //////////////////////////
    ///////////////////////////////////////////////////////////////////
    /// <summary>
    /// Constructor
    /// </summary>
    public GameController(string playerColor)
    {
        this.playerColor = playerColor;
    }



    ///////////////////////////////////////////////////////////////////
    //////////////////////////// Get & Set ////////////////////////////
    ///////////////////////////////////////////////////////////////////
    /// <summary>
    /// Get the game status.
    /// Options are “Ongoing”, “Waiting4Player2Join”, “WaitingForRequest”, “BlackWon”, or “RedWon”.
    /// </summary>    
    public string GetGameStatus()
    {
        return gameStatus;
    }

    /// <summary>
    /// Get the request status.  Options are “None”, “DrawRequestBlack”, “DrawRequestRed”, “RematchRequestBlack”, or “RematchRequestRed”. 
    /// </summary>    
    public string GetRequestStatus()
    {
        return requestStatus;
    }

    /// <summary>
    /// Set the request status.  Options are “None”, “DrawRequestBlack”, “DrawRequestRed”, “RematchRequestBlack”, or “RematchRequestRed”.
    /// </summary>    
    public void SetRequestStatus(string status)
    {
        this.requestStatus = status;
    }

    /// <summary>
    /// Get the player turn.  Options are "Red" or "Black".   
    /// </summary>    
    public string GetPlayerTurn()
    {
        return playerTurn;
    }

    ///////////////////////////////////////////////////////////////////
    //////////////////////////// Methods //////////////////////////////
    ///////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////
    //////////////////////////// Methods //////////////////////////////
    ///////////////////////////////////////////////////////////////////

    /// <summary>
    /// Queries database every 0.2 seconds.  When it is the player's turn, returns true.
    /// Does not return until then.  Never returns false.
    /// </summary>    
    public bool IsMyTurn()
    {
        while (true)
        {
            //Query database for whose turn it is: Nick - how do I get whose turn it is from the database?
            string currPlayerTurn = "Nicks stuff here";
            if (currPlayerTurn == this.playerTurn)
            {
                return true;
            }

            Thread.Sleep(1000);
        }
    }

    /// <summary>
    /// Changes player turn from "Red" to "Black" or from "Black" to "Red".
    /// </summary>    
    public void SwitchTurn()
    {
        if (playerTurn == "Red")
            playerTurn = "Black";
        else if (playerTurn == "Black")
            playerTurn = "Red";
    }

    /// <summary>
    /// Clears previous valid moves from validMoves and adds all valid moves to validMoves.
    /// Todo: finish find Jumps.  
    /// </summary>    
    public void FindValidMoves()
    {
        //remove previous valid moves
        validMoves.Clear();

        //if there are no jumps, then add slides to validMoves.  If there are jumps, they will automatically be added to validMoves
        //and no slide will be valid.  (A rule of checkers - if you can jump, you have to.)
        if (!FindJumps())
        {
            FindSlides();
        }
    }

    /// <summary>
    /// Adds all possible jumps to valid moves.
    /// Todo: finish
    /// </summary>    
    private bool FindJumps()
    {
        if (playerColor == "Black")
        {
            FindJumpsBlack();
        }
        else if (playerColor == "Red")
        {
            FindJumpsRed();
        }
        //if no moves are found, return false.  Otherwise, return true.
        if (validMoves.Count == 0)
        {
            return false;
        }
        return true;
    }


    /// <summary>
    /// Adds all possible jumps for the black player to validMoves.
    /// Todo: finish
    /// </summary>    
    private void FindJumpsBlack()
    {
        //look through all pieces in the piece array.  If their color matches the player's color, check for slides available to that piece.
        for (int i = 0; i < board.GetPieces().Length; i++)
        {
            if (board.GetPieces()[i].GetColor() == playerColor)
            {
                int[] currPos = new int[2];
                currPos[0] = board.GetPieces()[i].GetLocation()[0];
                currPos[1] = board.GetPieces()[i].GetLocation()[1];

                //If the space up and left has a red piece, and the space 2 up and 2 to the left is available and legal
                //add the jump to valid moves.  (NOTE: for Black, up is negative, left is neg, right is pos.) 
                int[] lefttarget = new int[2];
                lefttarget[0] = board.GetPieces()[i].GetLocation()[0] - 2;
                lefttarget[1] = board.GetPieces()[i].GetLocation()[1] - 2;
                int[] leftenemy = new int[2];
                leftenemy[0] = board.GetPieces()[i].GetLocation()[0] - 1;
                leftenemy[1] = board.GetPieces()[i].GetLocation()[1] - 1;

/*                if (IsAvailableAndLegal(lefttarget) && HoldsOpponent(leftenemy))
                {
                    validMoves.Add(currPos, lefttarget);
                }
                */


                //If the space up and to the right is available and legal, add it to valid moves.
                int[] righttarget = new int[2];
                righttarget[0] = board.GetPieces()[i].GetLocation()[0] - 1;
                righttarget[1] = board.GetPieces()[i].GetLocation()[1] + 1;
                if (IsAvailableAndLegal(righttarget))
                {
                    validMoves.Add(currPos, righttarget);
                }
            }
        }
    }

    /// <summary>
    /// Adds all possible jumps for the red player to validMoves.
    /// Todo: finish
    /// </summary>    
    private void FindJumpsRed()
    {

    }

    /// <summary>
    /// Adds all possible slides to valid moves.
    /// </summary>    
    private void FindSlides()
    {
        if (playerColor == "Black")
        {
            FindSlidesBlack();
        }
        else if (playerColor == "Red")
        {
            FindSlidesRed();
        }
    }

    /// <summary>
    /// Adds all possible slides for black player to valid moves.
    /// </summary>    
    private void FindSlidesBlack()
    {
        //look through all pieces in the piece array.  If their color matches the player's color, check for slides available to that piece.
        for (int i = 0; i < board.GetPieces().Length; i++)
        {
            if (board.GetPieces()[i].GetColor() == playerColor)
            {
                int[] currPos = new int[2];
                currPos[0] = board.GetPieces()[i].GetLocation()[0];
                currPos[1] = board.GetPieces()[i].GetLocation()[1];

                //If the space up and to the left is available and legal, add it to valid moves.  (NOTE: for Black, up is negative, left is neg, right is pos.) 
                int[] lefttarget = new int[2];
                lefttarget[0] = board.GetPieces()[i].GetLocation()[0] - 1;
                lefttarget[1] = board.GetPieces()[i].GetLocation()[1] - 1;
                if (IsAvailableAndLegal(lefttarget))
                {
                    validMoves.Add(currPos, lefttarget);
                }

                //If the space up and to the right is available and legal, add it to valid moves.
                int[] righttarget = new int[2];
                righttarget[0] = board.GetPieces()[i].GetLocation()[0] - 1;
                righttarget[1] = board.GetPieces()[i].GetLocation()[1] + 1;
                if (IsAvailableAndLegal(righttarget))
                {
                    validMoves.Add(currPos, righttarget);
                }
            }
        }
    }

    /// <summary>
    /// Adds all possible slides for red player to valid moves.
    /// </summary>    
    private void FindSlidesRed()
    {
        //look through all pieces in the piece array.  
        for (int i = 0; i < board.GetPieces().Length; i++)
        {
            //If their color matches the player's color, check for slides available to that piece.
            if (board.GetPieces()[i].GetColor() == playerColor)
            {
                int[] currPos = new int[2];
                currPos[0] = board.GetPieces()[i].GetLocation()[0];
                currPos[1] = board.GetPieces()[i].GetLocation()[1];

                //If the space up and to the left is available and legal, add it to valid moves.  (NOTE: for Red, up is positive, left is pos, right is neg.) 
                int[] lefttarget = new int[2];
                lefttarget[0] = board.GetPieces()[i].GetLocation()[0] + 1;
                lefttarget[1] = board.GetPieces()[i].GetLocation()[1] + 1;
                if (IsAvailableAndLegal(lefttarget))
                {
                    validMoves.Add(lefttarget, currPos);
                }

                //If the space up and to the right is available and legal, add it to valid moves.
                int[] righttarget = new int[2];
                righttarget[0] = board.GetPieces()[i].GetLocation()[0] + 1;
                righttarget[1] = board.GetPieces()[i].GetLocation()[1] - 1;
                if (IsAvailableAndLegal(righttarget))
                {
                    validMoves.Add(righttarget, currPos);
                }
            }
        }
    }

    /// <summary>
    /// Checks whether the target is empty of pieces and on the board.
    /// </summary>    
    private bool IsAvailableAndLegal(int[] target)
    {
        //If target square is off board, it is not legal.
        if (target[0] < 0 || target[0] > 7 || target[1] < 0 || target[1] > 7)
        {
            return false;
        }
        //If another piece is on the target square, it is not available.
        for (int i = 0; i < board.GetPieces().Length; i++)
        {
            if (board.GetPieces()[i].GetLocation()[0] == target[0] &&
                board.GetPieces()[i].GetLocation()[1] == target[1])
            {
                return false;
            }
        }
        //otherwise, the square is available
        return true;
    }

    /// <summary>
    /// Checks whether the piece is king at the end of its move.
    ///todo: NOT DONE YET
    /// </summary>    
    private bool IsNowKing()
    {
        return true;
    }

}
