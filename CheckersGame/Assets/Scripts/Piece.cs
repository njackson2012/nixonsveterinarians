using System;

/// <summary>
/// Summary description for Class1
/// </summary>
public class Piece
{
    /// <summary>
    /// Attributes
    /// </summary>
    private int id;
    private int[] location = new int[2];
    private string color;
    private bool king;

    /// <summary>
    /// Constructor
    /// </summary>
    public Piece(int id, string color)
	{

	    //
	    // TODO: Add constructor logic here
	    //
	}

    public int GetId()
    {
        return id;
    }

    public int[] GetLocation()
    {
        return location;
    }

    public void SetLocation(int[] loc)
    {
        location = loc;
    }

    public string GetColor()
    {
        return color;
    }

    public bool IsKing()
    {
        return king;
    }

    public void KingMe()
    {
        king = true;
    }
}
