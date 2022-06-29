using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodadaP2 : MonoBehaviour{
    public GameObject[] rodadas;
    public int posicao = 0;

    public void AddRodada(){
        rodadas[posicao].SetActive(true);
        posicao++;
    }
}
