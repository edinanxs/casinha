using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using GracesGames.SimpleFileBrowser;
using System.Data;
using Mono.Data.Sqlite;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Import : MonoBehaviour
{
    private string conn, sqlQuery;
    IDbConnection dbconn;
    IDbCommand dbcmd;
    private IDataReader reader;

    string DatabaseName = "Banco.s3db";
    public GameObject prefab;
    public GameObject painel;
    void Start()
    {
        Banco();
        painel.SetActive(false);
    }

    public void OpenExplorer()
    {
        painel.SetActive(true);
        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open();
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT Id,Name, Json FROM Saves";
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            var y = 280f;
            while (reader.Read())
            {
                GameObject objeto = Instantiate(prefab, new Vector3(10, y, 0), Quaternion.identity) as GameObject;
                objeto.GetComponentInChildren<Text>().text = reader.GetString(1);
                objeto.transform.SetParent(GameObject.FindGameObjectWithTag("Itens").transform, false);
                objeto.transform.name = reader.GetInt32(0).ToString();
                y -= 75;
            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
        }
    }

    public void loadCena()
    {
        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open(); 
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT Json FROM Saves where Id =" + this.transform.name;
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                Climax.IdSalvo = int.Parse(this.transform.name);
                Climax.objetoSave = JsonUtility.FromJson<Events.save>(reader.GetString(0));
            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
        }
        SceneManager.LoadScene("Climax");
    }

    void Banco()
    {
        //Application database Path android
        string filepath = Application.persistentDataPath + "/" + DatabaseName + ";";
        //if (!File.Exists(filepath))
        //{
        // If not found on android will create Tables and database

        // UNITY_ANDROID
        WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/Employers.s3db");
        while (!loadDB.isDone) { }
        // then save to Application.persistentDataPath
        File.WriteAllBytes(filepath, loadDB.bytes);

        conn = "URI=file:" + filepath;

        Debug.Log("Stablishing connection to: " + conn);
        dbconn = new SqliteConnection(conn);
        dbconn.Open();
        //string query = "DROP TABLE Saves";
        string query = "CREATE TABLE IF NOT EXISTS Saves (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name VARCHAR(255), Json TEXT)";
        try
        {
            dbcmd = dbconn.CreateCommand();
            dbcmd.CommandText = query;
            reader = dbcmd.ExecuteReader();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        //}
    }

}
