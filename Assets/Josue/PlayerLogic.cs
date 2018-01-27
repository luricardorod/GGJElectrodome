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

    Vector3 Aim;

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
<<<<<<< HEAD
    private float fGravityDash = 0.5f;

=======
    private float SpeedScale = 1;
    private SphereCollider ownCollider;
    private float fHexPerSecond = (1/5.0f);
    private float fTimeInAir = 0.0f;
>>>>>>> a7b18434420a57607226cf8029e1d04972c36059
    void Start()
    {
        Live();

        m_ActiveMoveset = MOVE_SET.DEFFENSIVE;

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
            Body.position += (direction * Time.deltaTime * fEnergy * fMaxSpeed*SpeedScale);
        }
        else
        {
            fStopTime += Time.deltaTime;
            fEnergy -= fStopTime * fStopTime * fDelayLooseEnergy;
        }
        fEnergy = Mathf.Clamp(fEnergy, 0, 1);
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
        MovePlayer(LeftStick);

        if (Input.GetButtonDown("Stun/Chained"))
        {
            if (m_ActiveMoveset == MOVE_SET.OFFENSIVE)
            {
                LaunchPower(POWER.STUN);
            }
            else if (m_ActiveMoveset == MOVE_SET.DEFFENSIVE)
            {
                LaunchPower(POWER.CHAINED);
            }
        }

        if (Input.GetButtonDown("Dash/Parry"))
        {
            LaunchPower(POWER.DASH);
        }

    }

    void Update()
    {
        CheckInput(m_PlayerNumber);
        SetColor();
<<<<<<< HEAD
=======
        if (GetGround()==null)
        {
            if(fEnergy>fHexPerSecond)
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
>>>>>>> a7b18434420a57607226cf8029e1d04972c36059
    }

    void Live()
    {
        fEnergy = 0.0f;
        m_ActiveMoveset = MOVE_SET.OFFENSIVE;
        m_Active = true;
    }

    void Die()
    {
        m_Active = false;
        GameState.GlobalGameState.PlayerKilled(m_PlayerNumber);
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

