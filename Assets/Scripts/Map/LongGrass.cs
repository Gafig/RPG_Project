using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongGrass : MonoBehaviour {

	public BiomeList grassType;

	private GameManager gm;

	// Use this for initialization
	void Start () {
		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.GetComponent<PlayerMovement> ()) {
			
			//prob = x / 187.5
			float vc = 10 / 187.5f;
			float c = 8.5f / 187.5f;
			float sr = 6.75f / 187.5f;
			float r = 3.33f / 187.5f;
			float vr = 1.25f / 187.5f;

			float p = Random.Range (0.0f, 100.0f);

			Debug.Log ("p"+p);
			Debug.Log ("vc"+vc);
			Debug.Log ("c"+c);
			Debug.Log ("sr"+sr);
			Debug.Log ("r"+r);
			Debug.Log ("vr"+vr);


			if (p > 98) {
				if (gm != null)
					gm.EnterBattle (Rarity.VeryRare);
			} 
			else if (p > 93) {
				if (gm != null)
					gm.EnterBattle (Rarity.Rare);
			}
			else if (p > 85) {
				if (gm != null)
					gm.EnterBattle (Rarity.SemiRare);
			}
			else if (p > 70) {
				if (gm != null)
					gm.EnterBattle (Rarity.Common);
			}
			else {
				if (gm != null)
					gm.EnterBattle (Rarity.VeryCommon);
			}

		}
	}
}

