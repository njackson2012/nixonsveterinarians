using System;
using System.Collections;
using System.Threading;

/// <summary>
/// Controls the logic of a checkers game.
/// </summary>    
public class GameController
{
    ///////////////////////////////////////////////////////////////////
    //////////////////////////// Attributes ///////////////////////////
    ///////////////////////////////////////////////////////////////////

    /// <summary>
    /// The checkerboard on which the game is played.  Contains the pieces.
    /// </summary>    
    private BoardLogic _board;

    /// <summary>
    /// The id of the game.
    /// </summary>    
    private int _gameid;

    /// <summary>
    /// The status of the game.
    /// Can be: “Ongoing”, “Waiting4Player2Join”, “WaitingForRequest”, “BlackWon”, or “RedWon”.
    /// </summary>    
    private string _gameStatus;

    /// <summary>
    /// The request of a status.
    /// Can be: “None”, “DrawRequestBlack”, “DrawRequestRed”, “RematchRequestBlack”, or “RematchRequestRed”.
    /// </summary>    
    private string _requestStatus;

    /// <summary>
    /// The player whose turn it is to move.  Can be "Red" or "Black".
    /// </summary>    
    private string _playerTurn;

    /// <summary>
    /// The player's color.  Can be "Red" or "Black".
    /// </summary>    
    private string _playerColor;

    /// <summary>
    /// A Hashtable of available moves.  
    /// </summary>    
    private Hashtable _validMoves = new Hashtable();

    ///////////////////////////////////////////////////////////////////
    //////////////////////////// Constructor //////////////////////////
    ///////////////////////////////////////////////////////////////////
    /// <summary>
    /// Constructor - sets gameId, playerColor,  and board according to parameters.
    /// Sets request status to None.  Still need to set Game Status, Player Turn, and Valid Moves
    /// </summary>
    public GameController(int gameId, string playerColor, BoardLogic board)
    {
        if (playerColor.ToLower() == "red")
            _playerColor = "Red";
        else
            _playerColor = "Black";

        _gameid = gameId;
        _board = board;
        _requestStatus = "None";
    }



    ///////////////////////////////////////////////////////////////////
    //////////////////////////// Get & Set ////////////////////////////
    ///////////////////////////////////////////////////////////////////

    /// <summary>
    /// Get the game id.
    /// </summary>    
    public int GetGameId()
    {
        return _gameid;
    }

    /// <summary>
    /// Get the game status.
    /// Options are “Ongoing”, “Waiting4Player2Join”, “WaitingForRequest”, “BlackWon”, or “RedWon”.
    /// </summary>    
    public string GetGameStatus()
    {
        return _gameStatus;
    }

    /// <summary>
    /// Get the game status.
    /// Options are “Ongoing”, “Waiting4Player2Join”, “WaitingForRequest”, “BlackWon”, or “RedWon”.
    /// </summary>    
    public void SetGameStatus(string status)
    {
        _gameStatus = status;
    }

    /// <summary>
    /// Get the request status.  Options are “None”, “DrawRequestBlack”, “DrawRequestRed”, “RematchRequestBlack”, or “RematchRequestRed”. 
    /// </summary>    
    public string GetRequestStatus()
    {
        return _requestStatus;
    }

    /// <summary>
    /// Set the request status.  Options are “None”, “DrawRequestBlack”, “DrawRequestRed”, “RematchRequestBlack”, or “RematchRequestRed”.
    /// </summary>    
    public void SetRequestStatus(string status)
    {
        this._requestStatus = status;
    }

    /// <summary>
    /// Get the player color.  Options are "Red" or "Black".   
    /// </summary>    
    public string GetPlayerColor()
    {
        return _playerColor;
    }

    /// <summary>
    /// Set the player turn.  Options are "Red" or "Black".   
    /// </summary>    
    public void SetPlayerTurn(string playerTurn)
    {
        this._playerTurn = playerTurn;
    }

    /// <summary>
    /// Get the player turn.  Options are "Red" or "Black".   
    /// </summary>    
    public string GetPlayerTurn()
    {
        return _playerTurn;
    }

