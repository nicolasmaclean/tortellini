using System.Collections;
using System.Collections.Generic;
using Gummi.Patterns;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    const string DEBUGHeader = "<b>[<color=green>Game Manager</color>]</b> ";

    public InputListener input { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        if (Instance != this) return;
        
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);

        input = GetComponent<InputListener>();
        if (input == null)
        {
            Debug.Log(DEBUGHeader + "No Input Listener found!");
        }
    }
}