using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Assets.Scripts;

public class Client : MonoBehaviour
{
    // PHP endpoint URLS for Game management
    public string insertGameURL = "http://nixonphpconnector.gearhostpreview.com/phpStuff/insertGame.php?";
    public string selectGameURL = "http://nixonphpconnector.gearhostpreview.com/phpStuff/selectGame.php?";
    public string deleteGameURL = "http://nixonphpconnector.gearhostpreview.com/phpStuff/deleteGame.php?";

    // PHP endpoint URLs for Piece management
    public string insertPieceURL = "http://nixonphpconnector.gearhostpreview.com/phpStuff/insertPiece.php?";
    public string selectPieceURL = "http://nixonphpconnector.gearhostpreview.com/phpStuff/selectPiece.php?";
    public string deletePieceURL = "http://nixonphpconnector.gearhostpreview.com/phpStuff/deletePiece.php?";

    // The URL below is for a test script. I doubt I'll ever need it again, but I don't want to delelte it yet.
    // public string helloWorldURL = "http://nixonphpconnector.gearhostpreview.com/phpStuff/helloWorld.php";
    // Use this for initialization
    void Start()
    {
        print("Client running");
        // TODO: add a check to determine if connected to the database on strart. 
        // If not, abort game. Maybe a ping endpoint could be established.
    }

    string[][] getGame(int ID)
    {
        WWW selectResult = new WWW(selectGameURL + "SEARCHTYPE=\"GAMEID\"&SEARCHVALUE=" + ID);
        while(! selectResult.isDone)
        {
            Thread.Sleep(100);
        }
        string gameRaw = selectResult.text;

        HackshTable gameTable = new HackshTable();
        gameTable.generateFromRaw(gameRaw);

        return gameTable.toArray();
    }

    string[][] findOpenGames()
    {
        WWW selectResult = new WWW(selectGameURL + "SEARCHTYPE=\"GAMESTATUS\"&SEARCHVALUE=\"Waiting4Player2Join\"");
        while(! selectResult.isDone)
        {
            Thread.Sleep(100);
        }

        string gameRaw = selectResult.text;

        HackshTable gameTable = new HackshTable();
        gameTable.generateFromRaw(gameRaw);

        return gameTable.toArray();
    }

    string listDatbase()
    {
        WWW gameResults = new WWW(selectGameURL);
        while(! gameResults.isDone)
        {
            Thread.Sleep(100);
        }
        string gameRaw = gameResults.text;

        WWW pieceResults = new WWW(selectPieceURL);
        while(! pieceResults.isDone)
        {
            Thread.Sleep(100);
        }
        string pieceRaw = pieceResults.text;

        HackshTable gameTable = new HackshTable();
        HackshTable pieceTable = new HackshTable();

        gameTable.generateFromRaw(gameRaw);
        pieceTable.generateFromRaw(pieceRaw);

        string formatted = "Games Table:\n";
        formatted += gameTable.toString();
        formatted += "\n\nPieces Table:\n";
        formatted += pieceTable.toString();
        
        return formatted;
    }

    // Update is called once per frame
    void Update()
    {

    }
}