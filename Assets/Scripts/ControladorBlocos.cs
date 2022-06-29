using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorBlocos : MonoBehaviour{
    public int linhas, colunas;
    public float espacoX, espacoY;
    public GameObject blocoPrefb;

    private List<GameObject> blocos = new List<GameObject>();

    private void Start() {
        ResetLevel();
    }

    public void ResetLevel (){
        foreach(GameObject bloco in blocos){
            Destroy(bloco);
        }
        
        blocos.Clear();

        for (int x = 0; x < colunas; x++){               
            for (int y = 0; y < linhas; y++){               
                Vector2 spawnPos = (Vector2)transform.position + new Vector2(x * (blocoPrefb.transform.localScale.x + espacoX), - y * (blocoPrefb.transform.localScale.y + espacoY));
                GameObject bloco = Instantiate(blocoPrefb, spawnPos, Quaternion.identity);
                blocos.Add(bloco);
            }
        }
    }

    public void RemoverBloco(Bloco bloco){
        blocos.Remove(bloco.gameObject);
        if(blocos.Count == 0){
            ResetLevel();
        }
    }
}