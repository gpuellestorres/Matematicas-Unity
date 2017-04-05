using UnityEngine;
using System.Collections;

public class moverConMouseNumero : MonoBehaviour {
	
	public bool comenzarASeguir = false;
	public bool seguirMouse=false;
	public bool enContenedor = false;
	public bool tienePadre = false;
	public bool tieneAbuelo = false;
	public Camera camara;
	public moverConMouseNumero hijo;
	
	Vector3 tamañoInicial;
	
	Vector3 posicionInicial;
	
	// Use this for initialization
	void Start () {
		camara = FindObjectOfType (typeof(Camera)) as Camera;
		posicionInicial = transform.position;
		tamañoInicial = transform.localScale;
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
		}
		
		if (hijo != null) {
			
			hijo.tienePadre = true;
			
			float multiplicadorTamaño = 1.1f;
			
			float arregloPosicionTriangulo=0;
			
			if (hijo.hijo != null)
				multiplicadorTamaño = 2.1f;
			
			if(transform.GetChild(0).name.Equals("TRIANGULO"))
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
				hijo.transform.position = new Vector3 (
					transform.position.x, 
					transform.position.y + arregloPosicionTriangulo, 
					transform.position.z - 0.5f);
			}
		}
		else if (tienePadre) {
			float multiplicadorTamaño = 0.6f;
			transform.localScale = new Vector3 (tamañoInicial.x * multiplicadorTamaño,
			                                    tamañoInicial.y * multiplicadorTamaño,
			                                    tamañoInicial.z);
		}
		else {
			transform.localScale=tamañoInicial;
		}
		
	}
	
	void OnMouseOver()
	{
	}
	
	void OnMouseDown () {
		//if(hijo==null)seguirMouse = !seguirMouse;
	}
	
	void OnTriggerEnter2D(Collider2D coll) {
		if (enContenedor && !tieneAbuelo && hijo == null 
		    && coll.GetComponent<moverConMouseNumero>()!=null
		    && coll.GetComponent<moverConMouseNumero>().seguirMouse
		    && Input.GetMouseButton (0)) {
			if(!coll.GetComponent<moverConMouseNumero>().tienePadre)
			{
				hijo=coll.GetComponent<moverConMouseNumero>();
				hijo.tienePadre=true;
				if(tienePadre)hijo.tieneAbuelo=true;
			}
		}
	}
	
	void OnTriggerStay2D(Collider2D coll) {
		if (enContenedor && !tieneAbuelo && hijo == null 
		    && coll.GetComponent<moverConMouseNumero>()!=null
		    && coll.GetComponent<moverConMouseNumero>().seguirMouse
		    && Input.GetMouseButton (0)) {
			if(!coll.GetComponent<moverConMouseNumero>().tienePadre)
			{
				hijo=coll.GetComponent<moverConMouseNumero>();
				hijo.tienePadre=true;
				if(tienePadre)hijo.tieneAbuelo=true;
			}
		}
	}
	
	/*void OnTriggerExit2D(Collider2D coll) {
		if (coll.GetComponent<moverConMouse> () != null
		    && coll.GetComponent<moverConMouse> () == hijo
		    && Input.GetMouseButton (0)
		    && hijo.seguirMouse) {
			hijo.tienePadre=false;
			hijo.tieneAbuelo=false;
			hijo=null;
		}
	}//*/
}
