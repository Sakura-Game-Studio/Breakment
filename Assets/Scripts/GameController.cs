using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = System.Object;

public class GameController : MonoBehaviour{

    public int vidaP1;
    public int vidaP2;
    public int fase;
    public int fasesP1;
    public int fasesP2;

    public int p1, p2;
    
    public String[] fases ={"Fase 01","Fase 02", "Fase 03", "Fase 04", "Fase 05"};

    public GameObject player1;
    public GameObject player2;
    public GameObject objetoVidaP1;
    public GameObject objetoVidaP2;

    private int ganhador;
    
    public FMOD.Studio.EventInstance instance;
    [FMODUnity.EventRef] public string fmodEvent;
    
    public FMOD.Studio.EventInstance derrotaP1;
    public FMOD.Studio.EventInstance pontoP1;
    public FMOD.Studio.EventInstance poderP1;
    public FMOD.Studio.EventInstance danoP1;
    public FMOD.Studio.EventInstance vitoriaP1;
    
    public FMOD.Studio.EventInstance derrotaP2;
    public FMOD.Studio.EventInstance pontoP2;
    public FMOD.Studio.EventInstance poderP2;
    public FMOD.Studio.EventInstance danoP2;
    public FMOD.Studio.EventInstance vitoriaP2;
    
    [FMODUnity.EventRef] public string derrotaEventDruida;
    [FMODUnity.EventRef] public string pontoEventDruida;
    [FMODUnity.EventRef] public string poderEventDruida;
    [FMODUnity.EventRef] public string danoEventDruida;
    [FMODUnity.EventRef] public string vitoriaEventDruida;
    
    [FMODUnity.EventRef] public string derrotaEventGolem;
    [FMODUnity.EventRef] public string pontoEventGolem;
    [FMODUnity.EventRef] public string poderEventGolem;
    [FMODUnity.EventRef] public string danoEventGolem;
    [FMODUnity.EventRef] public string vitoriaEventGolem;
    
    [FMODUnity.EventRef] public string derrotaEventNimbus;
    [FMODUnity.EventRef] public string pontoEventNimbus;
    [FMODUnity.EventRef] public string poderEventNimbus;
    [FMODUnity.EventRef] public string danoEventNimbus;
    [FMODUnity.EventRef] public string vitoriaEventNimbus;
    

    void Start(){
        DontDestroyOnLoad(gameObject);
        fase = 0;
        vidaP1 = 3;
        vidaP2 = 3;
    }

    public void AtualizarVidaP1(){
        player1 = GameObject.Find("Player 01");
        player2 = GameObject.Find("Player 02");
        vidaP1--;
        ResetarPosicao();
        player1.GetComponent<SpritesController>().Dano();
        danoP1.start();
        player2.GetComponent<SpritesController>().Ponto();
        pontoP2.start();
        
        FindObjectOfType<ControleBolinhaP1>().soltarBolinha();
        FindObjectOfType<ControleBolinhaP2>().soltarBolinha();

        intro();

        VerificarVida();
    }
    
    public void AtualizarVidaP2(){
        player1 = GameObject.Find("Player 01");
        player2 = GameObject.Find("Player 02");
        vidaP2--;
        ResetarPosicao();
        player2.GetComponent<SpritesController>().Dano();
        danoP2.start();
        player1.GetComponent<SpritesController>().Ponto();
        pontoP1.start();
        
        FindObjectOfType<ControleBolinhaP1>().soltarBolinha();
        FindObjectOfType<ControleBolinhaP2>().soltarBolinha();
        
        intro();
        
        VerificarVida();
    }

    public void VerificarVida(){
        if (vidaP1 <= 0){
            fase++;
            fasesP2++;
            if (fasesP2 == 3){
                EndGame(p2);
            }
            else{
                GoToFase(fase, 2);
            }
        }
        if (vidaP2 <= 0){
            fase++;
            fasesP1++;
            if (fasesP1 == 3){
                EndGame(p1);
            }
            else{
                GoToFase(fase, 1);
            }
        }
    }

    public void EndGame(int playerNumero){
        player1 = GameObject.Find("Player 01");
        Destroy(player1);
        player2 = GameObject.Find("Player 02");
        Destroy(player2);
        
        objetoVidaP1 =GameObject.Find("Vidas P1");
        objetoVidaP2 =GameObject.Find("Vidas P2");
        
        Destroy(GameObject.Find("Rodadas P1"));
        Destroy(GameObject.Find("Rodadas P2"));
        Destroy(GameObject.Find("Colisores"));
        Destroy(GameObject.Find("Borda Personagens"));
        Destroy(GameObject.Find("Fundo"));
        Destroy(GameObject.Find("Canvas"));
        Destroy(GameObject.Find("EventSystem"));
        
        if (objetoVidaP1 != null){
            Destroy(objetoVidaP1);
        }
        if (objetoVidaP2 != null){
            Destroy(objetoVidaP2);
        }

        ganhador = playerNumero;

        if (ganhador == p1){
            vitoriaP1.start();
            derrotaP2.start();
        }
        if (ganhador == p2){
            vitoriaP2.start();
            derrotaP1.start();
        }

        SceneManager.LoadScene("Fim de Jogo");
    }
    
