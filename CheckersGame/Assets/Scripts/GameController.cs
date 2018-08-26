using System.Collections;
using System.Collections.Generic;
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
    //////////////////////////// Constructor ////////////////////////////
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
    public string GetGameStatus()
    {
        return gameStatus;
    }

    public string GetRequestStatus()
    {
        return requestStatus;
    }

    public void SetRequestStatus(string status)
    {
        this.requestStatus = status;
    }

    public string GetPlayerTurn()
    {
        return playerTurn;
    }
}
