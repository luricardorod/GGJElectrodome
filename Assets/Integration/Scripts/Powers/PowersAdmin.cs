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
		Slide,
		Barrier,
		Bomb,
		Parry,
        Stun,
		Chain,
		Overlord
	}

    public float PowerCreationOffset = 2.0f;

	public GameObject StunObj;
    public GameObject ParryObj;
    public GameObject SlideObj;
    public GameObject BombObj;
    public GameObject ChainObj;
    public GameObject BarrierObj;
    public GameObject OverlordObj;

    PlayerInfo playerInfo;

	// Use this for initialization
	void Start ()
    {
		playerInfo = GetComponent<PlayerInfo> ();
	}
	
	public void ExcecutePower(Powers power)
	{
		float reducedEnergyLvl = (((int)power - (int)playerInfo.movSet * 4) + 1) * 0.25f;

		if (reducedEnergyLvl <= playerInfo.energy)
        {
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
        AudioManager.GlobalAudioManager.PlaySoundEffect(AudioManager.SOUND_EFFECT.DASH, 0.15f);

    }

    void Parry()
	{
        Vector3 barrierPos = transform.position;
        barrierPos.y -= 0.5f;

        Barrier_Script barrier = Instantiate(ParryObj,
                                    barrierPos + playerInfo.currentPointerDir * PowerCreationOffset,
                                    Quaternion.identity).GetComponent<Barrier_Script>();

        AudioManager.GlobalAudioManager.PlaySoundEffect(AudioManager.SOUND_EFFECT.PARRY, 0.5f);


        barrier.Init(playerInfo);
    }

	void Stun()
	{
		StunPower sp = Instantiate(StunObj, 
									playerInfo.transform.position + playerInfo.currentPointerDir * PowerCreationOffset, 
									Quaternion.identity).GetComponent<StunPower>();

        AudioManager.GlobalAudioManager.PlaySoundEffect(AudioManager.SOUND_EFFECT.SHOT, 0.5f);

        sp.Init (playerInfo.currentPointerDir);
	}

	void Chain()
	{
		ChainPower cp = Instantiate(ChainObj, 
									playerInfo.transform.position + playerInfo.currentPointerDir * PowerCreationOffset, 
									Quaternion.identity).GetComponent<ChainPower>();

        AudioManager.GlobalAudioManager.PlaySoundEffect(AudioManager.SOUND_EFFECT.SHOT, 0.5f);


        cp.Init (playerInfo.currentPointerDir, playerInfo.transform.position);
	}

	void Slide()
	{
		SlidePower sp = Instantiate(SlideObj, 
								    Vector3.zero, 
									Quaternion.identity).GetComponent<SlidePower>();
		sp.Init (playerInfo.transform);
	}

	void Barrier()
	{
        Vector3 barrierPos = transform.position;
        barrierPos.y -= 0.5f;

        Barrier_Script barrier = Instantiate(BarrierObj,
                                    barrierPos + playerInfo.currentPointerDir * PowerCreationOffset,
                                    Quaternion.identity).GetComponent<Barrier_Script>();

        AudioManager.GlobalAudioManager.PlaySoundEffect(AudioManager.SOUND_EFFECT.BARRIER, 0.5f);


        barrier.Init(playerInfo);
    }

	void Bomb()
	{
        Bomb bomb = Instantiate(BombObj,
                            playerInfo.transform.position + playerInfo.currentPointerDir * PowerCreationOffset,
                            Quaternion.identity).GetComponentInChildren<Bomb>();

    }

    void Overlord()
	{
        OverlordPower op = Instantiate (OverlordObj, 
										transform.position, 
										Quaternion.identity).GetComponentInChildren<OverlordPower>();

        AudioManager.GlobalAudioManager.PlaySoundEffect(AudioManager.SOUND_EFFECT.OVERLORD, 2);

        op.Init (playerInfo, transform.gameObject);
	}
}
