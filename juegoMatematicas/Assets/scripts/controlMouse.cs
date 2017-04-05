using UnityEngine;
using System.Collections;

public class controlMouse : MonoBehaviour {

	public bool objetoTomado=false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D[] hits = Physics2D.RaycastAll (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			
			for (int i=0; i<hits.Length; i++) {
				if (hits[i].transform.GetComponent<moverConMouse>() != null) {
					if(hits[i].transform.GetComponent<moverConMouse>().hijo==null)
					{
						hits[i].transform.GetComponent<moverConMouse>().comenzarASeguir=true;

						if(hits [i].transform.GetComponent<moverConMouse> ().esNumero &&
						   hits [i].transform.GetComponent<moverConMouse> ().hijo==null)
							hits [i].transform.GetComponent<moverConMouse> ().tienePadre = false;
					}
				}
			}
		}//*/

		if (Input.GetMouseButton (0)) {
			objetoTomado = false;
			RaycastHit2D[] hits = Physics2D.RaycastAll (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
		
			for (int i=0; i<hits.Length; i++) {
				if (hits [i].transform.GetComponent<moverConMouse> () != null) {
					if (hits [i].transform.GetComponent<moverConMouse> ().hijo == null) {
						if (hits [i].transform.GetComponent<moverConMouse> ().comenzarASeguir == true) {

							hits [i].transform.GetComponent<moverConMouse> ().seguirMouse = true;
							objetoTomado = true;
						}
						//else hits[i].transform.GetComponent<moverConMouse>().seguirMouse=false;
					}
				}
			}
		} else 
		{
			objetoTomado=false;
		}
	}
}
