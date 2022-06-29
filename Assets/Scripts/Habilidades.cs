using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Habilidades : MonoBehaviour{
    
    //golem 5, nuvem 6, druida = 4;

    private int player1, player2;
    
    [Header("Habilidade P1")]
    public Image imagemHabilidadeP1;
    public float cooldown1;
    private bool isCooldown1 = false;
    
    [Header("Habilidade P2")]
    public Image imagemHabilidadeP2;
    public float cooldown2;
    private bool isCooldown2 = false;


    private void Start(){
        imagemHabilidadeP1.fillAmount = 0;
        imagemHabilidadeP2.fillAmount = 0;
        
        player1 = FindObjectOfType<GameController>().getP1();
        player2 = FindObjectOfType<GameController>().getP2();

        if (player1 == 0){
            cooldown1 = 5;
        }
        if (player1 == 1){
            cooldown1 = 9;
        }
        if (player1 == 2){
            cooldown1 = 7;
        }
        if (player2 == 0){
            cooldown2 = 5;
        }
        if (player2 == 1){
            cooldown2 = 9;
        }
        if (player2 == 2){
            cooldown2 = 7;
        }
    }

    private void Update(){
        HabilidadeP1();
        HabilidadeP2();
    }

    public void HabilidadeP1(){
        bool comeco = FindObjectOfType<ControleBolinhaP1>().getHabilidade();
        if (comeco && isCooldown1 == false){
            isCooldown1 = true;
            imagemHabilidadeP1.fillAmount = 1;
        }
        if (isCooldown1){
            imagemHabilidadeP1.fillAmount -= 1 / cooldown1 * Time.deltaTime;
            if (imagemHabilidadeP1.fillAmount <= 0){
                imagemHabilidadeP1.fillAmount = 0;
                isCooldown1 = false;
                FindObjectOfType<ControleBolinhaP1>().setHabilidade(isCooldown1);
            }
        }
    }
    
    public void HabilidadeP2(){
        bool comeco = FindObjectOfType<ControleBolinhaP2>().getHabilidade();
        if (comeco && isCooldown2 == false){
            isCooldown2 = true;
            imagemHabilidadeP2.fillAmount = 1;
        }
        if (isCooldown2){
            imagemHabilidadeP2.fillAmount -= 1 / cooldown1 * Time.deltaTime;
            if (imagemHabilidadeP2.fillAmount <= 0){
                imagemHabilidadeP2.fillAmount = 0;
                isCooldown2 = false;
                FindObjectOfType<ControleBolinhaP2>().setHabilidade(isCooldown2);
            }
        }
    }
}
