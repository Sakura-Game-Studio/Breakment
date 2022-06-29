using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisorCima : MonoBehaviour{
    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Bola")){
            FindObjectOfType<ControleBolinhaP1>().ResetarPosicao();
            FindObjectOfType<ControleBolinhaP2>().ResetarPosicao();
            
            FindObjectOfType<ControleVidaP2>().RemoverVida();
            FindObjectOfType<GameController>().AtualizarVidaP2();
        }
    }
}