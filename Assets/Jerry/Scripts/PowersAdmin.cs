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
        GetComponent<AudioManager>().PlaySoundEffect(AudioManager.SOUND_EFFECT.DASH, 0.15f);
        gameObject.AddComponent<DashManager>();
        GetComponent<DashManager>().Dash(12);
    }

	void Parry()
	{
	}

	void Stun()
	{
	}

	void Chain()
	{
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
		
	}

	void Bomb()
	{
	}

	void Overlord()
	{
        GetComponent<AudioManager>().PlaySoundEffect(AudioManager.SOUND_EFFECT.LASER, 2.1f);
        OverlordPower op = Instantiate (powersPrefabs [(int)Powers.Overlord], 
										transform.position, 
										Quaternion.identity).GetComponent<OverlordPower>();
		op.Init (playerInfo);
	}
}
