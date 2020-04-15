using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour {
    [Header("All Weapons")]
    [SerializeField]
    protected Animation m_Anim;
    [SerializeField]
    public int m_Damage = 0;
    [SerializeField]
    protected float m_Weapon1Cooldown = 0f;
    [SerializeField]
    protected float m_Weapon2Cooldown = 0f;
    [SerializeField]
    protected AudioClip[] m_PrimarySounds;
    [SerializeField]
    protected AudioClip[] m_SecondarySounds;
    [SerializeField]
    protected AudioClip[] m_SoundsOnHit;

    protected float m_CoolDown;
    protected float m_SecondaryCoolDown;
    
    public abstract void primaryAttack();
    public abstract void secondaryAttack();

    public abstract void terminate();
}
