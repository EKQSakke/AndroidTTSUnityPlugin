package com.example.ttslibrary;

import android.util.Log;
import android.speech.tts.TextToSpeech;
import android.content.Context;
import android.widget.Toast;

import java.lang.CharSequence;
import java.util.Locale;
import com.unity3d.player.UnityPlayer;

public class TTSClass {

    Context context;
    TextToSpeech textToSpeech = null;
    private static TTSClass instance;
    public float Speed=1f;
    public float Pitch=1f;

    public void setContext(Context context) {
        this.context = context;
    }

    public  void TTS(final String text)
    {
        final CharSequence spokenText = text;

        textToSpeech = new TextToSpeech(context, new TextToSpeech.OnInitListener() {
            @Override
            public void onInit(int status) {
                if (status == TextToSpeech.SUCCESS) {
                    int result = textToSpeech.setLanguage(Locale.US);
                    if (result == TextToSpeech.LANG_MISSING_DATA || result == TextToSpeech.LANG_NOT_SUPPORTED) {
                        Log.d("Unity", "This language is not supported!");
                    }
                    textToSpeech.setSpeechRate(Speed);
                    textToSpeech.setPitch(Pitch);
                    textToSpeech.speak(spokenText, TextToSpeech.QUEUE_FLUSH, null, "0");
                } else {
                    Log.d("Unity", "TTS Initialization failed!");
                }
            }

        });
    }

    public void StopTTS()
    {
        if(textToSpeech != null)
        {
            textToSpeech.stop();
        }
    }

    public void ReleaseTTS()
    {
        if(textToSpeech != null)
        {
            textToSpeech.shutdown();
        }
    }

    public void SetSpeed(float speed)
    {
        Speed = speed;
    }

    public void  SetPitch(float pitch)
    {
        Pitch = pitch;
    }

    public void SetLang(String loc)
    {
        switch (loc)
        {
            case "UK":
                if(textToSpeech!=null)
                    textToSpeech.setLanguage(Locale.UK);
                break;
            case "US":
                if(textToSpeech!=null)
                    textToSpeech.setLanguage(Locale.US);
            case "EN":
                if(textToSpeech!=null)
                    textToSpeech.setLanguage(Locale.ENGLISH);
                break;
        }
    }
}
