using System;
using System.Collections;
using System.Threading;
using Assets.Scripts;
using UnityEngine;

/// <summary>
/// Controls the logic of a checkers game.
/// </summary>    
public class GameController : MonoBehaviour
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

    private Client _client;


    void Start()
    {
        print("Game Controller running");
    }

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
        _client = new Client();
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
            string currPlayerTurn = _client.getGame(GetGameId())[4][0];
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
    private void PlaceInAvailableTargets(ref int[] availableTargets, int[] newTarget)
    {
        //loop through all evenly numbered entries.  If you find -1, place newTarget there.
        for (int i = 0; i < availableTargets.Length; i++)
        {
            if (availableTargets[i] == -1)
            {
                availableTargets[i] = newTarget[0];
                availableTargets[i+1] = newTarget[1];
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
        //set up the data structure for the hastable's value entry.
        //Need 8 entries, because the most moves a piece could make (King) is 4 and each move takes 2 ints to represent.
        int[] availableTargets = new int[8];
        //look through all pieces in the piece array.
        for (int i = 0; i < _board.GetPieces().Length; i++)
        {
            //If their color matches the player's color, check for slides available to that piece.
            if (_board.GetPieces()[i].GetColor() == GetPlayerColor())
            {
                //re-initialize the array to -1 each time.
                for (int j = 0; j < availableTargets.Length; j++)
                    availableTargets[j] = -1;

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
                if (availableTargets[0] != -1)
                {
                    int[] nonRefAvailTargets = new int[8];
                    availableTargets.CopyTo(nonRefAvailTargets, 0);
                    AddToHashTable(currPos, nonRefAvailTargets);//_validMoves.Add(currPos, availableTargets);/**/
                }
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

        //set up the data structure for the hastable's value entry.
        //Need 8 entries, because the most moves a piece could make (King) is 4 and each move takes 2 ints to represent.
        int[] availableTargets = new int[8];

        //look through all pieces in the piece array.
        for (int i = 0; i < _board.GetPieces().Length; i++)
        {
            //If their color matches the player's color, check for slides available to that piece.
            if (_board.GetPieces()[i].GetColor() == GetPlayerColor())
            {
                //re-initialize the array to -1 each time.
                for (int j = 0; j < availableTargets.Length; j++)
                    availableTargets[j] = -1;
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
                if (availableTargets[0] != -1)
                {
                    int[] nonRefAvailTargets = new int[8];
                    availableTargets.CopyTo(nonRefAvailTargets, 0);
                    AddToHashTable(currPos, nonRefAvailTargets);//_validMoves.Add(currPos, availableTargets);/**/
                }
            }
        }
    }

    /// <summary>
    /// If target is occupied by an opponent's piece, returns true.  Otherwise, returns false.
    /// </summary>    
    private bool HoldsOpponent(int[] target)
    {
        //loop over all the pieces - checks whether any opponent piece is in the target location.
        for (int i = 0; i < _board.GetPieces().Length; i++)
        {
            if (_board.GetPieces()[i].GetColor() != GetPlayerColor() &&
                _board.GetPieces()[i].GetLocation()[0] == target[0] &&
                _board.GetPieces()[i].GetLocation()[1] == target[1])
                return true;
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
        //set up the data structure for the hastable's value entry.
        //Need 8 entries, because the most moves a piece could make (King) is 4 and each move takes 2 ints to represent.
        int[] availableTargets = new int[8];


        //look through all pieces in the piece array.  
        for (int i = 0; i < _board.GetPieces().Length; i++)
        {
            //If their color matches the player's color, check for slides available to that piece.
            if (_board.GetPieces()[i].GetColor() == GetPlayerColor())
            {
                //re-initialize the array to -1 each time.
                for (int j = 0; j < availableTargets.Length; j++)
                    availableTargets[j] = -1;

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
                if (availableTargets[0] != -1)
                {
                    int[] nonRefAvailTargets = new int[8];
                    availableTargets.CopyTo(nonRefAvailTargets,0);
                    AddToHashTable(currPos, nonRefAvailTargets);//_validMoves.Add(currPos, availableTargets);/**/
                }
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
        //set up the data structure for the hastable's value entry.
        //Need 8 entries, because the most moves a piece could make (King) is 4 and each move takes 2 ints to represent.
        int[] availableTargets = new int[8];

        //look through all pieces in the piece array.  
        for (int i = 0; i < _board.GetPieces().Length; i++)
        {
            //If their color matches the player's color, check for slides available to that piece.
            if (_board.GetPieces()[i].GetColor() == GetPlayerColor())
            {
                //re-initialize the array to -1 each time.
                for (int j = 0; j < availableTargets.Length; j++)
                    availableTargets[j] = -1;

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
                if (availableTargets[0] != -1)
                {
                    int[] nonRefAvailTargets = new int[8];
                    availableTargets.CopyTo(nonRefAvailTargets, 0);
                    AddToHashTable(currPos, nonRefAvailTargets);//_validMoves.Add(currPos, availableTargets);/**/
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
    /*
    /// <summary>
    /// Checks whether any of the valid moves results in a piece being kinged.
    /// If so, returns true.  If not, returns false.
    /// I'm not sure why we have this (Rob says).
    ///todo: NOT DONE YET
    /// </summary>    
    private bool IsNowKing()
    {
        //If the player is red, then the
        return true;
    }
    */

    /// <summary>
    /// Checks whether the piece is king at the end of its move.
    /// </summary>    
    public bool IsMoveValid(int[,] move)
    {
        //if destination of move is off board, the move is not valid.
        if (move[1, 0] < 0 || move[1, 0] > 7 || move[1, 1] < 0 || move[1, 1] > 7)
            return false;
            //create hashtable of all valid moves (_validMoves)
            FindValidMoves();
        //grab the starting position and finishing position of the argument move
        int[] start = new int[2];
        int[] finish = new int[2];
        for (int i = 0; i < 2; i++)
        {
            start[i] = move[0, i];
            finish[i] = move[1, i];
        }
        //convert start to the datastructure that the hash table needs
        DictValueArray startDva = new DictValueArray(start);

        //If the key is not in the hashtable then the move is not valid
        if (!_validMoves.ContainsKey(startDva))
            return false;

        //grab all the values (finishing positions of valid moves which start at the key) and convert to an int[]
        System.Object validFinishesObject = _validMoves[startDva];
        int[] validFinishes = validFinishesObject as int[];

        //go through all finishing position values.
        for (int i = 0; i < validFinishes.Length; i = i + 2)
        {
            //If we see a -1, the move is not valid
            if (validFinishes[i] == -1)
                return false;
            //If we find the move, it is valid
            if (validFinishes[i] == finish[0] && validFinishes[i + 1] == finish[1])
                return true;
        }
        //if we get through the whole list of valid moves and don't find our move, it is not valid.
        return false;
    }

    /// <summary>
    /// Adds to Hashtable HT[key] = value.  
    /// </summary>    
    private void AddToHashTable(int[] key, int[] value)
    {
        DictValueArray dva = new DictValueArray(key);
        _validMoves.Add(dva, value);
    }

    // Update is called once per frame 
    void Update()
    {

    }
}

