using UnityEngine;
using System.Collections;

public class moverConMouse : MonoBehaviour {

	public bool esNumero=false;

	public Transform signoMas;
	Transform signoMasInstanciado;

	public Transform signoContieneComienzo;
	Transform signoContieneComienzoInstanciado;

	public Transform signoContieneFin;
	Transform signoContieneFinInstanciado;

	public bool comenzarASeguir = false;
	public bool seguirMouse=false;
	public bool enContenedor = false;
	public bool tienePadre = false;
	public bool tieneAbuelo = false;
	public Camera camara;
	public moverConMouse hijo;

	Vector3 tamañoInicial;

	Vector3 posicionInicial;

	controlContenedorInferior contenedorInferior;
	controlMouse mouse;

	// Use this for initialization
	void Start () {
		camara = FindObjectOfType (typeof(Camera)) as Camera;
		contenedorInferior = FindObjectOfType (typeof(controlContenedorInferior)) as controlContenedorInferior;
		posicionInicial = transform.position;
		tamañoInicial = transform.localScale;

		if (esNumero) {
			signoMasInstanciado = Instantiate (signoMas).GetComponent<Transform> ();
			signoMasInstanciado.position = new Vector3 (0, 0, 20);

			signoContieneComienzoInstanciado = Instantiate (signoContieneComienzo).GetComponent<Transform> ();
			signoContieneComienzoInstanciado.position = new Vector3 (0, 0, 20);

			signoContieneFinInstanciado = Instantiate (signoContieneFin).GetComponent<Transform> ();
			signoContieneFinInstanciado.position = new Vector3 (0, 0, 20);
		}

		mouse = FindObjectOfType (typeof(controlMouse)) as controlMouse;
	}
	
	// Update is called once per frame
	void Update () {
		if (seguirMouse) {
			transform.position = camara.ScreenToWorldPoint (
				new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 1));
			transform.position = new Vector3 (transform.position.x, transform.position.y, -1.5f);

			if (!Input.GetMouseButton (0)) {
				seguirMouse=false;
				comenzarASeguir=false;
			}

		} else if(!enContenedor){
			transform.position=posicionInicial;

			if(esNumero){
				signoMasInstanciado.position = new Vector3 (0, 0, 20);
				signoContieneComienzoInstanciado.position = new Vector3 (0, 0, 20);
				signoContieneFinInstanciado.position = new Vector3 (0, 0, 20);
			}
		}

		if (hijo != null && !hijo.tienePadre) 
		{
			hijo=null;
		}

