using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

public class Events : MonoBehaviour
{
    public static GameObject item;
    public InputField titulo;
    public GameObject alert;

    private string conn, sqlQuery;
    IDbConnection dbconn;
    IDbCommand dbcmd;
    private IDataReader reader;

    string DatabaseName = "Banco.s3db";

    void Start()
    {
        Banco();
    }

    void Banco()
    {
        //Application database Path android
        string filepath = Application.persistentDataPath + "/" + DatabaseName+";";
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


    public void Excluir()
    {
        Destroy(item);
        Climax.itens.Remove(item);
    }

    public void Mirror()
    {

        if (item.transform.localRotation.y == 0)
            Climax.itens.Find(x => x.GetInstanceID() == item.GetInstanceID()).transform.localRotation = item.transform.localRotation = Quaternion.Euler(0, 180, 0);
        else
            Climax.itens.Find(x => x.GetInstanceID() == item.GetInstanceID()).transform.localRotation = item.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    public void Aumentar()
    {
        Climax.itens.Find(x => x.GetInstanceID() == item.GetInstanceID()).transform.localScale = item.transform.localScale += new Vector3(float.Parse((item.transform.localScale.x / 100) * 10 + ""), float.Parse(((item.transform.localScale.y / 100) * 10) + ""), 0);
    }

    public void Diminuir()
    {
        Climax.itens.Find(x => x.GetInstanceID() == item.GetInstanceID()).transform.localScale = item.transform.localScale -= new Vector3(float.Parse((item.transform.localScale.x / 100) * 10 + ""), float.Parse(((item.transform.localScale.y / 100) * 10) + ""), 0);
    }

    [Serializable]
    public class save
    {
        public string titulo;
        public int fundoSelecionado;
        public List<data> itens;
    }

    [Serializable]
    public class data
    {
        public Vector3 posicao;
        public Vector3 tamanho;
        public Quaternion rotacao;
        public int indexSelecionado;
        public int indexImage;
    }

    public void Salvar()
    {
        if (string.IsNullOrWhiteSpace(titulo.GetComponent<InputField>().text))
        {
            titulo.GetComponent<InputField>().placeholder.GetComponent<Text>().text = "Digite um titulo antes!";
        }
        else
        {
            var save = new save
            {
                titulo = titulo.text,
                fundoSelecionado = Climax.fundoSelecionado,
                itens = Climax.itens.Select(x => new data
                {
                    posicao = x.transform.position,
                    rotacao = x.transform.localRotation,
                    tamanho = x.transform.localScale,
                    indexSelecionado = int.Parse(x.name.Split('|')[0]),
                    indexImage = int.Parse(x.name.Split('|')[1])
                }).ToList()
            };
            if (Climax.IdSalvo == 0)
            {
                using (dbconn = new SqliteConnection(conn))
                {
                    dbconn.Open(); //Open connection to the database.
                    dbcmd = dbconn.CreateCommand();
                    sqlQuery = $"INSERT INTO Saves (Name, Json) VALUES ('{titulo.GetComponent<InputField>().text}','{JsonUtility.ToJson(save)}')";
                    dbcmd.CommandText = sqlQuery;
                    dbcmd.ExecuteScalar();
                    dbconn.Close();
                }
                using (dbconn = new SqliteConnection(conn))
                {
                    dbconn.Open();
                    IDbCommand dbcmd = dbconn.CreateCommand();
                    string sqlQuery = "SELECT MAX(Id) FROM Saves";
                    dbcmd.CommandText = sqlQuery;
                    IDataReader reader = dbcmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Climax.IdSalvo = reader.GetInt32(0);
                    }
                    reader.Close();
                    reader = null;
                    dbcmd.Dispose();
                    dbcmd = null;
                    dbconn.Close();
                }
            }
            else
            {
                using (dbconn = new SqliteConnection(conn))
                {
                    dbconn.Open(); //Open connection to the database.
                    dbcmd = dbconn.CreateCommand();
                    sqlQuery = $"UPDATE Saves SET Json = '{JsonUtility.ToJson(save)}' WHERE Id =" + Climax.IdSalvo;
                    dbcmd.CommandText = sqlQuery;
                    dbcmd.ExecuteScalar();
                    dbconn.Close();
                }
            }
            showAlert(true);
        }
    }

    public void showAlert(bool visible)
    {
        alert.SetActive(visible);
    }

}
