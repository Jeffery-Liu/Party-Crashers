using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Rigidbody rb;

    Vector3 KnockBackDirection;
    Vector3 KnockBack;
    public float KnockBackSpeed = 1f;

    PlayerController playerController;

    private enum ATTACKTYPE
    {
        PRIMARY,
        SECONDARY
    }

    public enum PLAYER
    {
        P1 = 1,
        P2 = 2,
        P3 = 3,
        P4 = 4
    }

    public enum Controller
    {
        P1,
        P2,
        P3,
        P4,
        Keyboard
    }

    public enum Model
    {
        Mascot,
        Nerd,
        Badboy,
        Goth,
        Pinata
    }

    public enum State
    {
        Alive,
        Dead
    }


    // Player stats
    public string m_PlayerName;
    public PLAYER m_Player;
    public Model m_Model;
    public float m_AttackSpeed;
    public float m_MovementSpeed;
    public int m_Damage;
    public int m_HeartUpgrades;
    public int m_Gold;
    public int m_Score;
    //public int m_Health;
    //public int m_MaxHealth;
    public bool m_CantAttack;
    public bool m_CanPickUp;
    public bool m_IsDead;
    public State m_State;
    public float m_RespawnTime;
    public float m_RespawnTimeMinigame;
    public float m_CheckLocationCooldown;
    //To hold location every x seconds to respawn to
    public Vector3 m_Location;
    public Animator m_Animator;
    public GameObject m_PlayerObject;
    private Transform m_Weapon;
    private HeartSystem m_Heart;
    private CharacterController m_CharController;
    private WeaponManager m_WeaponManager;
    private RespawnHealth m_RespawnHealth;
    private PlayerController m_PlayerController;

    //Input
    public string m_PrimaryAttack = "Primary_";
    public string m_SecondaryAttack = "Secondary_";
    public string m_Interact = "Interact_";
    public string m_Silly = "Silly_";
    public string m_Stats = "Stats_";
    public string m_Pause = "Pause_";

    public float delay = 2.0f;
    public float dotdelay = 2.0f;
    public float damageDelay = 2.0f;
    public Controller m_Controller;

    //float m_LastShotTime;
    //public float DamageInterval = 2f;

    // Cooldown tracker for grabbing current location
    private float m_CurrentCooldown;

    // Interact Cooldown
    private float m_InteractWaitTime;

    //Intercat UI Image
    public Canvas interactImage;

    //VFX
    public GameObject pickUpWeaponEffect;
    //VFX
    //SFX
    public GameObject SFXPlayer;
    public AudioClip[] pickupSFX;
    public AudioClip pinataSpawn;

    private AudioClip SFXtoPlay;


    //SFX END


    //Set up color --------------------------------------------------------------------------------------------------------------------------------------------------
    [SerializeField]
    Image PlayerMarker;
    public Color playerOneColor = Color.red;
    public Color playerTwoColor = Color.blue;
    public Color playerThreeColor = Color.green;
    public Color playerFourColor = Color.yellow;
    public Color playerCurrentColor;
    public Color playerDamageIndacator = Color.white;
    public bool FlashCheck = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        m_Heart = GetComponent<HeartSystem>();
        m_CharController = GetComponent<CharacterController>();
        m_PlayerController = GetComponent<PlayerController>();
        m_WeaponManager = GetComponent<WeaponManager>();
        m_RespawnHealth = GetComponent<RespawnHealth>();

        interactImage = transform.GetChild(3).GetComponent<Canvas>();
    }

    // Use this for initialization
    void Start()
    {
        //COOPER TESTING
        //GetComponent<CharacterController>().attachedRigidbody.mass = 1000f;
        //

    }

    // Update is called once per frame
    void Update()
    {
        playerindacator();
        if (Input.GetKeyDown(KeyCode.Y))
        {
            respawn();
        }

        if (m_Heart.IsDead() && m_IsDead == false)
        {
            respawn();
        }

        if (m_CharController.isGrounded)
        {
            if (m_CurrentCooldown <= Time.time - m_CheckLocationCooldown || m_CurrentCooldown == 0)
            {
                m_Location = transform.position;

                //m_Location = m_Location  + (- vel * 5.0f);
                //Debug.Log("Location after change: " + m_Location);

                m_CurrentCooldown = Time.time;
            }
        }

        //Primary Attack
        if (Input.GetButton(m_PrimaryAttack + m_Controller.ToString()))
        {
            if (!m_CantAttack)
            {
                attack(ATTACKTYPE.PRIMARY);
            }
        }

        //Secondary Attack
        if (Input.GetButtonDown(m_SecondaryAttack + m_Controller.ToString()))
        {
            if (!m_CantAttack && GetComponent<PlayerController>().m_CantMove == false)
            {
                attack(ATTACKTYPE.SECONDARY);
            }
        }

        //Interact
        if (Input.GetButtonDown(m_Interact + m_Controller.ToString()))
        {
            /*if (m_InteractWaitTime <= 0)
            {
                if (m_WeaponManager.isStandingOnWeapon())
                {
                    m_WeaponManager.InstantiateWeapon();
                    //VfX
                    if (pickUpWeaponEffect != null)
                    {
                        GameObject getWeapon;
                        getWeapon = (GameObject)Instantiate(pickUpWeaponEffect, transform.position, transform.rotation);
                        Destroy(getWeapon, 0.5f);
                    }
                    //VFX end
                    //SFX
                    if (SFXPlayer != null)
                    {
                        AudioSource source = SFXPlayer.GetComponent<AudioSource>();
                        SFXtoPlay = pickupSFX[Random.Range(0, pickupSFX.Length)];
                        source.clip = SFXtoPlay;
                    }

                    GameObject SFXtest = Instantiate(SFXPlayer, transform.position, transform.rotation) as GameObject;
                    //SFX End
                }
                m_InteractWaitTime = 1f;
            }*/
        }

        if (m_InteractWaitTime >= 0)
        {
            m_InteractWaitTime -= Time.deltaTime;
        }

        //Interact UI Pop-up
        if (m_CanPickUp)
            interactImage.GetComponent<Canvas>().enabled = true;
        else
            interactImage.GetComponent<Canvas>().enabled = false;

        //Pause
        if (Input.GetButtonDown(m_Pause + m_Controller.ToString()))
        {
        }

        if (Input.GetButtonDown(m_Stats + m_Controller.ToString()))
        {
            //GetComponent<Stats>().ToggleWindow();
        }
    }

    void attack(ATTACKTYPE a)
    {
        //If Current weapon is not assigned
        if (m_WeaponManager.m_CurrentWeaponObject != null)
        {
            //If current weapon has no component that inherits from Weapon
            if (m_WeaponManager.m_CurrentWeaponObject.GetComponent<Weapon>() != null)
            {
                //Use primary attack
                if (a == ATTACKTYPE.PRIMARY)
                {
                    m_WeaponManager.m_CurrentWeaponObject.GetComponent<Weapon>().primaryAttack();
                }
                //Use secondary attack
                else if (a == ATTACKTYPE.SECONDARY)
                {
                    m_WeaponManager.m_CurrentWeaponObject.GetComponent<Weapon>().secondaryAttack();
                }
            }
            else
            {
                Debug.Log("Error: Current Weapon has no component that inherits from Weapon");
            }
        }
        else
        {
            //Debug.Log("Error: Current Weapon is NULL");
        }
    }

    public void respawn()
    {
        // if (GameManager.m_Instance.m_GameState == GameManager.GameState.Dungeon)
        //{
        //var vel = gameObject.GetComponent<PlayerController>().m_Velocity.normalized;
        //Vector3 tempLocation = m_Location;
        //tempLocation.x -= vel.x * 20.0f;
        //tempLocation.z -= vel.z * 20.0f;
        NavMeshHit hit;
        if(NavMesh.SamplePosition(transform.position, out hit, 1000f, -1))
        {
            transform.position = hit.position;
        }

        if(m_Score != 0)
        m_Score /= 2;
        gameObject.layer = 11;
        m_IsDead = true;
        m_State = State.Dead;
        updateModel();
        //transform.position = tempLocation;
        stun(0.1f);
        m_PlayerController.m_Velocity.y = 0;
        m_RespawnHealth.initialize(m_RespawnTime);
        //SFX Start
        if (m_State == State.Alive)
        {
            if (SFXPlayer != null)
            {
                AudioSource source = SFXPlayer.GetComponent<AudioSource>();
                SFXtoPlay = pinataSpawn;
                source.clip = SFXtoPlay;
            }
        }

        GameObject SFXtest = Instantiate(SFXPlayer, transform.position, transform.rotation) as GameObject;
        //SFX End
        //}
        //else if(GameManager.m_Instance.m_GameState == GameManager.GameState.Minigame)
        //{
        //    var vel = gameObject.GetComponent<PlayerController>().m_Velocity.normalized;
        //    Vector3 tempLocation = m_Location;
        //    tempLocation.x -= vel.x * 15.0f;
        //    tempLocation.z -= vel.z * 15.0f;

        //    m_State = State.Alive;
        //    updateModel();
        //    transform.position = tempLocation;
        //    stun(0.1f);
        //    //m_RespawnHealth.initialize();
        //}

    }

    public void respawnMinigame()
    {
        var vel = gameObject.GetComponent<PlayerController>().m_Velocity.normalized;
        //Vector3 tempLocation = m_Location;
        //tempLocation.x -= vel.x * 20.0f;
        //tempLocation.z -= vel.z * 20.0f;
        m_State = State.Dead;
        m_IsDead = true;
        updateModel();
        transform.position = new Vector3(0.0f, 0.0f);
        stun(0.2f);
        m_RespawnHealth.initialize(m_RespawnTimeMinigame);
    }

    public void updateModel()
    {
        if (m_PlayerObject != null)
        {
            if (m_PlayerObject.transform.FindChild("Model") != null)
            {
                GameObject previousModel = m_PlayerObject.transform.FindChild("Model").gameObject;
                Destroy(previousModel);
            }
            if (m_State == State.Dead)
            {
                GameObject pinataClone = Instantiate(GameManager.m_Instance.m_PinataPrefab, transform.position, Quaternion.identity) as GameObject;
                pinataClone.transform.parent = m_PlayerObject.gameObject.transform;
                pinataClone.transform.localPosition = new Vector3(0, 0, 0);
                pinataClone.transform.localRotation = GameManager.m_Instance.m_PinataPrefab.transform.rotation;
                pinataClone.transform.localScale = new Vector3(1, 1, 1);
                pinataClone.name = "Model";
                return;
            }
            else
            {
                switch (m_Model)
                {
                    case Player.Model.Mascot:
                        GameObject mascotClone = Instantiate(GameManager.m_Instance.m_MascotPrefab, transform.position, Quaternion.identity) as GameObject;
                        mascotClone.transform.parent = m_PlayerObject.gameObject.transform;
                        mascotClone.transform.localPosition = new Vector3(0, 0, 0);
                        mascotClone.transform.localRotation = Quaternion.identity;
                        mascotClone.transform.localScale = new Vector3(1, 1, 1);
                        //mascotClone.transform.FindChild("lionGeo").GetComponent<Renderer>().material.SetColor("_OutlineColor", playerOneColor);
                        mascotClone.name = "Model";
                        m_Animator = mascotClone.GetComponent<Animator>();
                        StartCoroutine(waitThenSetPlayerOutline(mascotClone, m_Model));
                        break;
                    case Player.Model.Nerd:
                        GameObject nerdClone = Instantiate(GameManager.m_Instance.m_NerdPrefab, transform.position, Quaternion.identity) as GameObject;
                        nerdClone.transform.parent = m_PlayerObject.gameObject.transform;
                        nerdClone.transform.localPosition = new Vector3(0, 0, 0);
                        nerdClone.transform.localRotation = Quaternion.identity;
                        nerdClone.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                        nerdClone.name = "Model";
                        Debug.Log("CheekO");
                        m_Animator = nerdClone.GetComponent<Animator>();
                        StartCoroutine(waitThenSetPlayerOutline(nerdClone, m_Model));
                        break;
                    case Player.Model.Badboy:
                        GameObject badBoyClone = Instantiate(GameManager.m_Instance.m_BadboyPrefab, transform.position, Quaternion.identity) as GameObject;
                        badBoyClone.transform.parent = m_PlayerObject.gameObject.transform;
                        badBoyClone.transform.localPosition = new Vector3(0, 0, 0);
                        badBoyClone.transform.localRotation = Quaternion.identity;
                        badBoyClone.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                        badBoyClone.name = "Model";
                        m_Animator = badBoyClone.GetComponent<Animator>();
                        StartCoroutine(waitThenSetPlayerOutline(badBoyClone, m_Model));
                        break;
                    case Player.Model.Goth:
                        GameObject gothClone = Instantiate(GameManager.m_Instance.m_GothPrefab, transform.position, Quaternion.identity) as GameObject;
                        gothClone.transform.parent = m_PlayerObject.gameObject.transform;
                        gothClone.transform.localPosition = new Vector3(0, 0, 0);
                        gothClone.transform.localRotation = Quaternion.identity;
                        gothClone.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                        gothClone.name = "Model";
                        m_Animator = gothClone.GetComponent<Animator>();
                        StartCoroutine(waitThenSetPlayerOutline(gothClone, m_Model));
                        break;
                        //case Player.Model.Pinata:
                        //    GameObject pinataClone = Instantiate(GameManager.m_Instance.m_PinataPrefab, transform.position, Quaternion.identity) as GameObject;
                        //    pinataClone.transform.parent = m_PlayerObject.gameObject.transform;
                        //    pinataClone.transform.localPosition = new Vector3(0, 0, 0);
                        //    pinataClone.transform.localRotation = Quaternion.identity;
                        //    pinataClone.transform.localScale = new Vector3(1, 1, 1);
                        //    StartCoroutine(waitThenSetPlayerOutline(nerdClone, m_Model));
                        //    pinataClone.name = "Model";
                        //    break;

                }
            }
        }
        else
        {
            Debug.Log("Error: Player's 'm_PlayerObject' is not assigned!");
        }
    }

    IEnumerator waitThenSetPlayerOutline(GameObject modelObject, Model model)
    {
        yield return null;

        if (modelObject != null)
        {
            if (model == Model.Mascot)
            {
                modelObject.transform.FindChild("lionGeo").GetComponent<Renderer>().material.SetColor("_OutlineColor", playerCurrentColor);
            }
            else if (model != Model.Pinata)
            {
                modelObject.transform.FindChild("Human_Head").GetComponent<Renderer>().material.SetColor("_OutlineColor", playerCurrentColor);
                modelObject.transform.FindChild("Human_Body").GetComponent<Renderer>().material.SetColor("_OutlineColor", playerCurrentColor);
            }
        }
    }

    public void stun(float secs)
    {
        GetComponent<PlayerController>().m_CantMove = true;
        StartCoroutine(StunForSec(secs));
    }

    public void damage(int damageAmount)
    {
        m_Heart.TakeDamage(damageAmount);
    }

    public void heal(int healAmount)
    {
        m_Heart.Heal(healAmount);
    }

    public string getControllerAsString()
    {
        return m_Controller.ToString();
    }

    public Controller getController()
    {
        return m_Controller;
    }

    public void setController(Controller controller)
    {
        m_Controller = controller;
    }

    public void save()
    {
        switch (m_Player)
        {
            case PLAYER.P1:
                GameManager.m_Instance.m_Player1.name = m_PlayerName;
                GameManager.m_Instance.m_Player1.player = m_Player;
                GameManager.m_Instance.m_Player1.model = m_Model;
                GameManager.m_Instance.m_Player1.weapon = m_WeaponManager.m_CurrentWeapon;
                GameManager.m_Instance.m_Player1.attackSpeed = m_AttackSpeed;
                GameManager.m_Instance.m_Player1.movementSpeed = m_MovementSpeed;
                GameManager.m_Instance.m_Player1.damage = m_Damage;
                GameManager.m_Instance.m_Player1.heartUpgrades = m_HeartUpgrades;
                GameManager.m_Instance.m_Player1.score = m_Score;
                GameManager.m_Instance.m_Player1.gold = m_Gold;
                //GameManager.m_Instance.m_Player1.health = m_Health;
                //GameManager.m_Instance.m_Player1.maxHealth = m_MaxHealth;
                //GameManager.m_Instance.m_Player1.m_Controller = m_Controller;
                break;
            case PLAYER.P2:
                GameManager.m_Instance.m_Player2.name = m_PlayerName;
                GameManager.m_Instance.m_Player2.player = m_Player;
                GameManager.m_Instance.m_Player2.model = m_Model;
                GameManager.m_Instance.m_Player2.weapon = m_WeaponManager.m_CurrentWeapon;
                GameManager.m_Instance.m_Player2.attackSpeed = m_AttackSpeed;
                GameManager.m_Instance.m_Player2.movementSpeed = m_MovementSpeed;
                GameManager.m_Instance.m_Player2.damage = m_Damage;
                GameManager.m_Instance.m_Player2.heartUpgrades = m_HeartUpgrades;
                GameManager.m_Instance.m_Player2.score = m_Score;
                GameManager.m_Instance.m_Player2.gold = m_Gold;
                //GameManager.m_Instance.m_Player2.health = m_Health;
                //GameManager.m_Instance.m_Player2.maxHealth = m_MaxHealth;
                //GameManager.m_Instance.m_Player2.m_Controller = m_Controller;
                break;
            case PLAYER.P3:
                GameManager.m_Instance.m_Player3.name = m_PlayerName;
                GameManager.m_Instance.m_Player3.player = m_Player;
                GameManager.m_Instance.m_Player3.model = m_Model;
                GameManager.m_Instance.m_Player3.weapon = m_WeaponManager.m_CurrentWeapon;
                GameManager.m_Instance.m_Player3.attackSpeed = m_AttackSpeed;
                GameManager.m_Instance.m_Player3.movementSpeed = m_MovementSpeed;
                GameManager.m_Instance.m_Player3.damage = m_Damage;
                GameManager.m_Instance.m_Player3.heartUpgrades = m_HeartUpgrades;
                GameManager.m_Instance.m_Player3.score = m_Score;
                GameManager.m_Instance.m_Player3.gold = m_Gold;
                //GameManager.m_Instance.m_Player3.health = m_Health;
                //GameManager.m_Instance.m_Player3.maxHealth = m_MaxHealth;
                //GameManager.m_Instance.m_Player3.m_Controller = m_Controller;
                break;
            case PLAYER.P4:
                GameManager.m_Instance.m_Player4.name = m_PlayerName;
                GameManager.m_Instance.m_Player4.player = m_Player;
                GameManager.m_Instance.m_Player4.model = m_Model;
                GameManager.m_Instance.m_Player4.weapon = m_WeaponManager.m_CurrentWeapon;
                GameManager.m_Instance.m_Player4.attackSpeed = m_AttackSpeed;
                GameManager.m_Instance.m_Player4.movementSpeed = m_MovementSpeed;
                GameManager.m_Instance.m_Player4.damage = m_Damage;
                GameManager.m_Instance.m_Player4.heartUpgrades = m_HeartUpgrades;
                GameManager.m_Instance.m_Player4.score = m_Score;
                GameManager.m_Instance.m_Player4.gold = m_Gold;
                //GameManager.m_Instance.m_Player4.health = m_Health;
                //GameManager.m_Instance.m_Player4.maxHealth = m_MaxHealth;
                //GameManager.m_Instance.m_Player4.m_Controller = m_Controller;
                break;
        }
    }

    public void saveMain()
    {
        switch (m_Player)
        {
            case PLAYER.P1:
                GameManager.m_Instance.m_Player1.player = m_Player;
                //GameManager.m_Instance.m_Player1.model = m_Model;
                break;
            case PLAYER.P2:
                GameManager.m_Instance.m_Player2.player = m_Player;
                //GameManager.m_Instance.m_Player2.model = m_Model;
                break;
            case PLAYER.P3:
                GameManager.m_Instance.m_Player3.player = m_Player;
                //GameManager.m_Instance.m_Player3.model = m_Model;
                break;
            case PLAYER.P4:
                GameManager.m_Instance.m_Player4.player = m_Player;
                //GameManager.m_Instance.m_Player4.model = m_Model;
                break;
        }
    }

    public void load()
    {
        switch (m_Player)
        {
            case PLAYER.P1:
                m_PlayerName = GameManager.m_Instance.m_Player1.name;
                m_Player = GameManager.m_Instance.m_Player1.player;
                m_Model = GameManager.m_Instance.m_Player1.model;
                m_WeaponManager.m_CurrentWeapon = GameManager.m_Instance.m_Player1.weapon;
                m_AttackSpeed = GameManager.m_Instance.m_Player1.attackSpeed;
                m_MovementSpeed = GameManager.m_Instance.m_Player1.movementSpeed;
                m_Damage = GameManager.m_Instance.m_Player1.damage;
                m_HeartUpgrades = GameManager.m_Instance.m_Player1.heartUpgrades;
                m_Score = GameManager.m_Instance.m_Player1.score;
                m_Gold = GameManager.m_Instance.m_Player1.gold;
                //m_Health = GameManager.m_Instance.m_Player1.health;
                //m_MaxHealth = GameManager.m_Instance.m_Player1.maxHealth;
                m_Controller = GameManager.m_Instance.m_Player1.m_Controller;
                break;
            case PLAYER.P2:
                m_PlayerName = GameManager.m_Instance.m_Player2.name;
                m_Player = GameManager.m_Instance.m_Player2.player;
                m_Model = GameManager.m_Instance.m_Player2.model;
                m_WeaponManager.m_CurrentWeapon = GameManager.m_Instance.m_Player2.weapon;
                m_AttackSpeed = GameManager.m_Instance.m_Player2.attackSpeed;
                m_MovementSpeed = GameManager.m_Instance.m_Player2.movementSpeed;
                m_Damage = GameManager.m_Instance.m_Player2.damage;
                m_HeartUpgrades = GameManager.m_Instance.m_Player2.heartUpgrades;
                m_Score = GameManager.m_Instance.m_Player2.score;
                m_Gold = GameManager.m_Instance.m_Player2.gold;
                //m_Health = GameManager.m_Instance.m_Player2.health;
                //m_MaxHealth = GameManager.m_Instance.m_Player2.maxHealth;
                m_Controller = GameManager.m_Instance.m_Player2.m_Controller;
                break;
            case PLAYER.P3:
                m_PlayerName = GameManager.m_Instance.m_Player3.name;
                m_Player = GameManager.m_Instance.m_Player3.player;
                m_Model = GameManager.m_Instance.m_Player3.model;
                m_WeaponManager.m_CurrentWeapon = GameManager.m_Instance.m_Player3.weapon;
                m_AttackSpeed = GameManager.m_Instance.m_Player3.attackSpeed;
                m_MovementSpeed = GameManager.m_Instance.m_Player3.movementSpeed;
                m_Damage = GameManager.m_Instance.m_Player3.damage;
                m_HeartUpgrades = GameManager.m_Instance.m_Player3.heartUpgrades;
                m_Score = GameManager.m_Instance.m_Player3.score;
                m_Gold = GameManager.m_Instance.m_Player3.gold;
                //m_Health = GameManager.m_Instance.m_Player3.health;
                //m_MaxHealth = GameManager.m_Instance.m_Player3.maxHealth;
                m_Controller = GameManager.m_Instance.m_Player3.m_Controller;
                break;
            case PLAYER.P4:
                m_PlayerName = GameManager.m_Instance.m_Player4.name;
                m_Player = GameManager.m_Instance.m_Player4.player;
                m_Model = GameManager.m_Instance.m_Player4.model;
                m_WeaponManager.m_CurrentWeapon = GameManager.m_Instance.m_Player4.weapon;
                m_AttackSpeed = GameManager.m_Instance.m_Player4.attackSpeed;
                m_MovementSpeed = GameManager.m_Instance.m_Player4.movementSpeed;
                m_Damage = GameManager.m_Instance.m_Player4.damage;
                m_HeartUpgrades = GameManager.m_Instance.m_Player4.heartUpgrades;
                m_Score = GameManager.m_Instance.m_Player4.score;
                m_Gold = GameManager.m_Instance.m_Player4.gold;
                //m_Health = GameManager.m_Instance.m_Player4.health;
                //m_MaxHealth = GameManager.m_Instance.m_Player4.maxHealth;
                m_Controller = GameManager.m_Instance.m_Player4.m_Controller;
                break;
        }
    }

    public void SetPlayer(PLAYER p)
    {
        m_Player = p;
    }

    void OnCollisionEnter(Collision other)
    {
        // Bullet, Projectile using this way do deal damage
        /*if (other.gameObject.CompareTag("OneDamage"))
        {
            m_Heart.TakeDamage(1);
            m_Heart.UpdateHearts();
        }*/
        //if (other.gameObject.CompareTag("TwoDamage"))
        //{
        // m_Heart.TakeDamage(2);
        //m_Heart.UpdateHearts();
        //}
        //if (other.gameObject.CompareTag("MeleeEnemy"))
        //{
        //    rb.AddForce(transform.forward * 500);
        //}
    }

    //void OnCollisionStay(Collision other)
    //{
    //    if (other.gameObject.CompareTag("MeleeEnemy"))
    //    {
    //        m_Heart.lastDamage += Time.deltaTime;
    //        if (m_Heart.lastDamage >= 2)
    //        {
    //            //    KnockBackDirection = transform.position - other.transform.position;
    //            //KnockBack = transform.position + KnockBackDirection;
    //            //transform.position = Vector3.Lerp(transform.position, KnockBack, KnockBackSpeed);
    //            m_Heart.TakeDamage(1);
    //            m_Heart.UpdateHearts();
    //            m_Heart.lastDamage = 0;
    //        }
    //    }
    //}

    void OnTriggerStay(Collider other)
    {
       /* if (other.gameObject.CompareTag("DotTrap"))
        {
            dotdelay -= Time.deltaTime;
            if (dotdelay <= 0)
            {
                m_Heart.TakeDamage(1);
                m_Heart.UpdateHearts();
                dotdelay = 2.0f;
            }
        }
        if (other.gameObject.CompareTag("OneDamage"))
        {
            Destroy(other);
            m_Heart.TakeDamage(1);
            m_Heart.UpdateHearts();
        }*/
        //if (other.gameObject.CompareTag("MeleeEnemy"))
        //{
        //    damageDelay -= Time.deltaTime;
        //    if (damageDelay <= 0)
        //    {
        //        m_Heart.TakeDamage(1);
        //        m_Heart.UpdateHearts();
        //        damageDelay = 2.0f;
        //    }
        //    m_Heart.TakeDamage(1);
        //    m_Heart.UpdateHearts();
        //    // Add timer

        //}
    }


    /*public void OnTriggerStay(Collider other)
    {
        Health health = other.GetComponent<Health>();

        if (other.tag == "Health")
        {
            if (m_Health <= 90)
            {
                m_Health = m_Health + health.Health_value;
            }
            else if (m_Health > 90 && m_Health < m_MaxHealth)
            {
                m_Health = m_MaxHealth;
            }
            else // FizzPop
            {

            }
            other.gameObject.SetActive(false);
        }
        //else if (other.tag == "Coins")
        //{
        //    m_Score
        //}
    }*/

    IEnumerator StunForSec(float s)
    {
        yield return new WaitForSeconds(s);
        GetComponent<PlayerController>().m_CantMove = false;
    }


    //Health Indicator Code --------------------------------------------------------------------------------
    void playerindacator()
    {
        if (m_Player == PLAYER.P1)
        {
            playerCurrentColor = playerOneColor;
            if (m_Heart.curHealth <= m_Heart.maxHealth / 2 && m_Heart.curHealth > m_Heart.maxHealth / 4)
            {
                PlayerMarker.color = Color.Lerp(playerOneColor, playerDamageIndacator, Mathf.PingPong(Time.time, 0.9f));
            }
            else if (m_Heart.curHealth <= m_Heart.maxHealth / 4)
            {
                if (FlashCheck == true)
                {
                    FlashCheck = false;
                    StartCoroutine(Flash());
                }
            }
            else
            {
                PlayerMarker.color = playerCurrentColor;
            }
        }
        if (m_Player == PLAYER.P2)
        {
            playerCurrentColor = playerTwoColor;
            if (m_Heart.curHealth <= m_Heart.maxHealth / 2 && m_Heart.curHealth > m_Heart.maxHealth / 4)
            {
                PlayerMarker.color = Color.Lerp(playerTwoColor, playerDamageIndacator, Mathf.PingPong(Time.time, 0.9f));
            }
            else if (m_Heart.curHealth <= m_Heart.maxHealth / 4)
            {
                if (FlashCheck == true)
                {
                    FlashCheck = false;
                    StartCoroutine(Flash());
                }
            }
            else
            {
                PlayerMarker.color = playerCurrentColor;
            }
        }
        if (m_Player == PLAYER.P3)
        {
            playerCurrentColor = playerThreeColor;
            if (m_Heart.curHealth <= m_Heart.maxHealth / 2 && m_Heart.curHealth > m_Heart.maxHealth / 4)
            {
                PlayerMarker.color = Color.Lerp(playerThreeColor, playerDamageIndacator, Mathf.PingPong(Time.time, 0.9f));
            }
            else if (m_Heart.curHealth <= m_Heart.maxHealth / 4)
            {
                if (FlashCheck == true)
                {
                    FlashCheck = false;
                    StartCoroutine(Flash());
                }
            }
            else
            {
                PlayerMarker.color = playerCurrentColor;
            }
        }
        if (m_Player == PLAYER.P4)
        {
            playerCurrentColor = playerFourColor;
            if (m_Heart.curHealth <= m_Heart.maxHealth / 2 && m_Heart.curHealth > m_Heart.maxHealth / 4)
            {
                PlayerMarker.color = Color.Lerp(playerFourColor, playerDamageIndacator, Mathf.PingPong(Time.time, 0.9f));
            }
            else if (m_Heart.curHealth <= m_Heart.maxHealth / 4)
            {
                if (FlashCheck == true)
                {
                    FlashCheck = false;
                    StartCoroutine(Flash());
                }
            }
            else
            {
                PlayerMarker.color = playerCurrentColor;
            }
        }
    }
    IEnumerator Flash()
    {
        PlayerMarker.color = playerDamageIndacator;
        yield return new WaitForSeconds(.1f);
        PlayerMarker.color = playerCurrentColor;
        yield return new WaitForSeconds(.1f);
        FlashCheck = true;
    }
}
