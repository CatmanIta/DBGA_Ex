using UnityEngine;
using System.Collections;


namespace TestUnityEvents
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager scoreManager;

        void Awake()
        {
            scoreManager = this;
        }

        public void AddScore()
        {
            Debug.Log("PLING");
        }
    }
}