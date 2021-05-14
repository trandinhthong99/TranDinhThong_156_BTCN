using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip playerAttack, playerJump, playerHurt, playerDead, playerGem, playerWin;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        playerAttack = Resources.Load<AudioClip>("Attack");
        playerJump = Resources.Load<AudioClip>("Jump");
        playerHurt = Resources.Load<AudioClip>("Hurt");
        playerDead = Resources.Load<AudioClip>("Dead");
        playerGem = Resources.Load<AudioClip>("Gem");
        playerWin = Resources.Load<AudioClip>("Victory");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "Attack":
                audioSrc.PlayOneShot(playerAttack);
                break;
            case "Jump":
                audioSrc.PlayOneShot(playerJump);
                break;
            case "Hurt":
                audioSrc.PlayOneShot(playerHurt);
                break;
            case "Dead":
                audioSrc.PlayOneShot(playerDead);
                break;
            case "Gem":
                audioSrc.PlayOneShot(playerGem);
                break;
            case "Victory":
                audioSrc.PlayOneShot(playerWin);
                break;
        }
    }
}
