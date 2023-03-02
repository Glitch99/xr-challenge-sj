using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioHandler : MonoBehaviour
{
    public AudioClip JumpFX;
    public AudioClip CrouchFX;
    public AudioClip PickupFX;
    public AudioClip StepFX;
    public AudioClip GameOverFX;
    public AudioClip MenuClick;
    public AudioClip Congrats;
    public AudioClip Splash;

    public AudioClip MenuAmbient;
    public AudioClip CreditsAmbient;
    public AudioClip IslandAmbient;
    public AudioClip GameWinAmbient;

    public AudioSource AudioPlayer;
    public AudioSource FXPlayer;

    private Scene currentScene;



    public void playBGM(int jukeboxSelection)
    {
        switch (jukeboxSelection)
        {
            case 0:
                // Main Menu
                AudioPlayer.clip = MenuAmbient;
                break;
            case 1:
                // Island
                AudioPlayer.clip = IslandAmbient;
                break;
            case 10:
                // All Pickups Collected
                AudioPlayer.clip = GameWinAmbient;
                break;
            default:
                // All Other Screens
                AudioPlayer.clip = CreditsAmbient;
                break;
        }
        AudioPlayer.Play();
    }

    public void playSoundEffect(int jukeboxSelection)
    {
        switch (jukeboxSelection)
        {
            case 0:
                // Jump
                FXPlayer.clip = JumpFX;
                break;
            case 1:
                // Crouch
                FXPlayer.clip = CrouchFX;
                break;
            case 2:
                // Pickup
                FXPlayer.clip = PickupFX;
                break;
            case 3:
                // Game Over
                FXPlayer.clip = GameOverFX;
                break;
            case 4:
                // UI Click
                FXPlayer.clip = MenuClick;
                break;
            case 5:
                // Congrats
                FXPlayer.clip = Congrats;
                break;
            case 6:
                // Water Splash
                FXPlayer.clip = Splash;
                break;
            default:
                break;
        }
        FXPlayer.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.buildIndex == 2)
        {
            playSoundEffect(3);
        } else if (currentScene.buildIndex == 3)
            {
                playSoundEffect(5);
            }
    }

    void Update() {
        if (!FXPlayer.isPlaying && !AudioPlayer.isPlaying)
        {
            playBGM(currentScene.buildIndex);
        }
    }

    public void SwitchMood()
    {
        playBGM(10);
    }
}
