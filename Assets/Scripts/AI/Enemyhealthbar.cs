using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemyhealthbar : MonoBehaviour {
	private EnemyHealth enemyHealthValue;
	private float EnemyHealthMax;
	[SerializeField]Image enemyhealthBar;
	[SerializeField]Canvas enemyhealthCanvas;

	// Use this for initialization
	void Start () {
		enemyHealthValue = GetComponent<EnemyHealth>();
		EnemyHealthMax = enemyHealthValue.m_EnemyHealth;
	}
	
	// Update is called once per frame
	void Update () 
	{
		enemyhealthBar.fillAmount = enemyHealthValue.m_EnemyHealth / EnemyHealthMax;
		HideBar ();
		Barcolor ();
	}

	void HideBar()
	{
		if (enemyHealthValue.m_EnemyHealth != EnemyHealthMax) 
		{
			enemyhealthCanvas.enabled = true;
		} 
		else 
		{
			enemyhealthCanvas.enabled = false;
		}	
	}

	void Barcolor()
	{
		if (enemyHealthValue.m_EnemyHealth >= EnemyHealthMax / 2) 
		{
			enemyhealthBar.color = Color.green;
		} 
		else if (enemyHealthValue.m_EnemyHealth < EnemyHealthMax / 2 && enemyHealthValue.m_EnemyHealth >= EnemyHealthMax / 4) 
		{
			enemyhealthBar.color = Color.yellow;
		} 
		else 
		{
			enemyhealthBar.color = Color.red;
		}
	}
}
