using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReference : MonoBehaviour
{
    public static Transform Player { get; private set; }
    
    // Start is called before the first frame update
    private void Start()
    {
        Player = transform;
    }
}
