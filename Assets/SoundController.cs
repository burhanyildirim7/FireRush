using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
	public static SoundController instance;

	private AudioSource effectSound, musicSound;

	#region Sounds
	[Header("Sounds On / Off")]
	public bool enableSounds;
	[Header("Collectible Sound Setting")]
	public AudioClip collectSound;
	public bool enableCollectSound;
	[Header("Obstacle Sound Setting")]
	public AudioClip obstacleSound;
	public bool enableObstacleSound;
	[Header("Win Effect Sound Setting")]
	public AudioClip winEffectSound;
	public bool enablewinEffectSound;
	[Header("Loose Effect Sound Setting")]
	public AudioClip looseEffectSound;
	public bool enablelooseEffectSound;
	[Header("Jump Sound Setting")]
	public AudioClip jumpSound;
	public bool enableJumpSound;
	[Header("Button Click Sound Setting")]
	public AudioClip buttonClickSound;
	public bool enablebuttonClickSound;
	[Header("Win Screen Music Setting")]
	public AudioClip winScreenMusic;
	public bool enablewinScreenMusic;
	[Header("Loose Screen Music Setting")]
	public AudioClip looseScreenMusic;
	public bool enablelooseScreenMusic;
	[Header("TapToStart Screen Music Setting")]
	public AudioClip tapToStartScreenMusic;
	public bool enabletapToStartScreenMusic;
	[Header("GamePlay Screen Music Setting")]
	public AudioClip gamePlayScreenMusic;
	public bool enablegamePlayScreenMusic;
	#endregion


	private void Awake()
	{
		if (instance == null) instance = this;
		else Destroy(this);
	}

	private void Start()
	{
		effectSound = gameObject.AddComponent<AudioSource>();
		musicSound = gameObject.AddComponent<AudioSource>();
	}

	public void PlayCollectibleSound()
	{
		if (enableCollectSound && enableSounds)
		{
			effectSound.PlayOneShot(collectSound);
		}
	}

	public void PlayObstacleSound()
	{
		if (enableObstacleSound && enableSounds)
		{
			effectSound.PlayOneShot(obstacleSound);
		}
	}

	public void PlayWinEffectSound()
	{
		if (enablewinEffectSound && enableSounds)
		{
			effectSound.PlayOneShot(winEffectSound);
		}
	}

	public void PlayLooseEffectSound()
	{
		if (enablelooseEffectSound && enableSounds)
		{
			effectSound.PlayOneShot(looseEffectSound);
		}
	}

	public void PlayJumpSound()
	{
		if (enableJumpSound && enableSounds)
		{
			effectSound.PlayOneShot(jumpSound);
		}
	}

	public void PlayButtonClickSound()
	{
		if (enablebuttonClickSound && enableSounds)
		{
			effectSound.PlayOneShot(buttonClickSound);
		}
	}

	public void PlayWinScreenMusic()
	{
		if (enablewinScreenMusic && enableSounds)
		{
			musicSound.Stop();
			musicSound.PlayOneShot(winScreenMusic);
		}
	}

	public void PlayLooseScreenMusic()
	{
		if (enablelooseScreenMusic && enableSounds)
		{
			musicSound.Stop();
			musicSound.PlayOneShot(looseScreenMusic);
		}
	}

	public void PlayTapToStartScreenMusic()
	{
		if (enabletapToStartScreenMusic && enableSounds)
		{
			musicSound.Stop();
			musicSound.PlayOneShot(tapToStartScreenMusic);
		}
	}

	public void PlayGamePlayScreenMusic()
	{
		if (enablegamePlayScreenMusic && enableSounds)
		{
			musicSound.Stop();
			musicSound.PlayOneShot(gamePlayScreenMusic);
		}
	}

	public void SoundOnOff()
	{
		if (enableSounds)
		{
			enableSounds = false;
			musicSound.Stop();
			UIManager.instance.soundButtonText.text = "SOUNDS ON";
		}
		else
		{
			enableSounds = true;
			UIManager.instance.soundButtonText.text = "SOUNDS OFF";
			musicSound.Stop();
			PlayTapToStartScreenMusic();
		}
	}
}
