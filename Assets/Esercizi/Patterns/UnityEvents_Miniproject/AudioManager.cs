using UnityEngine;
using System.Collections;

namespace TestUnityEvents
{
    public class AudioManager : MonoBehaviour
    {

        void Start()
        {
            Player pl = FindObjectOfType<Player>();

            // pl.doingStuffEvent.AddListener(PlaySfxDoingStuff);
            pl.testStringEvent.AddListener(PlaySfx);
        }

        public void PlaySfxDoingStuff()
        {
            PlaySfx("PEW");
        }

        public void PlaySfx(string s)
        {
            Debug.Log(s);
        }

    }
}