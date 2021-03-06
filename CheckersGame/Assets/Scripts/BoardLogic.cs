﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a checkerboard.
/// </summary>
public class BoardLogic {


    ///////////////////////////////////////////////////////////////////
    //////////////////////////// Attributes ///////////////////////////
    ///////////////////////////////////////////////////////////////////
    /// <summary>
    /// Array containing all pieces on the board.
    /// </summary>
    private PieceLogic[] _pieces = new PieceLogic[24];


    ///////////////////////////////////////////////////////////////////
    //////////////////////////// Constructor //////////////////////////
    ///////////////////////////////////////////////////////////////////
    /// <summary>
    /// Constructor
    /// </summary>
    public BoardLogic(PieceLogic[] ps)
    {
        this._pieces = ps;
    }


    ///////////////////////////////////////////////////////////////////
    //////////////////////////// Get & Set ////////////////////////////
    ///////////////////////////////////////////////////////////////////
    /// <summary>
    /// Get's the pieces.
    /// </summary>
    public PieceLogic[] GetPieces()
    {
        return _pieces;
    }

    /// <summary>
    /// Set's the pieces.
    /// </summary>
    public void SetPieces(PieceLogic[] ps)
    {
        this._pieces = ps;
    }


    ///////////////////////////////////////////////////////////////////
    //////////////////////////// Methods //////////////////////////////
    ///////////////////////////////////////////////////////////////////
    /// <summary>
    /// Removes a piece from the board.
    /// </summary>    
    public void RemovePiece(int pieceId)
    {
        for (int i = 0; i < _pieces.Length; i++)
        {
            if (_pieces[i].GetId() == pieceId)
            {
                _pieces[i].SetLocation(new int[] { -1, -1 });
            }
        }
    }
}
