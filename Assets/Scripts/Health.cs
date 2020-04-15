using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{

    public enum Type
    {
        Simple,
        Death
    }
    public int Health_value;
    public Type type;
}

