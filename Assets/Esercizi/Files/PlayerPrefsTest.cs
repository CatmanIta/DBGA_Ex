#define MY_DEBUG

using UnityEngine;
using System.Collections;

namespace Files
{
    public class PlayerPrefsTest : MonoBehaviour
    {
        private int selectedProfileId;

        private const string SELECTED_PROFILE_ID_KEY = "selected_profile_id";

        void Start()
        {
            this.selectedProfileId = PlayerPrefs.GetInt(SELECTED_PROFILE_ID_KEY, 1);
            Debug.Log("Welcome player " + this.selectedProfileId + "!!!");

#if UNITY_IOS || UNITY_ANDROID || UNITY_STANDALONE_WIN 
            Debug.Log("I AM IN EDITOR");
#else
            Debug.Log("I AM IN BUILD");
            asdjaskdjkascasopdjiopASD
            i + i )= i
#endif
        }

        void Update()
        {
            string logString = "AAAA";
            MyLog(logString);

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                this.selectedProfileId = 1;
                Debug.Log("Switching to profile " + selectedProfileId);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                this.selectedProfileId = 2;
                Debug.Log("Switching to profile " + selectedProfileId);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                this.selectedProfileId = 3;
                Debug.Log("Switching to profile " + selectedProfileId);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                this.selectedProfileId = 4;
                Debug.Log("Switching to profile " + selectedProfileId);
            }

        }

        [System.Diagnostics.Conditional("MY_DEBUG")]
        private static void MyLog(string logString)
        {
#if DEBUG
            Debug.Log(logString);
#endif
        }

        void OnApplicationQuit()
        {
            if (PlayerPrefs.HasKey(SELECTED_PROFILE_ID_KEY))
            {
                int currentSavedPlayerProfileId = PlayerPrefs.GetInt(SELECTED_PROFILE_ID_KEY);
                if (this.selectedProfileId != currentSavedPlayerProfileId)
                {
                    PlayerPrefs.SetInt(SELECTED_PROFILE_ID_KEY, this.selectedProfileId);
                    PlayerPrefs.Save();
                }
            }
            else
            {
                PlayerPrefs.SetInt(SELECTED_PROFILE_ID_KEY, this.selectedProfileId);
                PlayerPrefs.Save();
            }
        }

    }

}