    /// <summary>
    /// Get the player's valid turns. 
    /// </summary>    
    public Hashtable GetValidMoves()
    {
        return _validMoves;
    }

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
            string currPlayerTurn = Client.getGame(GetGameId())[4][0];
            if (currPlayerTurn == this.GetPlayerColor())
            {
                return true;
            }
            Thread.Sleep(200);
        }
    }

    /// <summary>
    /// Changes player turn from "Red" to "Black" or from "Black" to "Red".
    /// </summary>    
    public void SwitchTurn()
    {
        if (GetPlayerTurn() == "Red")
            SetPlayerTurn("Black");
        else if (GetPlayerTurn() == "Black")
            SetPlayerTurn("Red");
    }

    /// <summary>
    /// Clears previous valid moves from validMoves and adds all valid moves to validMoves.
    /// </summary>    
    public void FindValidMoves()
    {
        //remove previous valid moves
        _validMoves.Clear();

        //if there are no jumps, then add slides to validMoves.  If there are jumps, they will automatically be added to validMoves
        //and no slide will be valid.  (A rule of checkers - if you can jump, you have to.)
        if (!FindJumps())
        {
            FindSlides();
        }
    }

    /// <summary>
    /// Adds all possible jumps to valid moves.
    /// </summary>    
    private bool FindJumps()
    {
        if (GetPlayerColor() == "Black")
        {
            FindJumpsBlack();
        }
        else if (GetPlayerColor() == "Red")
        {
            FindJumpsRed();
        }
        //if no moves are found, return false.  Otherwise, return true.
        if (GetValidMoves().Count == 0)
        {
            return false;
        }
        return true;
    }


    /// <summary>
    /// Adds all possible jumps for the black player to validMoves.
    /// </summary>    
    private void PlaceInAvailableTargets(ref Nullable<int>[,] availableTargets, int[] newTarget)
    {
        for (int i = 0; i < availableTargets.Length; i++)
        {
            if (availableTargets[i, 0].HasValue)
            {
                availableTargets[i, 0] = newTarget[0];
                availableTargets[i, 1] = newTarget[1];
                return;
            }
        }
    }


    /// <summary>
    /// Adds all possible jumps for the black player to validMoves.
    /// </summary>    
    private void FindJumpsBlack()
    {
        int[] currPos = new int[2];
        int[] target = new int[2];
        int[] enemy = new int[2];
        //set up the data structure for the hastable's value entry.  Needs to be nullable, so we can check whether a slot is full
        //and fill it, if not.  Only need 4 entries, because the most moves a piece could make (King) is 4.
        Nullable<int>[,] availableTargets = new Nullable<int>[4,2];
        //look through all pieces in the piece array.
        for (int i = 0; i < _board.GetPieces().Length; i++)
        {
            //If their color matches the player's color, check for slides available to that piece.
            if (_board.GetPieces()[i].GetColor() == GetPlayerColor())
            {
                //re-initialize the array to Null each time.
                for (int j = 0; j < availableTargets.Length; j++)
                {
                    availableTargets[j, 0] = null;
                    availableTargets[j, 1] = null;
                }
                currPos[0] = _board.GetPieces()[i].GetLocation()[0];
                currPos[1] = _board.GetPieces()[i].GetLocation()[1];

                //If the space up and left has a red piece, and the space 2 up and 2 to the left is available and legal
                //add the jump to valid moves.  (NOTE: for Black, up is negative, left is neg, right is pos.) 
                target[0] = _board.GetPieces()[i].GetLocation()[0] - 2;
                target[1] = _board.GetPieces()[i].GetLocation()[1] - 2;
                enemy[0] = _board.GetPieces()[i].GetLocation()[0] - 1;
                enemy[1] = _board.GetPieces()[i].GetLocation()[1] - 1;
                if (IsAvailableAndLegal(target) && HoldsOpponent(enemy))
                    PlaceInAvailableTargets(ref availableTargets, target);

                //If the space up and right has a red piece, and the space 2 up and 2 to the right is available and legal
                //add the jump to valid moves.  (NOTE: for Black, up is negative, left is neg, right is pos.) 
                target[0] = _board.GetPieces()[i].GetLocation()[0] - 2;
                target[1] = _board.GetPieces()[i].GetLocation()[1] + 2;
                enemy[0] = _board.GetPieces()[i].GetLocation()[0] - 1;
                enemy[1] = _board.GetPieces()[i].GetLocation()[1] + 1;
                if (IsAvailableAndLegal(target) && HoldsOpponent(enemy))
                    PlaceInAvailableTargets(ref availableTargets, target);

                //If we have added any values, add the list to the hash table
                if(availableTargets[0,0].HasValue)
                    _validMoves.Add(currPos, availableTargets);
            }
        }
    }

    /// <summary>
    /// Adds all possible jumps for the red player to validMoves.
    /// </summary>    
    private void FindJumpsRed()
    {
        int[] currPos = new int[2];
        int[] target = new int[2];
        int[] enemy = new int[2];

        //set up the data structure for the hastable's value entry.  Needs to be nullable, so we can check whether a slot is full
        //and fill it, if not.  Only need 4 entries, because the most moves a piece could make (King) is 4.
        Nullable<int>[,] availableTargets = new Nullable<int>[4, 2];

        //look through all pieces in the piece array.
        for (int i = 0; i < _board.GetPieces().Length; i++)
        {
            //If their color matches the player's color, check for slides available to that piece.
            if (_board.GetPieces()[i].GetColor() == GetPlayerColor())
            {
                //re-initialize the array to Null each time.
                for (int j = 0; j < availableTargets.Length; j++)
                {
                    availableTargets[j, 0] = null;
                    availableTargets[j, 1] = null;
                }
                currPos[0] = _board.GetPieces()[i].GetLocation()[0];
                currPos[1] = _board.GetPieces()[i].GetLocation()[1];

                //If the space up and left has a red piece, and the space 2 up and 2 to the left is available and legal
                //add the jump to valid moves.  (NOTE: for Red, up is positive, left is pos, right is neg.) 
                target[0] = _board.GetPieces()[i].GetLocation()[0] + 2;
                target[1] = _board.GetPieces()[i].GetLocation()[1] + 2;
                enemy[0] = _board.GetPieces()[i].GetLocation()[0] + 1;
                enemy[1] = _board.GetPieces()[i].GetLocation()[1] + 1;
                if (IsAvailableAndLegal(target) && HoldsOpponent(enemy))
                    PlaceInAvailableTargets(ref availableTargets, target);

                //If the space up and right has a red piece, and the space 2 up and 2 to the right is available and legal
                //add the jump to valid moves.  (NOTE: for Red, up is pos, left is pos, right is neg.) 
                target[0] = _board.GetPieces()[i].GetLocation()[0] + 2;
                target[1] = _board.GetPieces()[i].GetLocation()[1] - 2;
                enemy[0] = _board.GetPieces()[i].GetLocation()[0] + 1;
                enemy[1] = _board.GetPieces()[i].GetLocation()[1] - 1;
                if (IsAvailableAndLegal(target) && HoldsOpponent(enemy))
                    PlaceInAvailableTargets(ref availableTargets, target);

                //If we have added any values, add the list to the hash table
                if (availableTargets[0, 0].HasValue)
                    _validMoves.Add(currPos, availableTargets);
            }
        }
    }

    /// <summary>
    /// If target is occupied by an opponent's piece, returns true.  Otherwise, returns false.
    /// </summary>    
    private bool HoldsOpponent(int[] target)
    {
        for (int i = 0; i < _board.GetPieces().Length; i++)
        {
            if (_board.GetPieces()[i].GetColor() != GetPlayerColor() &&
                _board.GetPieces()[i].GetLocation()[0] == target[0] &&
                _board.GetPieces()[i].GetLocation()[1] == target[1])
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Adds all possible slides to valid moves.
    /// </summary>    
    private void FindSlides()
    {
        if (GetPlayerColor() == "Black")
        {
            FindSlidesBlack();
        }
        else if (GetPlayerColor() == "Red")
        {
            FindSlidesRed();
        }
    }

    /// <summary>
    /// Adds all possible slides for black player to valid moves.
    /// </summary>    
    private void FindSlidesBlack()
    {
        int[] currPos = new int[2];
        int[] target = new int[2];
        //set up the data structure for the hastable's value entry.  Needs to be nullable, so we can check whether a slot is full
        //and fill it, if not.  Only need 4 entries, because the most moves a piece could make (King) is 4.
        Nullable<int>[,] availableTargets = new Nullable<int>[4, 2];


        //look through all pieces in the piece array.  
        for (int i = 0; i < _board.GetPieces().Length; i++)
        {
            //If their color matches the player's color, check for slides available to that piece.
            if (_board.GetPieces()[i].GetColor() == GetPlayerColor())
            {
                //re-initialize the array to Null each time.
                for (int j = 0; j < availableTargets.Length; j++)
                {
                    availableTargets[j, 0] = null;
                    availableTargets[j, 1] = null;
                }
                currPos[0] = _board.GetPieces()[i].GetLocation()[0];
                currPos[1] = _board.GetPieces()[i].GetLocation()[1];

                //If the space up and to the left is available and legal, add it to valid moves.  (NOTE: for Black, up is negative, left is neg, right is pos.) 
                target[0] = _board.GetPieces()[i].GetLocation()[0] - 1;
                target[1] = _board.GetPieces()[i].GetLocation()[1] - 1;
                if (IsAvailableAndLegal(target))
                    PlaceInAvailableTargets(ref availableTargets, target);

                //If the space up and to the right is available and legal, add it to valid moves.
                target[0] = _board.GetPieces()[i].GetLocation()[0] - 1;
                target[1] = _board.GetPieces()[i].GetLocation()[1] + 1;
                if (IsAvailableAndLegal(target))
                    PlaceInAvailableTargets(ref availableTargets, target);

                //If we have added any values, add the list to the hash table
                if (availableTargets[0, 0].HasValue)
                    _validMoves.Add(currPos, availableTargets);
            }
        }
    }

    /// <summary>
    /// Adds all possible slides for red player to valid moves.
    /// </summary>    
    private void FindSlidesRed()
    {
        int[] currPos = new int[2];
        int[] target = new int[2];
        //set up the data structure for the hastable's value entry.  Needs to be nullable, so we can check whether a slot is full
        //and fill it, if not.  Only need 4 entries, because the most moves a piece could make (King) is 4.
        Nullable<int>[,] availableTargets = new Nullable<int>[4, 2];

        //look through all pieces in the piece array.  
        for (int i = 0; i < _board.GetPieces().Length; i++)
        {
            //If their color matches the player's color, check for slides available to that piece.
            if (_board.GetPieces()[i].GetColor() == GetPlayerColor())
            {
                //re-initialize the array to Null each time.
                for (int j = 0; j < availableTargets.Length; j++)
                {
                    availableTargets[j, 0] = null;
                    availableTargets[j, 1] = null;
                }
                currPos[0] = _board.GetPieces()[i].GetLocation()[0];
                currPos[1] = _board.GetPieces()[i].GetLocation()[1];

                //If the space up and to the left is available and legal, add it to valid moves.  (NOTE: for Red, up is positive, left is pos, right is neg.) 
                target[0] = _board.GetPieces()[i].GetLocation()[0] + 1;
                target[1] = _board.GetPieces()[i].GetLocation()[1] + 1;
                if (IsAvailableAndLegal(target))
                    PlaceInAvailableTargets(ref availableTargets, target);

                //If the space up and to the right is available and legal, add it to valid moves.
                target[0] = _board.GetPieces()[i].GetLocation()[0] + 1;
                target[1] = _board.GetPieces()[i].GetLocation()[1] - 1;
                if (IsAvailableAndLegal(target))
                    PlaceInAvailableTargets(ref availableTargets, target);

                //If we have added any values, add the list to the hash table
                if (availableTargets[0, 0].HasValue)
                    _validMoves.Add(currPos, availableTargets);
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
        for (int i = 0; i < _board.GetPieces().Length; i++)
        {
            if (_board.GetPieces()[i].GetLocation()[0] == target[0] &&
                _board.GetPieces()[i].GetLocation()[1] == target[1])
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
