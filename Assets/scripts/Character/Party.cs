using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party : MonoBehaviour {


	public Character head;
	public List<Character> partyList = new List<Character>();

	private static Party instance;

	public Party() {}

	public static Party getInstance(){
    	if (instance == null){
            instance = new Party();
        }
        
		return instance;
      
	}

	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void addMember(Character character){
		if(!partyList.Contains(character)){
			partyList.Add(character);
		}
		
	}

	public void removeMember(Character character){
		if(character != head){
			partyList.Remove(character);
		}
		
	}

	public void removeAllMember(){
		foreach(Character member in partyList){
			removeMember(member);
		}
	}
}
