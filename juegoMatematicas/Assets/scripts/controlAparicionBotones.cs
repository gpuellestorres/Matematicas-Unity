using UnityEngine;
using System.Collections;

public class controlAparicionBotones : MonoBehaviour {

	public bool aparecer=false;

	public Vector3 posicionDesaparecido = new Vector3 (0, 0, 20);
	public Vector3 posicionInicial;
	// Use this for initialization
	void Start () {
	
		posicionInicial = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (aparecer) 
		{
			transform.position=posicionInicial;
		}
		else 
		{
			transform.position=posicionDesaparecido;
		}
	}
}
