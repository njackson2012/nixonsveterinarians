using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Models an individual checkers piece.
/// </summary>
public class PieceLogic {

    ///////////////////////////////////////////////////////////////////
    //////////////////////////// Attributes ///////////////////////////
    ///////////////////////////////////////////////////////////////////
    /// <summary>
    /// A unique id for each checker.  (0-23)
    /// </summary>
    private int id;

    /// <summary>
    /// The row, column location (0-7) of the piece.  Row 0 is the
    /// closest row to the red side.  Row 7 is the closest row to the
    /// black side.  Columns increase from left to right from Black's perspective.
    /// </summary>
    private int[] location = new int[2];

    /// <summary>
    /// The piece's color.  Can be red or black.
    /// </summary>
    private string color;

    /// <summary>
    /// True if a piece is a king.  False otherwise.
    /// </summary>
    private bool king;

    ///////////////////////////////////////////////////////////////////
    //////////////////////////// Constructor //////////////////////////
    ///////////////////////////////////////////////////////////////////
    /// <summary>
    /// Constructor
    /// </summary>
    public PieceLogic(int id, string color)
    {
        this.id = id;
        this.color = color;
        this.king = false;
        this.location = new int[] { -1, -1 };
    }

    ///////////////////////////////////////////////////////////////////
    //////////////////////////// Get & Set ////////////////////////////
    ///////////////////////////////////////////////////////////////////
    /// <summary>
    /// Get's the piece's id.
    /// </summary>
    public int GetId()
    {
        return id;
    }

    /// <summary>
    /// Get's the piece's location.
    /// </summary>
    public int[] GetLocation()
    {
        return location;
    }

    /// <summary>
    /// Set's the piece's location.
    /// </summary>
    public void SetLocation(int[] loc)
    {
        location = loc;
    }

    /// <summary>
    /// Get's the piece's color.
    /// </summary>
    public string GetColor()
    {
        return color;
    }

    /// <summary>
    /// Returns true, if PieceLogic is king. False, otherwise.
    /// </summary>
    public bool IsKing()
    {
        return king;
    }

    ///////////////////////////////////////////////////////////////////
    //////////////////////////// Methods //////////////////////////////
    ///////////////////////////////////////////////////////////////////
    /// <summary>
    /// Changes a piece into a king.  If a piece is already kinged, it remains a king an no error is thrown.
    /// </summary>
    public void KingMe()
    {
        king = true;
    }

}
