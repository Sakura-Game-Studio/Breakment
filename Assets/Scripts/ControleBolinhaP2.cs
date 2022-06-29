using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using FMODUnity;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class ControleBolinhaP2 : MonoBehaviour{

    public Rigidbody2D rb;
    public BoxCollider2D colisor;
    public bool rodando;
    public Transform barrinha;
    public float velocidade;
    
    private float velocidadeOriginal = 5;
    private bool habilidade = false;
    private int contadorNimbus = 3;
    
    public GameObject druidATK;
    public GameObject golemATK;
    public GameObject nimbusATK;

    private GameObject newNimbusATK;
    private GameObject newGolemATK;
    private GameObject newDruidATK;

    public FMOD.Studio.EventInstance instance;
    [FMODUnity.EventRef] public string fmodEvent;
    private float valorP2;
    
    public FMOD.Studio.EventInstance skill;
    [FMODUnity.EventRef] public string druidaSkill;
    [FMODUnity.EventRef] public string golemSkill;
    [FMODUnity.EventRef] public string golemLeave;
    [FMODUnity.EventRef] public string nimbusSkill;
    [FMODUnity.EventRef] public string nimbusLeave;

    private bool ataqueNimbus = false;

    private void Start(){
        valorP2 = FindObjectOfType<GameController>().getP2() + 1.0f;
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.setParameterByName("SelectSprite" ,valorP2);
        
        if (valorP2 == 0){
            skill = FMODUnity.RuntimeManager.CreateInstance(druidaSkill);
        } else if (valorP2 == 1){
            skill = FMODUnity.RuntimeManager.CreateInstance(golemSkill);
        } else if (valorP2 == 2){
            skill = FMODUnity.RuntimeManager.CreateInstance(nimbusSkill);
        }

        skill = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        
        velocidadeOriginal = 5f;
        soltarBolinha();
    }

    void Update(){

        if (!rodando) {
            transform.position = barrinha.position;
            colisor.enabled = false;
        }
        else {
            if (rb.velocity.x > 0f && rb.velocity.x != velocidade) {
                rb.velocity.Set(velocidade, rb.velocity.y);
            }
                
            if (rb.velocity.x < 0f && rb.velocity.x != -velocidade){
                rb.velocity.Set(-velocidade, rb.velocity.y);
            }
                
            if (rb.velocity.y > 0f && rb.velocity.y != velocidade) {
                rb.velocity.Set(velocidade, rb.velocity.y);
            }
                
            if (rb.velocity.y < 0f && rb.velocity.y != -velocidade) {
                rb.velocity.Set(-velocidade, rb.velocity.y);
            }
        }

        if (Input.GetButtonDown("Habilidade Ataque 02") && rodando && !habilidade){
            GameObject.Find("Player 02").GetComponent<SpritesController>().Habilidade();
            habilidade = true;
            skill.start();
            FindObjectOfType<GameController>().poderP2.start();
            soltarHabilidade();
        }

        if (contadorNimbus == 0){
            ataqueNimbus = false;
            skill = FMODUnity.RuntimeManager.CreateInstance(nimbusLeave);
            skill.start();
            Destroy(newNimbusATK);
            skill = FMODUnity.RuntimeManager.CreateInstance(nimbusSkill);
            contadorNimbus = 3;
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (ataqueNimbus){
            if (other.CompareTag("Bloco")){
                if (contadorNimbus > 0){
                    int vidaBlocoAtual = other.GetComponent<Bloco>().getVida();
                    
                    if (contadorNimbus == 3){
                        if (vidaBlocoAtual == 3){
                            contadorNimbus -= 3;
                            other.GetComponent<Bloco>().atualizaVida();
                            other.GetComponent<Bloco>().atualizaVida();
                            other.GetComponent<Bloco>().atualizaVida();
                        } if (vidaBlocoAtual == 2){
                            contadorNimbus -= 2;
                            other.GetComponent<Bloco>().atualizaVida();
                            other.GetComponent<Bloco>().atualizaVida();
                        } if (vidaBlocoAtual == 1){
                            contadorNimbus--;
                            other.GetComponent<Bloco>().atualizaVida();
                        }
                    }
                    
                    if (contadorNimbus == 2){
                        if (vidaBlocoAtual == 3){
                            contadorNimbus -= 2;
                            other.GetComponent<Bloco>().atualizaVida();
                            other.GetComponent<Bloco>().atualizaVida();
                        } if (vidaBlocoAtual == 2){
                            contadorNimbus -= 2;
                            other.GetComponent<Bloco>().atualizaVida();
                            other.GetComponent<Bloco>().atualizaVida();
                        } if (vidaBlocoAtual == 1){
                            contadorNimbus--;
                            other.GetComponent<Bloco>().atualizaVida();
                        }
                    }

                    if (contadorNimbus == 1){
                        if (vidaBlocoAtual == 3){
                            contadorNimbus--;
                            other.GetComponent<Bloco>().atualizaVida();
                        } if (vidaBlocoAtual == 2){
                            contadorNimbus--;
                            other.GetComponent<Bloco>().atualizaVida();
                        } if (vidaBlocoAtual == 1){
                            contadorNimbus--;
                            other.GetComponent<Bloco>().atualizaVida();
                        }
                    }
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other){
        instance.start();
        if (other.gameObject.name == "Barrinha P1"){
            float x = PosicaoColisao(transform.position, other.transform.position, other.collider.bounds.size.x);
            Vector2 direcao = new Vector2(x, 1).normalized;

            rb.velocity = direcao * velocidade;
            
        }
        
        if (other.gameObject.name == "Barrinha P2"){
            float x = PosicaoColisao(transform.position, other.transform.position, other.collider.bounds.size.x);
            Vector2 direcao = new Vector2(x, -1).normalized;

            rb.velocity = direcao * velocidade;
        }
    }

    private float PosicaoColisao(Vector2 bolaPosicao, Vector2 barraPosicao, float barraTamanho){
        return (bolaPosicao.x - barraPosicao.x) / barraTamanho;
    }

    public void ResetarPosicao(){
        rb.velocity = Vector2.zero;
        rodando = false;
    }

    public void soltarHabilidade(){
        int player = FindObjectOfType<GameController>().getP2();
        
        if (player == 2){
            ataqueNimbus = true;
            newNimbusATK = Instantiate(nimbusATK, transform.position, Quaternion.identity);
            newNimbusATK.transform.parent = gameObject.transform;
        }

        if (player == 1){
            velocidade = 7.5f;
            newGolemATK = Instantiate(golemATK, transform.position, Quaternion.identity);
            newGolemATK.transform.parent = gameObject.transform;
            this.Wait(4f, () => {
                velocidade = velocidadeOriginal;
                Destroy(newGolemATK);
            });
        }

        if (player == 0){
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * -1);
            newDruidATK = Instantiate(druidATK, transform.position, Quaternion.identity);
            newDruidATK.transform.parent = gameObject.transform;
        }
    }

    public bool getAtaqueNimbus(){
        return ataqueNimbus;
    }
    
    public void soltarBolinha(){
        this.Wait(3f, () => {
            rodando = true;
            colisor.enabled = true;
            float x = Random.Range(-0.5f, 0.5f);
            Vector2 direcao = new Vector2(x, -1).normalized;
            rb.velocity = direcao * velocidade;
        });        
    }

    public bool getHabilidade(){
        return habilidade;
    }
    
    public void setHabilidade(bool boleano){
        habilidade = boleano;
    }
}
