using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cambioNivel : MonoBehaviour {

    Scene[] scenes;

    // Use this for initialization
    void Start () {
        scenes = SceneManager.GetAllScenes();
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void cambiarNivel()
    {
        string nivelActual = SceneManager.GetActiveScene().name;

        string nuevoNivel = nivelActual.Substring(nivelActual.Length - 1, 1);

        print(nuevoNivel);

        nuevoNivel = (int.Parse(nuevoNivel) + 1) + "";

        nuevoNivel = nivelActual.Substring(0,nivelActual.Length-1) + nuevoNivel;

        print("cargando: " + nuevoNivel);

        if (Application.CanStreamedLevelBeLoaded(nuevoNivel))
        {
            SceneManager.LoadSceneAsync(nuevoNivel);
        }
        else
        {
            nuevoNivel = nivelActual.Substring(nivelActual.Length - 2, 1);

            print(nuevoNivel);

            nuevoNivel = (char.Parse(nuevoNivel) + 1) + "";

            nuevoNivel = nivelActual.Substring(0, nivelActual.Length - 2) + nuevoNivel + "1";

            if (Application.CanStreamedLevelBeLoaded(nuevoNivel))
            {
                SceneManager.LoadScene(nuevoNivel);
            }
            else
            {
                SceneManager.LoadScene("NivelA0");
            }
        }
    }

    private bool existeEscena(string nuevoNivel)
    {
        print("Imprimir escenas. Buscando nivel: " + nuevoNivel);
        foreach (Scene scene in scenes)
        {
            print(scene.path);
            //if (scene.path.Contains(nuevoNivel)) return true;
        }
        return false;
    }
}
