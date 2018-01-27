using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MOVE_SET
{
    OFFENSIVE,
    DEFFENSIVE,
    MOVE_SET_MAX
}

public enum POWER
{
    BARRIER,
    STUN,
    CHAINED,
    BOMB,
    OVERLORD,
    SLIDE,
    POWER_MAX,
    DASH,
    PARRY
}



[RequireComponent(typeof(Rigidbody))]
public class PlayerLogic : MonoBehaviour
{
    public PLAYER m_PlayerNumber;
    public GameObject TrailPrefab;

	[HideInInspector]public Vector3 Aim;

    MOVE_SET m_ActiveMoveset;

    public Transform Pointer;
    public Transform Body;

    bool m_Active;

    public float groundDistance = 0.1f;
    public GameObject[] PowerPrefabs;
    public float fDashLength = 10.0f;
    public float fEnergy = 0;
    public float fMaxSpeed = 20;
    public float fScaleEnergy = 0.4f;
    public float fGainEnergy = 0.4f;
    public float fOffsetMinEnergy = 1.2f;
    public float fDelayTimeCharge = 0.5f;
    public float fDelayLooseEnergy = 0.001f;
    private float fStopTime = 0;
    private float fAngle = 0;
    private float fGravityDash = 0.5f;
	[HideInInspector]public bool lockDirection = false;
	private Vector3 currentDirection;    private float SpeedScale = 1;
    private SphereCollider ownCollider;
    private float fHexPerSecond = (1/5.0f);
    private float fTimeInAir = 0.0f;

    void Start()
    {
        Live();

        m_ActiveMoveset = MOVE_SET.OFFENSIVE;

        GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        ownCollider = GetComponentInChildren<SphereCollider>();
    }

    void MovePlayer(Vector3 direction)
    {
        float fMagnitude = direction.magnitude;
        if (fMagnitude > 0)
        {
            fStopTime = 0;
            fEnergy += Mathf.Pow((fOffsetMinEnergy - fEnergy), (fEnergy + fGainEnergy)) * (fScaleEnergy) * Time.deltaTime * fMagnitude * fDelayTimeCharge;
            if (direction != Vector3.zero)
                Body.rotation = Quaternion.LookRotation(direction);
            Body.position += (direction * Time.deltaTime * fEnergy * fMaxSpeed);
			//Save current direction
			currentDirection = direction.normalized;
			//
            Body.position += (direction * Time.deltaTime * fEnergy * fMaxSpeed);
        }
        else
        {
            fStopTime += Time.deltaTime;
            fEnergy -= fStopTime * fStopTime * fDelayLooseEnergy;
        }
        fEnergy = Mathf.Clamp(fEnergy, 0, 1);
    }

    void CheckForPowerInput(PLAYER player)
    {
        const float Level1Energy = 0.25f;
        const float Level2Energy = 0.5f;
        const float Level3Energy = 0.75f;
        const float Level4Energy = 1.00f;

        if (Input.GetButtonDown("FlipMoveSet" + (int)player))
        {
            if (m_ActiveMoveset == MOVE_SET.DEFFENSIVE) m_ActiveMoveset = MOVE_SET.OFFENSIVE;
            else
            if (m_ActiveMoveset == MOVE_SET.OFFENSIVE) m_ActiveMoveset = MOVE_SET.DEFFENSIVE;
        }

        if (Input.GetButtonDown("Stun/Slide" + (int)player) && fEnergy >= Level2Energy)
        {
            fEnergy = Mathf.Max(fEnergy - Level2Energy, 0.0f);
            if (m_ActiveMoveset == MOVE_SET.OFFENSIVE)
            {
                LaunchPower(POWER.STUN);
            }
            else if (m_ActiveMoveset == MOVE_SET.DEFFENSIVE)
            {
                LaunchPower(POWER.CHAINED);
            }
        }

        if (Input.GetButtonDown("Dash/Parry" + (int)player) && fEnergy >= Level1Energy)
        {
            fEnergy = Mathf.Max(fEnergy - Level1Energy, 0.0f);
            LaunchPower(POWER.DASH);
        }
    }

