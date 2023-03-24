using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SairJogo : MonoBehaviour{
    public void Sair(){
        Application.Quit();
    }

    public void CarregarCena(string cena){
        SceneManager.LoadScene(cena);
    }
}
