using UnityEngine;
using System.Collections;

public class MovingBlob : MonoBehaviour {
	
	
	public bool waitingAvatarInZone = false;
	public bool addExtraCollisionBox = false;
	
	public bool patrol = false;
	public int patrolSpeed = 75;
	
	public Transform[] patrolNodes;
	
	
	
	private Transform _AvatarT;
	private Avatar _AvatarScript;
	private ChromatoseManager _Manager;
	private MovingBlob_DetectionZone _DetectionZone;

	private int currentIndex = 0;
	private int maxIndex;
	
	void Start () {
		SetupBlob();
		SetupPatrol();
	}
	
	// Update is called once per frame
	void Update () {
		if(_DetectionZone && _DetectionZone.inZone){
			Move();
		}
	}
	
	void SetupPatrol(){
		maxIndex = patrolNodes.Length;
		
	}
	
	IEnumerator SetupBlob(){
		yield return new WaitForSeconds(0.1f);
		_AvatarT = GameObject.FindGameObjectWithTag("avatar").transform;
		_AvatarScript = GameObject.FindGameObjectWithTag("avatar").GetComponent<Avatar>();
		_Manager = ChromatoseManager.manager;
		_DetectionZone = transform.parent.gameObject.GetComponentInChildren<MovingBlob_DetectionZone>();
	}
	
	void Move(){
		if(!patrol)return;
		Vector2 traj = ((Vector2)patrolNodes[currentIndex].position - (Vector2)transform.position);
		traj = traj.magnitude > patrolSpeed * Time.deltaTime ? traj.normalized * patrolSpeed * Time.deltaTime : Vector2.zero;		//Adjust the traj!
		
		if (traj == Vector2.zero){
			currentIndex ++;
			
			if (currentIndex >= maxIndex){
				currentIndex = 0;
			}				
		}
		else{
			transform.Translate(traj, Space.World);
		}
	}
	
	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag != "avatar")return;
		
		_Manager.Death();
	}
}
