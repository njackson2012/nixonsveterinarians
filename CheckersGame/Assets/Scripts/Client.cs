using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data;
using MySql.Data.MySqlClient;

public class Client : MonoBehaviour
{
    private string username = "nixondb";
    private string password = "Cy5G?_5x09e9";
    private string hostname = "den1.mysql4.gear.host";
    private string connectionString;
    private MySqlConnection DBConn = null;
    // Use this for initialization
    void Start()
    {
        connectionString = "UID=" + username + ";DATABASE=" + hostname + ";PWD=" + password + ";";
        DBConn = new MySqlConnection(connectionString);
    }

    // Update is called once per frame
    void Update()
    {

    }
}