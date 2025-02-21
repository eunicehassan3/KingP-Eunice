using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    AudioSource src;
    public void GoToGame(){
        StartCoroutine(WaitForSoundAndTransition());
        SceneManager.LoadScene("MainGame");
        
    }

    private IEnumerator WaitForSoundAndTransition(){
        src = GetComponent<AudioSource>();
        src.Play();
        yield return new WaitForSeconds(src.clip.length);
        

    }

    public void GoToMenu(){
        StartCoroutine(WaitForSoundAndTransition());
        SceneManager.LoadScene("MenuScene");
    }

    public void GoToCharacterSelection(){
        StartCoroutine(WaitForSoundAndTransition());
        SceneManager.LoadScene("CharacterSelection");
    }

}
