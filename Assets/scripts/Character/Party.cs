using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party : MonoBehaviour {


	public Character head;
	public List<Character> partyList = new List<Character>();

	public static Party instance;

	public Party() {}

	private void Awake()
    {
        
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
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
