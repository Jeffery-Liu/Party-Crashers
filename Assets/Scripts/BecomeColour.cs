using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BecomeColour : MonoBehaviour {

    public Player player;
    public Image image;

	void Update()
    {
        image.color = player.playerCurrentColor;
	}

}
