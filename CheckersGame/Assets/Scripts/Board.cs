using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private Piece[] pieces = new Piece[24];

    public GameObject redPiecePreFab;

    public GameObject blackPiecePreFab;

    //some parameter defined by Nick - what does that database need from us to know which game to get the piece array from?
    public Piece[] GetPieces()
    {
        return pieces;
    }

    public void RemovePiece(int pieceId)
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            if (pieces[i].GetId() == pieceId)
            {
                pieces[i].SetLocation(new int[] {-1,-1});
            }
        }
    }
    public void GenerateBoard()
    {
        //Generate Red Pieces

        //Generate Black Pieces

    }
	// Use this for initialization
	public void Start () {
		
	}
	
	// Update is called once per frame
	public void Update () {
		
	}
}
