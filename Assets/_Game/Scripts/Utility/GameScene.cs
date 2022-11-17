using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Gummi.Patterns;

namespace Game.Utility
{
    [System.Serializable]
    public enum SceneIndex
    {
        Start = 0, Game = 1,
    }
    
    public class GameScene : PLazySingleton<GameScene>
    {
        public static void Load(SceneIndex index)
        {
            SceneManager.LoadScene((int) index);
        }

        public static void Load(int index) => Load((SceneIndex) index);
        public static void Load(string indexName) => Load((SceneIndex) Enum.Parse(typeof(SceneIndex), indexName));
        
        public static void Quit()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
            
            Application.Quit();
        }
    }
}