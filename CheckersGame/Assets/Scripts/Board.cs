﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    public Piece[,] pieces = new Piece[8, 8];
    public GameObject redPiece;
    public GameObject blackPiece;

    public Vector3 boardOffset = new Vector3(4.0f, 0, 4.0f);

    // New game board function
    private void GenerateBoard()
    {
        // Red pieces
        for(int y = 0; y < 3; y++)
        {
            for(int x = 0; x < 8; x += 2)
            {
                GeneratePiece(x, y);
            }
        }
    }

    private void GeneratePiece(int x, int y)
    {
        GameObject newpiece = Instantiate(redPiece) as GameObject;
        newpiece.transform.SetParent(transform);
        Piece p = newpiece.GetComponent<Piece>();
        pieces[x, y] = p;
        MovePiece(p, x, y);
    }

    private void MovePiece(Piece p, int x, int y)
    {
        p.transform.position = (Vector3.right * x) + (Vector3.forward * y) + boardOffset;
    }

	// Use this for initialization
	void Start () {
        GenerateBoard();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
