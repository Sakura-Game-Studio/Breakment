using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloco : MonoBehaviour{
    public int vida;
    public bool imortal;
    public int player1, player2;
    public bool ataqueNimbus;
    public GameObject blocoParticle;
    private Shake shake;

    public Sprite[] imagens; 
    
    public FMOD.Studio.EventInstance blocoQuebrando;
    [FMODUnity.EventRef] public string blocoEvent;
    public FMOD.Studio.EventInstance correnteQuebrando;
    [FMODUnity.EventRef] public string correnteEvent;

    private void Start(){
        player1 = FindObjectOfType<GameController>().getP1();
        player2 = FindObjectOfType<GameController>().getP2();

        GetComponent<SpriteRenderer>().sprite = imagens[vida - 1];

        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
        blocoQuebrando = FMODUnity.RuntimeManager.CreateInstance(blocoEvent);
        correnteQuebrando = FMODUnity.RuntimeManager.CreateInstance(correnteEvent);
    }

    private void OnCollisionEnter2D(Collision2D other){
        if (player1 == 2){
            ataqueNimbus = FindObjectOfType<ControleBolinhaP1>().getAtaqueNimbus();
        }
        if (player2 == 2){
            ataqueNimbus = FindObjectOfType<ControleBolinhaP2>().getAtaqueNimbus();
        }
        
        if (!imortal && !ataqueNimbus){
            atualizaVida();
        }
    }
    
    public void atualizaImagem(){
        GetComponent<SpriteRenderer>().sprite = imagens[vida - 1];
    }

    public void atualizaVida(){
        if (!imortal){
            vida--;
            if (vida > 0){
                atualizaImagem();
                correnteQuebrando.start();
            }else{
                Destroy(gameObject);
                blocoQuebrando.start();
                Instantiate(blocoParticle, transform.position, Quaternion.identity);
                shake.CamShake();
            }
        }
    }

    public int getVida(){
        return vida;
    }
}
