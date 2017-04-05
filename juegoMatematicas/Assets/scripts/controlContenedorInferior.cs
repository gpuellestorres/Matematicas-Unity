using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class controlContenedorInferior : MonoBehaviour {

	public List<moverConMouse>objetos;
	int cantidadHijos=0;

	public string exito="rojo + azul";
	public int cantidadMinimaObjetos=2;
	public string combinacionActual;

	public Object objetoExito;
	public Object objetoFracaso;
	Object objetoExitoInstanciado, objetoFracasoInstanciado;

	public string siguienteNivel="nivel1", nivelAnterior="nivel3";

	public bool exitoInstanciado=false;
	public bool fracasoInstanciado=false;

	controlAparicionBotones controlBotones;

	controlMouse ctrlMouse;
		
	// Use this for initialization
	void Start () {
		objetos = new List<moverConMouse> ();
		ctrlMouse = FindObjectOfType (typeof(controlMouse)) as controlMouse;
		controlBotones = FindObjectOfType (typeof(controlAparicionBotones)) as controlAparicionBotones;

	}
	
	// Update is called once per frame
	void Update () {

		//Se ordenan los objetos solo si son del tipo numero
		if (objetos.Count > 0 && objetos [0].esNumero) {
			ordenarObjetos();
		}


		cantidadHijos = 0;
		for (int i=0; i<objetos.Count; i++) {
			if (!objetos [i].seguirMouse && (!objetos [i].tienePadre || objetos [i].esNumero)) {
				cantidadHijos++;
			}
		}

		int posicion = 0;
		combinacionActual = "";
		if (cantidadHijos != 0) {
			for (int i=0; i<objetos.Count; i++) {
				if (!objetos [i].seguirMouse && (!objetos [i].tienePadre || objetos [i].esNumero)) {
					if (!objetos [i].esNumero) {
						posicion++;
						//Colocar en posicion
						objetos [i].transform.position = obtenerPosicionObjeto (posicion);

					} else if (!objetos [i].tienePadre) {
						posicion++;
						//Colocar en posicion
						objetos [i].transform.position = obtenerPosicionObjeto (posicion);

						if (objetos [i].hijo != null) {
							posicion++;
							if (objetos [i].hijo.hijo != null)
								posicion++;
						}
					}
					if(!objetos[i].tienePadre)
					{
						if (combinacionActual.Equals ("")) {
							combinacionActual += objetos [i].name;
							if (objetos [i].hijo != null) {
								combinacionActual += " (" + objetos [i].hijo.name + ")";
								if (objetos [i].hijo.hijo != null) {
									combinacionActual += " (" + objetos [i].hijo.hijo.name + ")";
								}
							}
						} else {
							combinacionActual += " + " + objetos [i].name;
							if (objetos [i].hijo != null) {
								combinacionActual += " (" + objetos [i].hijo.name + ")";
								if (objetos [i].hijo.hijo != null) {
									combinacionActual += " (" + objetos [i].hijo.hijo.name + ")";
								}
							}
						}
					}
				}
			}
			if (contarNoTomados () >= cantidadMinimaObjetos && !ctrlMouse.objetoTomado) {

				if (combinacionActual.Equals (exito)) {
					if (!exitoInstanciado)
						objetoExitoInstanciado = (Object)Instantiate (objetoExito);
					exitoInstanciado = true;
					DestroyObject (objetoFracasoInstanciado);
					fracasoInstanciado = false;
					controlBotones.aparecer=true;
				} else {
					if (!fracasoInstanciado)
						objetoFracasoInstanciado = (Object)Instantiate (objetoFracaso);
					fracasoInstanciado = true;
					DestroyObject (objetoExitoInstanciado);
					exitoInstanciado = false;
				}
			} else {
				DestroyObject (objetoExitoInstanciado);
				DestroyObject (objetoFracasoInstanciado);
				exitoInstanciado = false;
				fracasoInstanciado = false;
			}
		} else {
			DestroyObject(objetoExitoInstanciado);
			DestroyObject(objetoFracasoInstanciado);
			exitoInstanciado=false;
			fracasoInstanciado=false;
		}
	}

	int contarNoTomados(){
		int retorno = 0;
		for (int i=0; i<objetos.Count; i++) {
			if(!objetos[i].seguirMouse)
				retorno++;
		}
		return retorno;
	}

	Vector3 obtenerPosicionObjeto(int posicion){
		float division=transform.localScale.x/10/(cantidadHijos+1);
		float posX=transform.position.x - transform.localScale.x/10 /2 + division * posicion;

		return new Vector3 (posX, transform.position.y, 0.1f);
	}


	void OnTriggerEnter2D(Collider2D coll) {
		if(!objetos.Contains(coll.GetComponent<moverConMouse> ()))objetos.Add(coll.GetComponent<moverConMouse> ());
		coll.GetComponent<moverConMouse> ().enContenedor = true;

	}

	void agregarAObjetos(moverConMouse objeto){
	}

	void OnTriggerExit2D(Collider2D coll) {
		if (objetos.Contains (coll.GetComponent<moverConMouse> ()))
			objetos.Remove (coll.GetComponent<moverConMouse> ());
		if(coll.GetComponent<moverConMouse> ()!=null)coll.GetComponent<moverConMouse> ().enContenedor = false;
		if(coll.GetComponent<moverConMouseNumero> ()!=null)coll.GetComponent<moverConMouseNumero> ().enContenedor = false;
	}

	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.GetComponent<moverConMouse> ().seguirMouse) {
			ordenarObjetos();
		}
	}

	void ordenarObjetos()
	{
		moverConMouse temp;

		for (int i=0; i<objetos.Count; i++) {
			int posicionMenor=i;
			for(int j=i+1;j<objetos.Count;j++){

				if(objetos[j].transform.localPosition.x<objetos[posicionMenor].transform.localPosition.x){
					posicionMenor=j;
				}
			}

			temp=objetos[posicionMenor];
			objetos[posicionMenor]=objetos[i];
			objetos[i]=temp;
		}
	}

	public void cambiarNivel(string nivel)
	{
		print ("cargando: " + nivel);

		if(nivel.Equals("siguiente"))Application.LoadLevel (siguienteNivel);
		else if(nivel.Equals("anterior"))Application.LoadLevel (nivelAnterior);
	}
}
