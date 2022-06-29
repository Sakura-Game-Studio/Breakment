using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleBarrinhaP1 : MonoBehaviour{
    private float controle2;

    public float velocidade, cantoEsquerdo, cantoDireito;
    public float posicaoInicialX;
    public float posicaoInicialY;

    void Start() {
        controle2 = 0;
        posicaoInicialX = transform.position.x;
        posicaoInicialY = transform.position.y;
    }
    
    void Update(){
        controle2 = Input.GetAxis("Controle 01");
        
        transform.Translate(Vector2.right * (controle2 * Time.deltaTime * velocidade));

        if (transform.position.x < cantoEsquerdo) {
            transform.position = new Vector2(cantoEsquerdo, transform.position.y);
        }
        
        if (transform.position.x > cantoDireito) {
            transform.position = new Vector2(cantoDireito, transform.position.y);
        }
    }

    public void ResetarPosicao(){
        transform.position = new Vector3(posicaoInicialX, posicaoInicialY, 0);
    }
}