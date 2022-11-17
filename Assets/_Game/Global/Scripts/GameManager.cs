using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager main;
    private static string DEBUGHeader = "<b>[<color=green>Game Manager</color>]</b> ";

    [HideInInspector] public InputListener input;

    // Awake is called as soon as the script is initialized (Before Start)
    public void Awake()
    {
        if (main == null)
        { main = this; }
        else
        { Destroy(gameObject); return; }

        input = GetComponent<InputListener>();
        if (input == null)
        { Debug.Log(DEBUGHeader + "No Input Listender found!"); }
    }

    // Start is called before the first frame update
    //void Start()
    //{
    //    
    //}

    // Update is called once per frame
    //void Update()
    //{
    //    
    //}
}