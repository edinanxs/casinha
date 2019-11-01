using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Climax : MonoBehaviour
{
    public Button btnItens1;
    public Button btnItens2;
    public Button btnItens3;
    public Button btnItens4;
    public Button btnItens5;
    public Button btnItens6;
    public Button btnItens7;

    public Button btnArray1;
    public Button btnArray2;
    public Button btnArray3;
    public Button btnArray4;
    public Button btnArray5;
    public Button btnArray6;
    public Button btnArray7;

    public Button btnRolarTopo;
    public Button btnRolarBaixo;

    public Sprite Default;
    public Sprite[] ImagesPlano;
    public Sprite[] ImagesBanheiro;
    public Sprite[] ImagesQuarto;
    public Sprite[] ImagesDecoration;
    public Sprite[] ImagesElectronics;
    public Sprite[] ImagesCozinha;
    public Sprite[] ImageSala;

    public SpriteRenderer fundo;

    private int index = 0;
    private int selecionado;
    public GameObject prefab;
    public InputField titulo;

    public GameObject alert;

    //Save Game
    public static int fundoSelecionado = 9999;
    public static List<GameObject> itens;

    public static Events.save objetoSave;
    public static int IdSalvo = 0;

    private static float z = -0.0001f;

    public void showAlert(bool visible)
    {
        alert.SetActive(visible);
    }

    void Awake()
    {
        itens = new List<GameObject>();
        if (objetoSave != null)
        {
            titulo.GetComponent<InputField>().text = objetoSave.titulo;
            //Fundos
            fundo.sprite = ImagesPlano[objetoSave.fundoSelecionado];
            fundoSelecionado = objetoSave.fundoSelecionado;

            foreach (var objeto in objetoSave.itens)
            {
                var newObjeto = Instantiate(prefab, objeto.posicao, objeto.rotacao);
                newObjeto.transform.localScale = objeto.tamanho;

                z += -0.0001f;
                if (objeto.indexSelecionado == 2)
                    newObjeto.GetComponent<SpriteRenderer>().sprite = ImagesBanheiro[objeto.indexImage];
                else if (objeto.indexSelecionado == 3)
                    newObjeto.GetComponent<SpriteRenderer>().sprite = ImagesQuarto[objeto.indexImage];
                else if (objeto.indexSelecionado == 4)
                    newObjeto.GetComponent<SpriteRenderer>().sprite = ImagesDecoration[objeto.indexImage];
                else if (objeto.indexSelecionado == 5)
                    newObjeto.GetComponent<SpriteRenderer>().sprite = ImagesElectronics[objeto.indexImage];
                else if (objeto.indexSelecionado == 6)
                    newObjeto.GetComponent<SpriteRenderer>().sprite = ImagesCozinha[objeto.indexImage];
                else if (objeto.indexSelecionado == 7)
                    newObjeto.GetComponent<SpriteRenderer>().sprite = ImageSala[objeto.indexImage];

                newObjeto.name = objeto.indexSelecionado + "|" + objeto.indexImage;
                newObjeto.transform.SetParent(GameObject.FindGameObjectWithTag("Itens").transform, false);
                itens.Add(newObjeto);
            }
        }
    }
    void Start()
    {
        selecionado = 1;
        showAlert(false);
        preencherImages();
        this.btnItens1.interactable = true;
    }

    #region selecionar itens
    public void SelecionarItens(int btn)
    {
        index = 0;
        selecionado = btn;
        preencherImages();

        if (btn == 1)
            this.btnItens1.interactable = true;
        else
            this.btnItens1.interactable = false;

        if (btn == 2)
            this.btnItens2.interactable = true;
        else
            this.btnItens2.interactable = false;

        if (btn == 3)
            this.btnItens3.interactable = true;
        else
            this.btnItens3.interactable = false;

        if (btn == 4)
            this.btnItens4.interactable = true;
        else
            this.btnItens4.interactable = false;

        if (btn == 5)
            this.btnItens5.interactable = true;
        else
            this.btnItens5.interactable = false;

        if (btn == 6)
            this.btnItens6.interactable = true;
        else
            this.btnItens6.interactable = false;

        if (btn == 7)
            this.btnItens7.interactable = true;
        else
            this.btnItens7.interactable = false;
    }
    #endregion

    #region movimentar images
    public void prosseguirImagem()
    {
        index += 7;
        preencherImages();
    }
    public void voltarImagem()
    {
        index -= 7;
        preencherImages();
    }
    public void preencherImages()
    {
        if (index == 0)
            this.btnRolarTopo.interactable = false;
        else
            this.btnRolarTopo.interactable = true;

        if (selecionado == 1 && index < 15)
        {
            this.btnArray1.image.overrideSprite = ImagesPlano[index];
            this.btnArray2.image.overrideSprite = ImagesPlano[index + 1];
            this.btnArray3.image.overrideSprite = ImagesPlano[index + 2];
            this.btnArray4.image.overrideSprite = ImagesPlano[index + 3];
            this.btnArray5.image.overrideSprite = ImagesPlano[index + 4];
            this.btnArray6.image.overrideSprite = ImagesPlano[index + 5];
            this.btnArray7.image.overrideSprite = ImagesPlano[index + 6];
        }
        else if (selecionado == 1)
        {
            index -= 7;
        }

        if (selecionado == 2 && index < 29)
        {
            this.btnArray1.image.overrideSprite = ImagesBanheiro[index];
            if (index < 28)
            {
                this.btnArray2.image.overrideSprite = ImagesBanheiro[index + 1];
                this.btnArray3.image.overrideSprite = ImagesBanheiro[index + 2];
                this.btnArray4.image.overrideSprite = ImagesBanheiro[index + 3];
                this.btnArray5.image.overrideSprite = ImagesBanheiro[index + 4];
                this.btnArray6.image.overrideSprite = ImagesBanheiro[index + 5];
                this.btnArray7.image.overrideSprite = ImagesBanheiro[index + 6];
            }
            else
            {
                this.btnArray2.image.overrideSprite = Default;
                this.btnArray3.image.overrideSprite = Default;
                this.btnArray4.image.overrideSprite = Default;
                this.btnArray5.image.overrideSprite = Default;
                this.btnArray6.image.overrideSprite = Default;
                this.btnArray7.image.overrideSprite = Default;
            }
        }
        else if (selecionado == 2)
        {
            index -= 7;
        }

        if (selecionado == 3 && index < 22)
        {
            this.btnArray1.image.overrideSprite = ImagesQuarto[index];
            if (index != 21)
            {
                this.btnArray2.image.overrideSprite = ImagesQuarto[index + 1];
                this.btnArray3.image.overrideSprite = ImagesQuarto[index + 2];
                this.btnArray4.image.overrideSprite = ImagesQuarto[index + 3];
                this.btnArray5.image.overrideSprite = ImagesQuarto[index + 4];
                this.btnArray6.image.overrideSprite = ImagesQuarto[index + 5];
                this.btnArray7.image.overrideSprite = ImagesQuarto[index + 6];
            }
            else
            {
                this.btnArray2.image.overrideSprite = Default;
                this.btnArray3.image.overrideSprite = Default;
                this.btnArray4.image.overrideSprite = Default;
                this.btnArray5.image.overrideSprite = Default;
                this.btnArray6.image.overrideSprite = Default;
                this.btnArray7.image.overrideSprite = Default;
            }
        }
        else if (selecionado == 3)
        {
            index -= 7;
        }

        if (selecionado == 4 && index < 29)
        {
            this.btnArray1.image.overrideSprite = ImagesDecoration[index];
            this.btnArray2.image.overrideSprite = ImagesDecoration[index + 1];
            this.btnArray3.image.overrideSprite = ImagesDecoration[index + 2];
            if (index != 28)
            {
                this.btnArray4.image.overrideSprite = ImagesDecoration[index + 3];
                this.btnArray5.image.overrideSprite = ImagesDecoration[index + 4];
                this.btnArray6.image.overrideSprite = ImagesDecoration[index + 5];
                this.btnArray7.image.overrideSprite = ImagesDecoration[index + 6];
            }
            else
            {
                this.btnArray4.image.overrideSprite = Default;
                this.btnArray5.image.overrideSprite = Default;
                this.btnArray6.image.overrideSprite = Default;
                this.btnArray7.image.overrideSprite = Default;
            }
        }
        else if (selecionado == 4)
        {
            index -= 7;
        }

        if (selecionado == 5 && index < 23)
        {
            this.btnArray1.image.overrideSprite = ImagesElectronics[index];
            this.btnArray2.image.overrideSprite = ImagesElectronics[index + 1];
            if (index != 21)
            {
                this.btnArray3.image.overrideSprite = ImagesElectronics[index + 2];
                this.btnArray4.image.overrideSprite = ImagesElectronics[index + 3];
                this.btnArray5.image.overrideSprite = ImagesElectronics[index + 4];
                this.btnArray6.image.overrideSprite = ImagesElectronics[index + 5];
                this.btnArray7.image.overrideSprite = ImagesElectronics[index + 6];
            }
            else
            {
                this.btnArray3.image.overrideSprite = Default;
                this.btnArray4.image.overrideSprite = Default;
                this.btnArray5.image.overrideSprite = Default;
                this.btnArray6.image.overrideSprite = Default;
                this.btnArray7.image.overrideSprite = Default;
            }
        }
        else if (selecionado == 5)
        {
            index -= 7;
        }

        if (selecionado == 6 && index < 25)
        {
            this.btnArray1.image.overrideSprite = ImagesCozinha[index];
            this.btnArray2.image.overrideSprite = ImagesCozinha[index + 1];
            this.btnArray3.image.overrideSprite = ImagesCozinha[index + 2];
            if (index != 21)
            {
                this.btnArray4.image.overrideSprite = ImagesCozinha[index + 3];
                this.btnArray5.image.overrideSprite = ImagesCozinha[index + 4];
                this.btnArray6.image.overrideSprite = ImagesCozinha[index + 5];
                this.btnArray7.image.overrideSprite = ImagesCozinha[index + 6];
            }
            else
            {
                this.btnArray4.image.overrideSprite = Default;
                this.btnArray5.image.overrideSprite = Default;
                this.btnArray6.image.overrideSprite = Default;
                this.btnArray7.image.overrideSprite = Default;
            }
        }
        else if (selecionado == 6)
        {
            index -= 7;
        }

        if (selecionado == 7 && index < 15)
        {
            this.btnArray1.image.overrideSprite = ImageSala[index];
            this.btnArray2.image.overrideSprite = ImageSala[index + 1];
            this.btnArray3.image.overrideSprite = ImageSala[index + 2];
            this.btnArray4.image.overrideSprite = ImageSala[index + 3];
            this.btnArray5.image.overrideSprite = ImageSala[index + 4];
            this.btnArray6.image.overrideSprite = ImageSala[index + 5];
            if (index != 14)
            {
                this.btnArray7.image.overrideSprite = ImageSala[index + 6];
            }
            else
            {
                this.btnArray7.image.overrideSprite = Default;
            }
        }
        else if (selecionado == 7)
        {
            index -= 7;
        }

    }
    #endregion
    public void acao(int btn)
    {
        if (selecionado == 1)
            alterarFundo(btn);
        else
        {
            GameObject objeto = Instantiate(prefab, new Vector3(0f, 0f, z), Quaternion.identity) as GameObject;
            z += -0.0001f;
            if (selecionado == 2)
                objeto.GetComponent<SpriteRenderer>().sprite = ImagesBanheiro[index + btn];
            else if (selecionado == 3)
                objeto.GetComponent<SpriteRenderer>().sprite = ImagesQuarto[index + btn];
            else if (selecionado == 4)
                objeto.GetComponent<SpriteRenderer>().sprite = ImagesDecoration[index + btn];
            else if (selecionado == 5)
                objeto.GetComponent<SpriteRenderer>().sprite = ImagesElectronics[index + btn];
            else if (selecionado == 6)
                objeto.GetComponent<SpriteRenderer>().sprite = ImagesCozinha[index + btn];
            else if (selecionado == 7)
                objeto.GetComponent<SpriteRenderer>().sprite = ImageSala[index + btn];

            objeto.name = selecionado.ToString() + "|" + (index + btn);
            objeto.transform.SetParent(GameObject.FindGameObjectWithTag("Itens").transform, false);
            Events.item = objeto;
            itens.Add(objeto);
        }

    }

    public void alterarFundo(int btn)
    {
        fundo.sprite = ImagesPlano[index + btn];
        fundoSelecionado = index + btn;
    }

}
