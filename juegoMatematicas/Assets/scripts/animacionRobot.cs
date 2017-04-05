using UnityEngine;
using System.Collections;

public class animacionRobot : MonoBehaviour {

	public Sprite[] imagenes;

	public float tiempoCambioImagen=0.1f;

	float tiempo0,tiempo1;

	int posImagen=0;

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().sprite = imagenes [posImagen];

		tiempo0 = Time.timeSinceLevelLoad;
	}
	
	// Update is called once per frame
	void Update () {

		tiempo1 = Time.timeSinceLevelLoad;

		if ((tiempo1 - tiempo0) > tiempoCambioImagen) {

			if(posImagen<imagenes.Length-1)posImagen++;

			GetComponent<SpriteRenderer> ().sprite = imagenes [posImagen];

			tiempo0=tiempo1;
		}
	
	}
}
