using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SpritesController : MonoBehaviour{
    public int player;
    public int player2;
    public bool trocafundo;
    public GameObject icone;
    public Image habilidadeAtaque;
    public Image habilidadeAtaqueBW;
    public GameObject bolinha;
    public GameObject barrinha;
    public GameObject borda;
    public GameObject fundo;

    public Sprite[] icones;
    public Sprite[] ataques;
    public Sprite[] ataquesBW;
    public Sprite[] bolinhas;
    public Sprite[] barrinhas;

    public Sprite[] bordas;
    public Sprite[] fundos;
    
    public Sprite[] pontos;
    public Sprite[] danos;
    public Sprite[] habilidades;

    private void Start(){
        if (gameObject.name.Equals("Player 01")){
            player = FindObjectOfType<GameController>().getP1();
            player2 = FindObjectOfType<GameController>().getP2();
        }
        
        if (gameObject.name.Equals("Player 02")){
            player = FindObjectOfType<GameController>().getP2();
            player2 = FindObjectOfType<GameController>().getP1();
        }

        icone.GetComponent<SpriteRenderer>().sprite = icones[player];
        habilidadeAtaque.GetComponent<Image>().sprite = ataques[player];
        habilidadeAtaqueBW.GetComponent<Image>().sprite = ataquesBW[player];
        bolinha.GetComponent<SpriteRenderer>().sprite = bolinhas[player];
        barrinha.GetComponent<SpriteRenderer>().sprite = barrinhas[player];

        if (trocafundo){
            if (Random.value<0.5f){
                borda.GetComponent<SpriteRenderer>().sprite = bordas[player];
                fundo.GetComponent<SpriteRenderer>().sprite = fundos[player];
            }else{
                borda.GetComponent<SpriteRenderer>().sprite = bordas[player2];
                fundo.GetComponent<SpriteRenderer>().sprite = fundos[player2];
            }
        }
    }

    public void Ponto(){
        icone.GetComponent<SpriteRenderer>().sprite = pontos[player];
        this.Wait(2f, () => {
            icone.GetComponent<SpriteRenderer>().sprite = icones[player];
        }) ;
    }

    public void Dano(){
        icone.GetComponent<SpriteRenderer>().sprite = danos[player];
        this.Wait(2f, () => {
            icone.GetComponent<SpriteRenderer>().sprite = icones[player];
        }) ;
    }

    public void Habilidade(){
        icone.GetComponent<SpriteRenderer>().sprite = habilidades[player];
        this.Wait(0.5f, () => {
            icone.GetComponent<SpriteRenderer>().sprite = icones[player];
        }) ;
    }
}