		if (hijo != null) {

			hijo.tienePadre = true;

			float multiplicadorTamaño = 1.1f;

			float arregloPosicionTriangulo=0;

			if (hijo.hijo != null)
				multiplicadorTamaño = 2.1f;

			if(transform.childCount>0 && transform.GetChild(0).name.Equals("TRIANGULO"))
			{
				arregloPosicionTriangulo=-0.3f;
				if(hijo.hijo!=null) arregloPosicionTriangulo=-0.4f;
				multiplicadorTamaño=1.4f;

				if (hijo.hijo != null)
					multiplicadorTamaño = 2.4f;
			}

			transform.localScale = new Vector3 (tamañoInicial.x * multiplicadorTamaño,
			                                    tamañoInicial.y * multiplicadorTamaño,
			                                    tamañoInicial.z);
			if (!hijo.seguirMouse){
				if(esNumero)
				{
					float division=contenedorInferior.transform.localScale.x/10/(contenedorInferior.objetos.Count+1);
					float divisionSignos=contenedorInferior.transform.localScale.x/10/(contenedorInferior.objetos.Count*2+2);

					hijo.transform.position = new Vector3 (
						transform.position.x + division, 
						transform.position.y, 
						transform.position.z);

					if(enContenedor &&
					   !seguirMouse &&
					   !mouse.objetoTomado &&
					   contenedorInferior.objetos.Count>0)
					{
						signoContieneComienzoInstanciado.position = new Vector3 (
							transform.position.x + divisionSignos, 
							transform.position.y, 
							transform.position.z);

						if(hijo.hijo==null)
						{
							signoContieneFinInstanciado.position = new Vector3 (
								transform.position.x + divisionSignos + division*3/4, 
								transform.position.y, 
								transform.position.z);
						}
						else
						{
							signoContieneFinInstanciado.position = new Vector3 (
								transform.position.x + divisionSignos + division + division/2, 
								transform.position.y, 
								transform.position.z);
						}
					}
				}
				else
				{
					hijo.transform.position = new Vector3 (
						transform.position.x, 
						transform.position.y + arregloPosicionTriangulo, 
						transform.position.z - 0.5f);
				}
			}
		}
		else if (tienePadre && !esNumero) {

			float multiplicadorTamaño = 0.6f;
			transform.localScale = new Vector3 (tamañoInicial.x * multiplicadorTamaño,
			                                    tamañoInicial.y * multiplicadorTamaño,
			                                    tamañoInicial.z);
		}
		else {

			if(esNumero){
				signoContieneComienzoInstanciado.position = new Vector3 (0, 0, 20);
				signoContieneFinInstanciado.position = new Vector3 (0, 0, 20);
				transform.localScale=tamañoInicial;
			}
			else
			{
				if(enContenedor)
				{
					float multiplicadorTamaño = 0.7f;
					transform.localScale = new Vector3 (tamañoInicial.x * multiplicadorTamaño,
					                                    tamañoInicial.y * multiplicadorTamaño,
					                                    tamañoInicial.z);
				}
				else{
					transform.localScale=tamañoInicial;
				}
			}

			if(esNumero)
			{
				float division=contenedorInferior.transform.localScale.x/10/(contenedorInferior.objetos.Count*2+2);
				if(enContenedor &&
				   !seguirMouse &&
				   !mouse.objetoTomado &&
				   contenedorInferior.objetos.Count>0 &&
				   contenedorInferior.objetos[contenedorInferior.objetos.Count-1].gameObject!=this.gameObject)
				{
					if(tienePadre){
						signoMasInstanciado.position=new Vector3(transform.position.x + division + division*1/4,
				                              transform.position.y,
				                              transform.position.z);
					}
					else
					{
						signoMasInstanciado.position=new Vector3(transform.position.x + division,
						                                         transform.position.y,
						                                         transform.position.z);
					}
				}
				else
				{
					signoMasInstanciado.position = new Vector3 (0, 0, 20);
				}
			}
		}

		if (esNumero && enContenedor) {
			float multiplicadorTamaño = 0.6f;
		
			transform.localScale = new Vector3 (tamañoInicial.x * multiplicadorTamaño,
		                                    tamañoInicial.y * multiplicadorTamaño,
		                                    tamañoInicial.z);
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (enContenedor && !tieneAbuelo && hijo == null 
		    && coll.GetComponent<moverConMouse>()!=null
		    && coll.GetComponent<moverConMouse>().seguirMouse
		    && Input.GetMouseButton (0) && !esNumero) {
			if(!coll.GetComponent<moverConMouse>().tienePadre)
			{
				hijo=coll.GetComponent<moverConMouse>();
				hijo.tienePadre=true;
				if(tienePadre)hijo.tieneAbuelo=true;
			}
		}
	}

	void OnTriggerStay2D(Collider2D coll) {
		if (enContenedor && !tieneAbuelo && hijo == null 
		    && coll.GetComponent<moverConMouse>()!=null
		    && coll.GetComponent<moverConMouse>().seguirMouse
		    && Input.GetMouseButton (0) && !esNumero) {
			if(!coll.GetComponent<moverConMouse>().tienePadre)
			{
				hijo=coll.GetComponent<moverConMouse>();
				hijo.tienePadre=true;
				if(tienePadre)hijo.tieneAbuelo=true;
			}
		}

		if (enContenedor && !tieneAbuelo && hijo == null 
		    && coll.GetComponent<moverConMouse>()!=null
		    && coll.GetComponent<moverConMouse>().seguirMouse
		    && Input.GetMouseButtonUp (0) && esNumero) {
			if(!coll.GetComponent<moverConMouse>().tienePadre)
			{
				hijo=coll.GetComponent<moverConMouse>();
				hijo.tienePadre=true;
				if(tienePadre)hijo.tieneAbuelo=true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D coll) {
		if (esNumero)
			return;

		if (coll.GetComponent<moverConMouse> () != null
			&& coll.GetComponent<moverConMouse> () == hijo
		    && Input.GetMouseButton (0)
		    && hijo.seguirMouse) {
			hijo.tienePadre=false;
			hijo.tieneAbuelo=false;
			hijo=null;
		}
	}
}
