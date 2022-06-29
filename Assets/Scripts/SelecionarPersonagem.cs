using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelecionarPersonagem : MonoBehaviour{
    public int p1, p2;
    public GameObject cursorP1, cursorP2;
    public GameObject[] posicoesP1;
    public GameObject[] posicoesP2;
    public bool p1Selecionado = false;
    public bool p2Selecionado = false;

    private void Start(){
        cursorP1.transform.position = posicoesP1[p1].transform.position;
        cursorP2.transform.position = posicoesP2[p2].transform.position;
    }

    private void Update(){
        if (Input.GetKeyDown("left") && !p1Selecionado){
            p1--;

            if (p1 == p2){
                p1--;
            }
            
            if (p1 < 0){
                p1 = 2;
            }

            MudaP1();
        }
        if (Input.GetKeyDown("right") && !p1Selecionado){
            p1++;
            
            if (p1 == p2){
                p1++;
            }
            
            if (p1 == 3){
                p1 = 0;
            }

            MudaP1();
        }
        
        if (Input.GetKeyDown("a") && !p2Selecionado){
            p2--;
            
            if (p1 == p2){
                p2--;
            }
            
            if (p2 < 0){
                p2 = 2;
            }
            
            MudaP2();
        }
        if (Input.GetKeyDown("d") && !p2Selecionado){
            p2++;
            
            if (p1 == p2){
                p1++;
            }
            
            if (p2 == 3){
                p2 = 0;
            }
            
            MudaP2();
        }

        if (Input.GetKeyDown("up")){
            p1Selecionado = true;
        }
        
        if (Input.GetKeyDown("down")){
            p1Selecionado = false;
        }
        
        if (Input.GetKeyDown("w")){
            p2Selecionado = true;
        }
        
        if (Input.GetKeyDown("s")){
            p2Selecionado = false;
        }

        if (p1Selecionado && p2Selecionado){
            FindObjectOfType<GameController>().setP1(p1);
            FindObjectOfType<GameController>().setP2(p2);
            FindObjectOfType<GameController>().GoToFase(0,0);
        }
    }

    private void MudaP1(){
        cursorP1.transform.position = posicoesP1[p1].transform.position;
    }

    private void MudaP2(){
        cursorP2.transform.position = posicoesP2[p2].transform.position;
    }
}
