using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {
				
	public enum Locks
	{
		Movement,
		MovementControl,
		Pointing,
		Energy,
		Count
	}

	struct LockInfo
	{
		public bool locked;
		public int caller;
	}

	public struct SpeedBoost
	{
		public bool locked;
		public int caller;
		public float boost;
	}

	public float energy;
	public bool isAlive;
	public int number;
	public int lives;
	public Color color;
	public float maxSpeed;
	public PowersAdmin.MovSet movSet;
	public Vector3 currentMovementDir;
	public Vector3 currentPointerDir;
	public SpeedBoost speedBoost;
	public float minReqMoveForChargeEnergy;
	public float offsetMinenergy;
	public float scaleEnergy;
	public float gainEnergy;
	public float delayTimeCharge;
	public float delayLoseEnergy;

	LockInfo[] locks = new LockInfo[(int)Locks.Count];
	bool isVulnerable;
	//public bool isAlive;
	void Start()
	{
	
	}

	public void Init(int num, Color col, float speed) {
		movSet = PowersAdmin.MovSet.Offensive;		
		number = num;
		color = col;
		maxSpeed = speed;
		isAlive = true;

		for (int i = 0; i < (int)Locks.Count; ++i) 
		{
			locks [i].locked = false;
			locks [i].caller = -998;
		}

		speedBoost.locked = false;
		speedBoost.caller = -998;
		speedBoost.boost = 1;
	}

	public void ToggleMovSet()
	{		
		movSet = (PowersAdmin.MovSet)(((int)movSet + 1) % (int)PowersAdmin.MovSet.Count);
	}


	public bool IsLocked(Locks l)
	{
		return locks [(int)l].locked;
	}

	public void Lock(Locks l, int caller)
	{
		locks[(int)l].locked = true;
		locks[(int)l].caller = caller;
	}

	public void Unlock(Locks l, int caller)
	{
		if (caller == locks [(int)l].caller || caller == -999) 
		{
			locks [(int)l].locked = false;
			locks [(int)l].caller = -998;
		}
	}
		
	public void LockSpeedBoost (float speedBos, int caller)
	{
		speedBoost.boost = speedBos;
		speedBoost.caller = caller;
		speedBoost.locked = true;
	}

	public void UnlockSpeedBoost (int caller) 
	{
		if (caller == speedBoost.caller || caller == -999) {
			speedBoost.boost = 1;
			speedBoost.caller = -998;
			speedBoost.locked = false;
		}
	}
}

