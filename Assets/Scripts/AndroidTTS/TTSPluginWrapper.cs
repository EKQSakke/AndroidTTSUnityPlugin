using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTSPluginWrapper : MonoBehaviour
{
    #region Variables
    public static TTSPluginWrapper instance;
    AndroidJavaObject unityContext = null;
    AndroidJavaObject ttsClass = null;
    #endregion

    #region Unity Functions
    // Start is called before the first frame update
    void Awake()
    {
        //Singleton
        if (instance == null)
            instance = this;
        else if(this != instance)
            Destroy(gameObject);
    }
    void Start()
    {
        if(unityContext == null)
            Initialize();

        ttsClass.Call("setContext", unityContext);
    }

    private void OnApplicationQuit()
    {
        Release();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            Stop();
    }
    #endregion

    #region Plugin Functions
    /// <summary>
    /// Setup for the plugin
    /// </summary>
    void Initialize()
    {
        if (ttsClass == null)
            ttsClass = new AndroidJavaObject("com.example.ttslibrary.TTSClass");

        if (unityContext == null)
        {
            AndroidJavaClass unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject unityActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
            unityContext = unityActivity.Call<AndroidJavaObject>("getApplicationContext");
        }
    }

    /// <summary>
    /// Set the TTS voice Pitch
    /// </summary>
    /// <param name="pitch"></param>
    public void SetPitch(float pitch)
    {
        ttsClass.Call("SetPitch", pitch);
    }
    /// <summary>
    /// Set the TTS voice Speed
    /// </summary>
    /// <param name="speed"></param>
    public void SetSpeed(float speed)
    {
        ttsClass.Call("SetSpeed", speed);
    }

    /// <summary>
    /// The TTS function that will start a new TTS
    /// </summary>
    public void TTS(string textToTTS)
    {
        if (unityContext == null)
            Initialize();

        Stop();
        ttsClass.Call("TTS", textToTTS);
    }

    /// <summary>
    /// Release the TTS service from the app
    /// </summary>
    public void Release()
    {
        ttsClass.Call("ReleaseTTS");
    }

    /// <summary>
    /// Stops the TTS
    /// </summary>
    public void Stop()
    {
        ttsClass.Call("StopTTS");
    }
    #endregion

}