    void CheckInput(PLAYER playerInput)
    {
        Vector3 RightStick;
        RightStick.x = Input.GetAxis("RHorizontal" + (int)playerInput);
        RightStick.y = 0;
        RightStick.z = Input.GetAxis("RVertical" + (int)playerInput);

        RightStick.Normalize();
        Aim = RightStick.magnitude == 0.0f ? Aim : RightStick;

        Vector3 LeftStick;
        LeftStick.x = Input.GetAxis("LHorizontal" + (int)playerInput);
        LeftStick.y = 0;
        LeftStick.z = Input.GetAxis("LVertical" + (int)playerInput);

        LeftStick.Normalize();

        //Move the pointer.
        Pointer.LookAt(Pointer.position + RightStick);

        //Move the Player
		if (lockDirection) {
			MoveStraight ();
		} 
		else {
			MovePlayer (LeftStick);
		}

        CheckForPowerInput(playerInput);

        }    

    void Update()
    {
        if (m_Active)
        {
            CheckInput(m_PlayerNumber);
            SetColor();


            if (GetGround() == null && Body.gameObject.GetComponent<Rigidbody>().useGravity)
            {
                if (fEnergy > fHexPerSecond)
                {
                    fTimeInAir += Time.deltaTime;
                    if (fTimeInAir > fHexPerSecond)
                    {
                        SpeedScale = 0;
                    }
                }
            }
            else
            {
                fTimeInAir = 0;
            }

            if (transform.GetChild(0).position.y < -3)
            {
                float fFalling = Mathf.Clamp(7 / 170.0f * transform.GetChild(0).position.y + 191 / 170.0f, 0.3f, 1);
                transform.GetChild(0).localScale = new Vector3(fFalling, fFalling, fFalling);
                transform.GetChild(1).localScale = new Vector3(fFalling, fFalling, fFalling);
            }

        }
    }

    void Live()
    {
        fEnergy = 0.0f;
        m_ActiveMoveset = MOVE_SET.OFFENSIVE;
        m_Active = true;
    }

    public void Die()
    {
        m_Active = false;
        GameState.GlobalGameState.PlayerKilled(m_PlayerNumber);
    }

	void MoveStraight () 
	{
		Body.position += (currentDirection * Time.deltaTime * fMaxSpeed);
	}

    public void LaunchPower(POWER powerToFire)
    {
        switch (powerToFire)
        {
            case POWER.DASH:
                Body.position += (Aim * fDashLength);
                Body.gameObject.GetComponent<Rigidbody>().useGravity = false;
                StartCoroutine(GravityOff());
                break;
            case POWER.STUN:
                Instantiate<GameObject>(PowerPrefabs[0], Body.position + Aim * 2.0f, Quaternion.identity).GetComponent<Strun_script>().Direction = Aim;
                break;
            case POWER.CHAINED:
                Instantiate<GameObject>(PowerPrefabs[1], Body.position + Aim * 2.0f, Quaternion.identity).GetComponent<Chain_Script>().StartPosition = transform.position;
                break;
			case POWER.OVERLORD:
				OverlordPower op = Instantiate<GameObject> (PowerPrefabs [2], Body.position, Quaternion.identity).transform.GetChild(0).GetComponent<OverlordPower>();
				op.invokerObj = transform.GetChild(0);
				op.pl = this;
				break;
        }

    }

    IEnumerator GravityOff()
    {
        yield return new WaitForSeconds(fGravityDash);

        Body.gameObject.GetComponent<Rigidbody>().useGravity = true;
    }

    void SetColor()
    {
        var player = gameObject.GetComponent<PlayerLogic>().m_PlayerNumber;

        Debug.Log(player.ToString());
        switch (player)
        {
            case PLAYER.PLAYER_1:
                TrailPrefab.GetComponent<TrailRenderer>().materials[0].color = Color.red;
                break;
            case PLAYER.PLAYER_2:
                TrailPrefab.GetComponent<TrailRenderer>().materials[0].color = Color.blue;
                break;
            case PLAYER.PLAYER_3:
                TrailPrefab.GetComponent<TrailRenderer>().materials[0].color = Color.yellow;
                break;
            case PLAYER.PLAYER_4:
                TrailPrefab.GetComponent<TrailRenderer>().materials[0].color = Color.green;
                break;
            default:
                break;
        }
    }

    private Transform GetGround()
    {
        Vector3 feets = ownCollider.bounds.center;
        feets -= Vector3.up * (ownCollider.bounds.extents.y * 0.99f);

        RaycastHit rayInfo;

        Debug.DrawLine(feets,feets-new Vector3(0,-groundDistance,0));

        if (Physics.Raycast(feets,
                            -Vector3.up,
                            out rayInfo,
                            groundDistance))
        {
            return rayInfo.collider.transform;
        }

        return null;
    }
}

