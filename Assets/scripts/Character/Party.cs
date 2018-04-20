using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party : MonoBehaviour {


	public Character head;
	public List<Character> party = new List<Character>();

	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void addMember(Character character){
		if(!party.Contains(character)){
			party.Add(character);
		}
		
	}

	public void removeMember(Character character){
		if(character != head){
			party.Remove(character);
		}
		
	}

	public void removeAllMember(){
		foreach(Character member in party){
			removeMember(member);
		}
	}
}
