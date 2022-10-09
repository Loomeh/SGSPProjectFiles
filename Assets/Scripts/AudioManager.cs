using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

// Token: 0x0200001A RID: 26
public class AudioManager : MonoBehaviour
{
	// Token: 0x06000050 RID: 80 RVA: 0x00003A24 File Offset: 0x00001C24
	private void Awake()
	{
		AudioManager.instance = this;
		foreach (Sound sound in this.sounds)
		{
			sound.source = base.gameObject.AddComponent<AudioSource>();
			sound.source.clip = sound.clip;
			sound.source.volume = sound.volume;
			sound.source.pitch = sound.pitch;
			sound.source.outputAudioMixerGroup = this.soundMixerGroup;
		}
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00003AA5 File Offset: 0x00001CA5
	public static AudioManager Get()
	{
		return AudioManager.instance;
	}

	// Token: 0x06000052 RID: 82 RVA: 0x00003AAC File Offset: 0x00001CAC
	public void Play(string name)
	{
		Sound sound2 = Array.Find<Sound>(this.sounds, (Sound sound) => sound.name == name);
		if (sound2 == null)
		{
			Debug.Log("Audio source " + name + " not found.");
			return;
		}
		sound2.source.Play();
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00003B07 File Offset: 0x00001D07
	public void MaxVolume()
	{
		this.mainMixer.SetFloat("MasterVolume", 1f);
	}

	// Token: 0x06000054 RID: 84 RVA: 0x00003B1F File Offset: 0x00001D1F
	public void MusicFadeOut(float duration)
	{
		base.StartCoroutine(this.MusicFade(this.mainMixer, "MasterVolume", duration, 0f));
	}

	// Token: 0x06000055 RID: 85 RVA: 0x00003B3F File Offset: 0x00001D3F
	public void MusicFadeIn(float duration)
	{
		base.StartCoroutine(this.MusicFade(this.mainMixer, "MasterVolume", duration, 1f));
	}

	// Token: 0x06000056 RID: 86 RVA: 0x00003B5F File Offset: 0x00001D5F
	private IEnumerator MusicFade(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume)
	{
		float currentTime = 0f;
		float currentVol;
		audioMixer.GetFloat(exposedParam, out currentVol);
		currentVol = Mathf.Pow(10f, currentVol / 20f);
		float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1f);
		while (currentTime < duration)
		{
			currentTime += Time.deltaTime;
			float f = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
			audioMixer.SetFloat(exposedParam, Mathf.Log10(f) * 20f);
			yield return null;
		}
		yield break;
	}

	// Token: 0x04000055 RID: 85
	private static AudioManager instance;

	// Token: 0x04000056 RID: 86
	public static float defaultMusicFadeDuration = 0.5f;

	// Token: 0x04000057 RID: 87
	public AudioMixer mainMixer;

	// Token: 0x04000058 RID: 88
	public AudioMixerGroup soundMixerGroup;

	// Token: 0x04000059 RID: 89
	public Sound[] sounds;
}
