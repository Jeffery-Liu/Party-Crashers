//using UnityEngine;
//using System.Collections;
//
//[RequireComponent(typeof(LineRenderer))]
//public class LaserBeam : MonoBehaviour
//{    
//    Bow bow;
//    LineRenderer m_LineRenderer;
//    Damage dmg;
//    
//    void Start()
//    {
//        dmg = GetComponent<Damage>();
//        bow = GetComponentInParent<Bow>();       
//        m_LineRenderer = GetComponent<LineRenderer>();         
//        m_LineRenderer.enabled = false;
//    }
//
//    void Update()
//    {
//        m_LineRenderer.SetWidth(bow.m_LaserWidth, bow.m_LaserWidth);
//
//        Ray ray = new Ray(transform.position, transform.forward);
//        m_LineRenderer.SetPosition(0, ray.origin);  
//
//        RaycastHit[] hits;
//        hits = Physics.RaycastAll(ray, bow.m_LaserLenght);
//        foreach(RaycastHit hit in hits)
//        {
//            if(m_LineRenderer.enabled == true)
//            {
//                if (hit.transform.GetComponent<EnemyHealth>() != null)
//                {
//                    EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();
//                    if (dmg != null)
//                    {
//                        dmg.m_Damage = bow.m_Damage * bow.m_LaserDmgMultiplier;
//                    }
//                    else
//                    {
//                        Debug.Log("Bullet doesn't have a Damage Component");
//                    }
//                    enemyHealth.Damage(dmg.m_Damage);                   
//                }
//            }            
//        }                   
//        m_LineRenderer.SetPosition(1, ray.GetPoint(bow.m_LaserLenght));
//    }
//}



