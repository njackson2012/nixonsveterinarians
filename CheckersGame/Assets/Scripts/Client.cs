using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Assets.Scripts;

public class Client : MonoBehaviour //commented out because not unity UI class
{
    // PHP endpoint URLS for Game management
    public static string insertGameURL = "http://nixonphpconnector.gearhostpreview.com/phpStuff/insertGame.php?";
    public static string selectGameURL = "http://nixonphpconnector.gearhostpreview.com/phpStuff/selectGame.php?";
    public static string deleteGameURL = "http://nixonphpconnector.gearhostpreview.com/phpStuff/deleteGame.php?";

    // PHP endpoint URLs for Piece management
    public static string insertPieceURL = "http://nixonphpconnector.gearhostpreview.com/phpStuff/insertPiece.php?";
    public static string selectPieceURL = "http://nixonphpconnector.gearhostpreview.com/phpStuff/selectPiece.php?";
    public static string deletePieceURL = "http://nixonphpconnector.gearhostpreview.com/phpStuff/deletePiece.php?";

    // The URL below is for a test script. I doubt I'll ever need it again, but I don't want to delelte it yet.
    // public string helloWorldURL = "http://nixonphpconnector.gearhostpreview.com/phpStuff/helloWorld.php";
    // Use this for initialization
    void Start()
    {
        print("Client running");
        // TODO: add a check to determine if connected to the database on strart. 
        // If not, abort game. Maybe a ping endpoint could be established.
    }
    
    public string[][] getGame(int ID)
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

    public string[][] findOpenGames()
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

    public int createGame(string startingColor)
    {
        WWW insertResult = new WWW(insertGameURL + "REQUESTSTATUS=\"Waiting4Player2Join\"&REQUESTSTATUS=NULL&PLAYERTURN=\"Black\"");
        while (!insertResult.isDone)
        {
            Thread.Sleep(100);
        }
        string raw = insertResult.text;
        int num;
        if (int.TryParse(raw, out num))
        {
            return num;
        }
        // A return value of -1 implies an error
        return -1;
    }

    public string listDatbase()
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