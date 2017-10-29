using UnityEngine;

public class MuteAudio : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Mute()
    {
        int mute = PlayerPrefs.GetInt("mute");
        if (mute != 1)
        {
            AudioListener.pause = true;
            PlayerPrefs.SetInt("mute", 1);
        }
        else
        {
            AudioListener.pause = false;
            PlayerPrefs.SetInt("mute", 0);
        }

    }
}
