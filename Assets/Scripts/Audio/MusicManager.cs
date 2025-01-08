using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Sprite musicOnImage;
    public Sprite musicOffImage;
    public Button button;
    private bool isOn = true;
    public AudioSource audioSource;
    public AudioClip bossmusic;
    public AudioClip mainTheme;
    void Start()
    {
        musicOnImage=button.image.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClicked(){
        if (isOn){
            button.image.sprite=musicOffImage;
            isOn=false;
            audioSource.mute=true;
        }
        else{
            button.image.sprite=musicOnImage;
            isOn=true;
            audioSource.mute=false;
        }
    }

    public void changeMusic(AudioClip music){
        audioSource.Stop();
        audioSource.clip=music;
        audioSource.Play();
    }

    public void changeToBossMusic(){
        changeMusic(bossmusic);
    }

    public void changeToMain(){
        changeMusic(mainTheme);
    }
}
