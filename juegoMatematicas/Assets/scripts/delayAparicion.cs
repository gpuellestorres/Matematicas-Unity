using UnityEngine;
using System.Collections;

public class delayAparicion : MonoBehaviour {

	public float segundosAparicion=4;

	public Vector3 posicionDesaparecido= new Vector3(0,0,20);
	public Vector3 posicionInicial;

	float tiempo0=1;

	bool finCiclo=false;
	bool controlarMoverConMouse=false;

	// Use this for initialization
	void Start () {
		posicionInicial = transform.position;
		if (GetComponent<moverConMouse> () != null && GetComponent<moverConMouse> ().enabled) {
			controlarMoverConMouse=true;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (finCiclo)
			GetComponent<delayAparicion> ().enabled = false;

		if (controlarMoverConMouse)
			GetComponent<moverConMouse> ().enabled = false;
	
		transform.position = posicionDesaparecido;

		if ((Time.timeSinceLevelLoad - tiempo0) > segundosAparicion) {
			transform.position=posicionInicial;
			finCiclo=true;

			if (controlarMoverConMouse)
				GetComponent<moverConMouse> ().enabled = true;
		}
	}
}
