using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowersAdmin : MonoBehaviour {

	public enum MovSet {
		Defensive = 0,
		Offensive = 1,
		Count
	}

	public enum Powers {
		Dash,
		Stun,
		Slide,
		Bomb,
		Parry,
		Chain,
		Barrier,
		Overlord
	}

	public Transform[] powersPrefabs;
	PlayerInfo playerInfo;
	// Use this for initialization
	void Start () {
		playerInfo = GetComponent<PlayerInfo> ();
	}
	
	public void ExcecutePower(Powers power)
	{
		float reducedEnergyLvl = (((int)power - (int)playerInfo.movSet * 4) + 1) * 0.25f;

		if (reducedEnergyLvl <= playerInfo.energy) {
			playerInfo.energy -= reducedEnergyLvl;
			string name = System.Enum.GetName (power.GetType (), power);

            Debug.Log(name);
            Invoke (name, 0);
		}
	}

    void Dash()
    {
        gameObject.AddComponent<DashManager>();
        GetComponent<DashManager>().Dash(12);
    }

	void Parry()
	{
	}

	void Stun()
	{
		StunPower sp = Instantiate(powersPrefabs[(int)Powers.Stun], 
									playerInfo.transform.position + playerInfo.currentPointerDir * 2, 
									Quaternion.identity).GetComponent<StunPower>();
		sp.Init (playerInfo.currentPointerDir);
	}

	void Chain()
	{
		ChainPower cp = Instantiate(powersPrefabs[(int)Powers.Chain], 
									playerInfo.transform.position + playerInfo.currentPointerDir * 2, 
									Quaternion.identity).GetComponent<ChainPower>();
		cp.Init (playerInfo.currentPointerDir, playerInfo.transform.position);
	}

	void Slide()
	{
		SlidePower sp = Instantiate(powersPrefabs[(int)Powers.Slide], 
								    Vector3.zero, 
									Quaternion.identity).GetComponent<SlidePower>();
		sp.Init (transform);
	}

	void Barrier()
	{
        Vector3 barrierPos = transform.position;
        barrierPos.y -= 2.0f;

        Barrier_Script barrier = Instantiate(powersPrefabs[(int)Powers.Barrier],
                                    barrierPos + playerInfo.currentPointerDir * 3.0f,
                                    Quaternion.identity).GetComponent<Barrier_Script>();

        barrier.Init(playerInfo);
    }

	void Bomb()
	{
	}

	void Overlord()
	{
        OverlordPower op = Instantiate (powersPrefabs [(int)Powers.Overlord], 
										transform.position, 
										Quaternion.identity).GetComponent<OverlordPower>();
		op.Init (playerInfo, transform.gameObject);
	}
}
