using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FimJogo : MonoBehaviour{
    public GameObject imagemDruida, imagemGolem, imagemNuvem;
    public GameObject textoDruida, textoGolem, textoNuvem;

    private void Start(){
        int player = FindObjectOfType<GameController>().getGanhador();  
        PlayerWin(player);
    }

    public void PlayerWin(int player){
        if (player == 0){
            imagemDruida.SetActive(true);
            textoDruida.SetActive(true);
        } else if (player == 1){
            imagemGolem.SetActive(true);
            textoGolem.SetActive(true);
        } else{
            imagemNuvem.SetActive(true);
            textoNuvem.SetActive(true);
        }
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.Return)){
            GameObject fimGame = GameObject.Find("GameController");
            GameObject colisores = GameObject.Find("Colisores");
            Destroy(fimGame);
            Destroy(colisores);
            SceneManager.LoadScene("Main Menu");
        }
    }
}
