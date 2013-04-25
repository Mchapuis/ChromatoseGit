using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Destructible : MonoBehaviour {
	
	protected tk2dAnimatedSprite anim;
	public int npcsToTrigger = 1;
	public Transform myNode;
	public string specificName = "";
	
	List<Npc> myNPCs = new List<Npc>();
	List<Npc> contributingNPCs;
	
	
	Collider[] children = new Collider[2];
	int curNPCs = 0;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<tk2dAnimatedSprite>();
		/*
		Object[] allNPCs = GameObject.FindObjectsOfType(typeof(Npc))
		int totalNPCs = allNPCs.Length;
		myNPCs = new Npc[totalNPCs];
		contributingNPCs = new Npc[totalNPCs];
		*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void Destroy(){
		
		children = GetComponentsInChildren<Collider>(true);
		
		foreach (Collider c in children){
			c.enabled = false;
		}
		
		anim.Play();
		
		gameObject.RemoveComponent(typeof (Destructible));
	}
	
	public bool AddOne(Npc npc){
		if (specificName != ""){
			if (npc.name != specificName)
				return false;
			Debug.Log("Same name");
		}
		myNPCs.Add(npc);
		curNPCs ++;
		
		if (curNPCs >= npcsToTrigger){
			Destroy();
		}
		return true;
	}
	
	public bool CheckName(string name){
		if (specificName == "") return true;
		bool identical = specificName == name;
		print("It is " + identical + " that it's the same name");
		return name == specificName;
	}
	
}