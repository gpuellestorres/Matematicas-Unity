using UnityEngine;
using System.Collections;

public class controlOjosRobot : MonoBehaviour {

	controlContenedorInferior contenedorInferior;
	controlMouse mouse;

	Vector3 posicionInicial;

	public Sprite 
		ojosNormales,
		ojosFracaso,
		ojosExito;

	public float minimoX=-5.7f;
	public float maximoX=5.1f;
	public float minimoY = 3.64f;
	public float maximoY = 3.99f;

	// Use this for initialization
	void Start () {
		contenedorInferior = FindObjectOfType (typeof(controlContenedorInferior)) as controlContenedorInferior;
		mouse = FindObjectOfType (typeof(controlMouse)) as controlMouse;
		GetComponent<SpriteRenderer> ().sprite = ojosNormales;

		posicionInicial = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		if (GetComponent<delayAparicion> () != null && GetComponent<delayAparicion> ().enabled)
			return;


		if (contenedorInferior.exitoInstanciado) 
		{
			GetComponent<SpriteRenderer> ().sprite = ojosExito;
		} 
		else if (contenedorInferior.fracasoInstanciado) 
		{
			GetComponent<SpriteRenderer> ().sprite = ojosFracaso;
		}
		else 
		{
			GetComponent<SpriteRenderer> ().sprite = ojosNormales;
		}

		if(mouse.objetoTomado)
		{
			Vector3 diferencia= Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
			
			Vector3 nuevaPosicion = new Vector3(
				posicionInicial.x + diferencia.x/20,
				posicionInicial.y + diferencia.y/35,
				-1);

			if(nuevaPosicion.x<minimoX)
			{
				nuevaPosicion = new Vector3(minimoX, nuevaPosicion.y,nuevaPosicion.z);
			}
			else if(nuevaPosicion.x>maximoX)
			{
				nuevaPosicion = new Vector3(maximoX, nuevaPosicion.y,nuevaPosicion.z);
			}
			if(nuevaPosicion.y<minimoY)
			{
				nuevaPosicion = new Vector3(nuevaPosicion.x, minimoY, nuevaPosicion.z);
			}
			else if(nuevaPosicion.y>maximoY)
			{
				nuevaPosicion = new Vector3(nuevaPosicion.x, maximoY, nuevaPosicion.z);
			}

			transform.position=nuevaPosicion;
		}
		else
		{
			transform.position=posicionInicial;
		}
	}
}
