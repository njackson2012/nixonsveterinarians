﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a checkerboard.
/// </summary>
public class BoardLogic {
    /// <summary>
    /// Array containing all pieces on the board.
    /// </summary>
    private PieceLogic[] pieces = new PieceLogic[24];

    //some parameter defined by Nick - what does that database need from us to know which game to get the piece array from?
    /// <summary>
    /// Get's the pieces.
    /// </summary>
    public PieceLogic[] GetPieces()
    {
        return pieces;
    }

    /// <summary>
    /// Removes a piece from the board.
    /// </summary>    
    public void RemovePiece(int pieceId)
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            if (pieces[i].GetId() == pieceId)
            {
                pieces[i].SetLocation(new int[] { -1, -1 });
            }
        }
    }
}
