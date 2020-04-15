using UnityEngine;
using System.Collections;

public abstract class Ranged : Weapon
{
    [Header("Ranged Weapons Settings")]
    [SerializeField]
    protected GameObject m_RightTriggerProjectile;
    [SerializeField]
    protected GameObject m_LeftTriggerProjectile;
    [SerializeField]
    protected GameObject[] m_FirePoint;

    public void setFirePoint(GameObject firePoint, int index)
    {
        m_FirePoint[index] = firePoint;
    }

}
