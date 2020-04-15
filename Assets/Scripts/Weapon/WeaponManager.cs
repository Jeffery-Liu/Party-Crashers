using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class WeaponManager : MonoBehaviour
{

    public enum EWeapon
    {
        GlowSword,
        WaterBalloonBow,        
        FizzyGun,
        Length
    }

    [HideInInspector]
    public GameObject m_CurrentWeaponObject;
    public EWeapon m_CurrentWeapon = EWeapon.GlowSword;
    [HideInInspector]
    private EWeapon m_ChangeWeapon = EWeapon.GlowSword;
    public string m_PickupConcactinateString = "_Pickup";
    public float m_DelayBetweenSwaps = 1f;

    public GameObject[] m_WeaponPrefabs;
    public GameObject[] m_WeaponPrefabPickups;
    private Dictionary<string, GameObject> m_Weapons = new Dictionary<string, GameObject>();
    private Transform m_WeaponsTransform;
    private Player m_Player;

    //PickupSound
    public int maxChance;
    public int ChanceNumber;
    public AudioClip[] BadBoySFX;
    public AudioClip[] GothSFX;
    public AudioClip[] NerdSFX;
    public AudioClip[] MascotSFX;
    public AudioClip SFXtoPlay;
    static private int Chance = 1;

    void Awake()
    {
        m_Player = GetComponent<Player>();
    }

    void Start()
    {
        //Fill up the weapons Dictionary with all the weapon prefabs and their names
        foreach (GameObject weapon in m_WeaponPrefabs)
        {
            m_Weapons.Add(weapon.gameObject.name, weapon);
        }

        if (SceneManager.GetActiveScene().name == GameManager.m_Instance.m_LevelToStart)
        {
             m_CurrentWeapon = (EWeapon)Random.Range(0, (int)EWeapon.Length);
        }

        if (GameManager.m_Instance.m_GameState == GameManager.GameState.Minigame)
        {
            m_CurrentWeapon = EWeapon.GlowSword;
        }
        initialize();
    }

    private void Update()
    {
        if(m_ChangeWeapon != m_CurrentWeapon)
        {
            SetWeapon(m_CurrentWeapon);
            m_ChangeWeapon = m_CurrentWeapon;
        }
    }

    public void initialize()
    {
        if (transform.FindChild("Model") != null) { findWeaponRecursive(transform.FindChild("Model")); }
        else { Debug.LogError("Model not found under player"); }

        SetWeapon(m_CurrentWeapon);
    }

    public void SetWeapon(EWeapon weaponPrefabName)
    {
        if (m_WeaponsTransform != null)
        {
            if (m_WeaponsTransform.FindChild(weaponPrefabName.ToString()) != null)
            {
                GameObject child = m_WeaponsTransform.FindChild(weaponPrefabName.ToString()).gameObject;
                if (m_Weapons.ContainsKey(weaponPrefabName.ToString()))
                {
                    InstantiateWeapon(m_Weapons[weaponPrefabName.ToString()], child, false);
                }
                else
                {
                    Debug.LogError("Could not find key with that Weapon name under the m_Weapons dictionary");
                }
            }
            else
            {
                Debug.LogError("Could not find GameObject with that Weapon name under player");
            }
        }
    }

    public void InstantiateWeapon(GameObject weapon, GameObject child, bool dropWeapon)
    {
        if(dropWeapon)
            DropCurrentWeapon();

        if (m_CurrentWeaponObject != null)
        {
            m_CurrentWeaponObject.GetComponent<Weapon>().terminate();
            Destroy(m_CurrentWeaponObject);
        }

        GameObject newWeapon = Instantiate(weapon, weapon.transform.position, weapon.transform.rotation) as GameObject;
        child.transform.localScale = new Vector3(1, 1, 1);
        newWeapon.transform.parent = child.gameObject.transform;
        newWeapon.transform.localPosition = new Vector3(0, 0, 0);
        newWeapon.transform.localRotation = Quaternion.identity;
        //newWeapon.transform.localScale = m_Weapons[weapon.name].transform.localScale;

        if(newWeapon.GetComponent<Ranged>() != null)
        {
            if(transform.FindChild("Firepoints").FindChild("Firepoint1") != null)
            {
                GameObject firePoint = transform.FindChild("Firepoints").FindChild("Firepoint1").gameObject;
                GameObject firePoint2 = transform.FindChild("Firepoints").FindChild("Firepoint2").gameObject;
                GameObject firePoint3 = transform.FindChild("Firepoints").FindChild("Firepoint3").gameObject;

                newWeapon.GetComponent<Ranged>().setFirePoint(firePoint, 0);
                newWeapon.GetComponent<Ranged>().setFirePoint(firePoint2, 1);
                newWeapon.GetComponent<Ranged>().setFirePoint(firePoint3, 2);
            }
            else
            {
                Debug.LogError("[WeaponManager] Firepoints/Firepoint1 not found under player");
            }
        }
        else if (newWeapon.GetComponent<Melee>() != null)
        {
            if (transform.FindChild("WeaponHitBox") != null)
            {
                GameObject weaponHitBox = transform.FindChild("WeaponHitBox").gameObject;
                weaponHitBox.GetComponent<Damage>().m_Damage = newWeapon.GetComponent<Melee>().m_Damage;
                //weaponHitBox.GetComponent<StateEffect>().m_KnockBack = newWeapon.GetComponent<StateEffect>().m_KnockBack;
                //weaponHitBox.GetComponent<StateEffect>().m_StunTime = newWeapon.GetComponent<StateEffect>().m_StunTime;

                newWeapon.GetComponent<Melee>().setSwordTrigger(weaponHitBox);
            }
            else
            {
                Debug.LogError("[WeaponManager] WeaponHitBox not found under player");
            }
        }

        newWeapon.name = child.name;
        m_CurrentWeaponObject = newWeapon;
        m_CurrentWeapon = (EWeapon) System.Enum.Parse(typeof(EWeapon), m_CurrentWeaponObject.name);
    }

    /*public void InstantiateWeapon()
    {
        DropCurrentWeapon();

        GameObject newWeapon = Instantiate(m_WeaponStandingOn, m_WeaponStandingOn.transform.position, m_WeaponStandingOn.transform.rotation) as GameObject;
        newWeapon.transform.parent = m_WeaponParent.gameObject.transform;
        newWeapon.transform.localPosition = new Vector3(0, 0, 0);
        newWeapon.transform.localRotation = Quaternion.identity;
        newWeapon.transform.localScale = new Vector3(1, 1, 1);

        newWeapon.name = m_WeaponParent.name;
        m_CurrentWeaponObject = newWeapon;
        m_CurrentWeapon = (Weapon)System.Enum.Parse(typeof(Weapon), m_CurrentWeaponObject.name);

        Destroy(m_WeaponStandingOnPickup);
    }*/

    private void DropCurrentWeapon()
    {
        if (m_CurrentWeaponObject != null)
        {
            foreach (GameObject weaponPickup in m_WeaponPrefabPickups)
            {
                if ((m_CurrentWeaponObject.gameObject.name + m_PickupConcactinateString).ToLower() == weaponPickup.gameObject.name.ToLower())
                {
                    GameObject weaponToDrop = Instantiate(weaponPickup, m_CurrentWeaponObject.transform.position, Quaternion.identity) as GameObject;
                    StartCoroutine(SetWeaponPickupable(weaponToDrop, weaponPickup.name));
                    Destroy(m_CurrentWeaponObject);

                    weaponToDrop.transform.position = transform.position;

                    break;
                }
            }
        }
    }

    IEnumerator SetWeaponPickupable(GameObject droppedItem, string correctName)
    {
        yield return new WaitForSeconds(m_DelayBetweenSwaps);
        droppedItem.name = correctName;
    }

    /*void OnTriggerStay(Collider other)
    {
        //Loop through all weapon prefabs
        foreach (GameObject weapon in m_WeaponPrefabs)
        {
            //If the name of the prefab is equal to the name of the collided object with the pickup text added
            if (weapon.gameObject.name + m_PickupConcactinateString == other.gameObject.name)
            {
                //Loop through all the child GameObjects under the Weapon gameobject in Player
                foreach (Transform child in m_WeaponsTransform)
                {
                    //If it finds a child under Weapon GameObject with the same name as the prefab, this is the Object to instantiate the Weapon Prefab under
                    if (child.gameObject.name == weapon.gameObject.name)
                    {
                        //If the player already has a weapon equipped, destroy it before instatiating the new one
                        //InstantiateWeapon(weapon, child.gameObject);
                        m_WeaponParent = child.gameObject;
                        m_WeaponStandingOn = weapon;
                        m_WeaponStandingOnPickup = other.gameObject;
                        Debug.Log("Standing on " + weapon.name);
                        GetComponent<Player>().m_CanPickUp = true;
                        break;
                    }
                }
                break;
            }
        }
    }*/

    void OnTriggerEnter(Collider other)
    {
        //Loop through all weapon prefabs
        foreach (GameObject weapon in m_WeaponPrefabs)
        {
            //If the name of the prefab is equal to the name of the collided object with the pickup text added
            if ((weapon.gameObject.name + m_PickupConcactinateString).ToLower() == other.gameObject.name.ToLower())
            {
                //Loop through all the child GameObjects under the Weapon gameobject in Player

                ChanceNumber = Random.Range(0, maxChance);
                if (ChanceNumber == Chance)
                {
                    if (m_Player.m_Model == Player.Model.Badboy)
                    {
                        SFXtoPlay = BadBoySFX[Random.Range(0, BadBoySFX.Length)];
                        AudioManager.m_Instance.PushMusic(SFXtoPlay);
                    }

                    if (m_Player.m_Model == Player.Model.Goth)
                    {
                        SFXtoPlay = GothSFX[Random.Range(0, GothSFX.Length)];
                        AudioManager.m_Instance.PushMusic(SFXtoPlay);
                    }

                    if (m_Player.m_Model == Player.Model.Nerd)
                    {
                        SFXtoPlay = NerdSFX[Random.Range(0, NerdSFX.Length)];
                        AudioManager.m_Instance.PushMusic(SFXtoPlay);
                    }

                    if (m_Player.m_Model == Player.Model.Mascot)
                    {
                        SFXtoPlay = MascotSFX[Random.Range(0, MascotSFX.Length)];
                        AudioManager.m_Instance.PushMusic(SFXtoPlay);
                    }
                }

                foreach (Transform child in m_WeaponsTransform)
                {
                    //If it finds a child under Weapon GameObject with the same name as the prefab, this is the Object to instantiate the Weapon Prefab under
                    if (child.gameObject.name == weapon.gameObject.name)
                    {
                        //If the player already has a weapon equipped, destroy it before instatiating the new one
                        InstantiateWeapon(weapon, child.gameObject, true);
                        Destroy(other.gameObject);
                        break;
                    }
                }
                break;
            }
        }
    }

    /*public void OnTriggerExit(Collider other)
    {
        if (m_WeaponStandingOn != null)
        {
            Debug.Log("Now leaving " + m_WeaponStandingOn.name + " behind.. :'(");
            GetComponent<Player>().m_CanPickUp = false;
            m_WeaponParent = null;
            m_WeaponStandingOn = null;
            m_WeaponStandingOnPickup = null;
        }
    }*/

    private void findWeaponRecursive(Transform root)
    {
        foreach (Transform child in root)
        {
            if(child.name.Equals("Weapon"))
            {
                m_WeaponsTransform = child;
                break;
            }
            findWeaponRecursive(child);
            //Debug.LogError("Weapons Transform not found under player model");
        }
    }
}
