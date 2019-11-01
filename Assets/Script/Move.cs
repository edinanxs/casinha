using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    public void CarregarCena(string nome)
    {
        SceneManager.LoadScene(nome);
    }

    public void NovoJogo()
    {
        Climax.objetoSave = null;
        Climax.IdSalvo = 0;
        SceneManager.LoadScene("Climax");
    }

}