    public void GoToFase(int fase, int player){
        if (fase != 0){
            ResetarPosicao();
            FindObjectOfType<ControleVidaP1>().resetarVida();
            FindObjectOfType<ControleVidaP2>().resetarVida();
            if (player == 1){
                FindObjectOfType<RodadaP1>().AddRodada();
            } else{
                FindObjectOfType<RodadaP2>().AddRodada();
            }
        }

        setUpSounds();

        vidaP1 = 3;
        vidaP2 = 3;
        SceneManager.LoadScene(fases[this.fase]);
        if (!SceneManager.GetActiveScene().name.Equals("Fim de Jogo")){
            intro();
        }
    }

    public int getP1(){
        return p1;
    }
    
    public int getP2(){
        return p2;
    }

    public void setP1(int valor){
        p1 = valor;
    }
    
    public void setP2(int valor){
        p2 = valor;
    }

    public int getGanhador(){
        return ganhador;
    }

    public void ResetarPosicao(){
        FindObjectOfType<ControleBolinhaP1>().ResetarPosicao();
        FindObjectOfType<ControleBolinhaP2>().ResetarPosicao();
        FindObjectOfType<ControleBarrinhaP1>().ResetarPosicao();
        FindObjectOfType<ControleBarrinhaP2>().ResetarPosicao();
    }

    public void intro(){
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.start();
    }

    public void setUpSounds(){
        if (p1 == 0){
            derrotaP1 = FMODUnity.RuntimeManager.CreateInstance(derrotaEventDruida);
            pontoP1 = FMODUnity.RuntimeManager.CreateInstance(pontoEventDruida);
            poderP1 = FMODUnity.RuntimeManager.CreateInstance(poderEventDruida);
            danoP1 = FMODUnity.RuntimeManager.CreateInstance(danoEventDruida);
            vitoriaP1 = FMODUnity.RuntimeManager.CreateInstance(vitoriaEventDruida);
        } else if (p1 == 1){
            derrotaP1 = FMODUnity.RuntimeManager.CreateInstance(derrotaEventGolem);
            pontoP1 = FMODUnity.RuntimeManager.CreateInstance(pontoEventGolem);
            poderP1 = FMODUnity.RuntimeManager.CreateInstance(poderEventGolem);
            danoP1 = FMODUnity.RuntimeManager.CreateInstance(danoEventGolem);
            vitoriaP1 = FMODUnity.RuntimeManager.CreateInstance(vitoriaEventGolem);
        } else if (p1 == 2){
            derrotaP1 = FMODUnity.RuntimeManager.CreateInstance(derrotaEventNimbus);
            pontoP1 = FMODUnity.RuntimeManager.CreateInstance(pontoEventNimbus);
            poderP1 = FMODUnity.RuntimeManager.CreateInstance(poderEventNimbus);
            danoP1 = FMODUnity.RuntimeManager.CreateInstance(danoEventNimbus);
            vitoriaP1 = FMODUnity.RuntimeManager.CreateInstance(vitoriaEventNimbus);
        }
        
        if (p2 == 0){
            derrotaP2 = FMODUnity.RuntimeManager.CreateInstance(derrotaEventDruida);
            pontoP2 = FMODUnity.RuntimeManager.CreateInstance(pontoEventDruida);
            poderP2 = FMODUnity.RuntimeManager.CreateInstance(poderEventDruida);
            danoP2 = FMODUnity.RuntimeManager.CreateInstance(danoEventDruida);
            vitoriaP2 = FMODUnity.RuntimeManager.CreateInstance(vitoriaEventDruida);
        } else if (p2 == 1){
            derrotaP2 = FMODUnity.RuntimeManager.CreateInstance(derrotaEventGolem);
            pontoP2 = FMODUnity.RuntimeManager.CreateInstance(pontoEventGolem);
            poderP2 = FMODUnity.RuntimeManager.CreateInstance(poderEventGolem);
            danoP2 = FMODUnity.RuntimeManager.CreateInstance(danoEventGolem);
            vitoriaP2 = FMODUnity.RuntimeManager.CreateInstance(vitoriaEventGolem);
        } else if (p2 == 2){
            derrotaP2 = FMODUnity.RuntimeManager.CreateInstance(derrotaEventNimbus);
            pontoP2 = FMODUnity.RuntimeManager.CreateInstance(pontoEventNimbus);
            poderP2 = FMODUnity.RuntimeManager.CreateInstance(poderEventNimbus);
            danoP2 = FMODUnity.RuntimeManager.CreateInstance(danoEventNimbus);
            vitoriaP2 = FMODUnity.RuntimeManager.CreateInstance(vitoriaEventNimbus);
        }
    }
}
