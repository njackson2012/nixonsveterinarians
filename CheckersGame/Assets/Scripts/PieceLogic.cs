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
    private int _id;

    /// <summary>
    /// The row, column location (0-7) of the piece.  Row 0 is the
    /// closest row to the red side.  Row 7 is the closest row to the
    /// black side.  Columns increase from left to right from Black's perspective.
    /// </summary>
    private int[] _location = new int[2];

    /// <summary>
    /// The piece's color.  Can be red or black.
    /// </summary>
    private string _color;

    /// <summary>
    /// True if a piece is a king.  False otherwise.
    /// </summary>
    private bool _king;

    ///////////////////////////////////////////////////////////////////
    //////////////////////////// Constructor //////////////////////////
    ///////////////////////////////////////////////////////////////////
    /// <summary>
    /// Constructor
    /// </summary>
    public PieceLogic(int id, string color)
    {
        _id = id;
        _color = color;
        _king = false;
        _location = new int[] { -1, -1 };
    }

    ///////////////////////////////////////////////////////////////////
    //////////////////////////// Get & Set ////////////////////////////
    ///////////////////////////////////////////////////////////////////
    /// <summary>
    /// Get's the piece's id.
    /// </summary>
    public int GetId()
    {
        return _id;
    }

    /// <summary>
    /// Get's the piece's location.
    /// </summary>
    public int[] GetLocation()
    {
        return _location;
    }

    /// <summary>
    /// Set's the piece's location.
    /// </summary>
    public void SetLocation(int[] loc)
    {
        _location = loc;
    }

    /// <summary>
    /// Get's the piece's color.
    /// </summary>
    public string GetColor()
    {
        return _color;
    }

    /// <summary>
    /// Returns true, if PieceLogic is king. False, otherwise.
    /// </summary>
    public bool IsKing()
    {
        return _king;
    }

    ///////////////////////////////////////////////////////////////////
    //////////////////////////// Methods //////////////////////////////
    ///////////////////////////////////////////////////////////////////
    /// <summary>
    /// Changes a piece into a king.  If a piece is already kinged, it remains a king an no error is thrown.
    /// </summary>
    public void KingMe()
    {
        _king = true;
    }

}
