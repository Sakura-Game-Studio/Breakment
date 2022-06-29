using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ControleVidaP1 : MonoBehaviour{
    public List<GameObject> vidas = new List<GameObject>();
    public int posicao;
    
    public Sprite[] spriteCoracoes;

    private void Start(){
        vidas[0].GetComponent<SpriteRenderer>().sprite = spriteCoracoes[FindObjectOfType<GameController>().getP1()];
        vidas[1].GetComponent<SpriteRenderer>().sprite = spriteCoracoes[FindObjectOfType<GameController>().getP1()];
        vidas[2].GetComponent<SpriteRenderer>().sprite = spriteCoracoes[FindObjectOfType<GameController>().getP1()];
    }
    
    public void RemoverVida(){
        vidas[posicao].gameObject.SetActive(false);
        //Destroy(vidas[posicao]);
        posicao++;
    }

    public void resetarVida(){
        for (int i = 0; i < vidas.Count; i++){
            vidas[i].gameObject.SetActive(true);
        }
        posicao = 0;
    }
}