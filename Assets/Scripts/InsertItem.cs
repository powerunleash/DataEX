using UnityEngine;
using System.Collections;
using Mono.Data.SqliteClient;
using System.Data;
using System;
using UnityEngine.UI;

public class InsertItem : MonoBehaviour {

    public Text itemTxt;
    public InputField nameInput;
	public InputField passwordInput;
    public InputField ageInput;

    public void Insert(){
		string connectionString = "URI=file:" + Application.dataPath + "/myDatabase.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(connectionString);

		dbconn.Open(); //Open connection to the database.

		IDbCommand dbcmd = dbconn.CreateCommand();

		string sqlQuery = "insert into Account (UserName,Password,Age) values ('"+ nameInput.text +"','"+ passwordInput.text + "'," + ageInput.text + ")";
		dbcmd.CommandText = sqlQuery;

		IDataReader reader = dbcmd.ExecuteReader();

		reader.Close();reader = null;
		dbcmd.Dispose();dbcmd = null;
		dbconn.Close();dbconn = null;

    }

    public void Select()
    {
        string connectionString = "URI=file:" + Application.dataPath + "/myDatabase.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(connectionString);

        dbconn.Open(); //Open connection to the database.

        IDbCommand dbcmd = dbconn.CreateCommand();

        string sqlQuery = "SELECT UserName,Password,Age FROM Account";
        dbcmd.CommandText = sqlQuery;

        IDataReader reader = dbcmd.ExecuteReader();
        itemTxt.text = "";
        while (reader.Read())
        {
            string itemUserName = reader.GetString(0);
            string itemPassword = reader.GetString(1);         
            int itemAge = reader.GetInt32(2);
            itemTxt.text += itemUserName + " - " + itemPassword + " - " + itemAge +"\n";
        }

        reader.Close(); reader = null;
        dbcmd.Dispose(); dbcmd = null;
        dbconn.Close(); dbconn = null;
    }

    public void Update()
    {
        Select();
    }
 }

