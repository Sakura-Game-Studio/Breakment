using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logo : MonoBehaviour{
    void Start(){
        this.Wait(3.5f, () => {
            SceneManager.LoadScene("Main Menu");
        });
    }
}
