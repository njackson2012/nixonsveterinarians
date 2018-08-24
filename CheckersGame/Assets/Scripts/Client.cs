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

    public string helloWorldURL = "http://nixonphpconnector.gearhostpreview.com/phpStuff/helloWorld.php";
    // Use this for initialization
    void Start()
    {
        string selectResult = getGame(1);
        print(selectResult);
        selectResult = listDatbase();
        string[] words = selectResult.Split(' ');
        foreach (string word in words)
        {
            print(word);
        }
        print(selectResult);
    }

    string getGame(int ID)
    {
        WWW selectResult = new WWW(selectGameURL + "GAMEID=" + ID);
        while(! selectResult.isDone)
        {
            Thread.Sleep(100);
        }
        return selectResult.text;
    }

    string listDatbase()
    {
        WWW results = new WWW(helloWorldURL);
        while(! results.isDone)
        {
            Thread.Sleep(100);
        }
        return results.text;
    }

    // Update is called once per frame
    void Update()
    {

    }
}