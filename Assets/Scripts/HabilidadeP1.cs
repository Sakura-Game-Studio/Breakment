using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilidadeP1 : MonoBehaviour{
    public int player;
    public bool rodando;
    
    void Start(){
        player = FindObjectOfType<GameController>().getP1();
    }

    // Update is called once per frame
    void Update(){
    }
}
