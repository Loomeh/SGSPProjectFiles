using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200002F RID: 47
public class GameManager : MonoBehaviour
{
	// Token: 0x06000098 RID: 152 RVA: 0x00004852 File Offset: 0x00002A52
	private void Awake()
	{
		GameManager.instance = this;
	}

	// Token: 0x06000099 RID: 153 RVA: 0x0000485A File Offset: 0x00002A5A
	public static GameManager Get()
	{
		return GameManager.instance;
	}

	// Token: 0x0600009A RID: 154 RVA: 0x00004864 File Offset: 0x00002A64
	private void Start()
	{
		if (ObjectiveManager.currentObjective == 6)
		{
			ObjectiveManager.currentObjective = 5;
		}
		switch (GameManager.level)
		{
		case Level.BIKINIBOTTOM:
			if (ObjectiveManager.currentObjective == 3 && GameManager.prevLevel == Level.SQUIDWARDSHOUSE)
			{
				ObjectiveManager.Get().UpdateObjective();
			}
			FadeManager.Get().PlayFadeOut(FadeManager.defaultSpeed);
			AudioManager.Get().MusicFadeIn(AudioManager.defaultMusicFadeDuration);
			return;
		case Level.SPONGEBOBSHOUSE:
			if (GameManager.prevLevel == Level.NONE)
			{
				PauseManager.Get().AssignMixerValues();
				ObjectiveManager.Get().UpdateObjective();
			}
			FadeManager.Get().PlayFadeOut(FadeManager.defaultSpeed);
			AudioManager.Get().MusicFadeIn(AudioManager.defaultMusicFadeDuration);
			return;
		case Level.SQUIDWARDSHOUSE:
			AudioManager.Get().MusicFadeIn(AudioManager.defaultMusicFadeDuration);
			FadeManager.Get().PlayFadeOut(FadeManager.defaultSpeed);
			if (ObjectiveManager.currentObjective == 1)
			{
				CutsceneManager.Get().PlayCutscene(Cutscene.SQUIDWARDENTER);
				return;
			}
			CutsceneManager.Get().cutsceneCamera.SetActive(false);
			GameObject.FindGameObjectWithTag("BloodySpongeBob").SetActive(false);
			return;
		case Level.SHADYSHOALS:
			FadeManager.Get().PlayFadeOut(FadeManager.defaultSpeed);
			AudioManager.Get().MusicFadeIn(AudioManager.defaultMusicFadeDuration);
			return;
		case Level.BANK:
			FadeManager.Get().PlayFadeOut(FadeManager.defaultSpeed);
			AudioManager.Get().MusicFadeIn(AudioManager.defaultMusicFadeDuration);
			return;
		default:
			return;
		}
	}

	// Token: 0x0600009B RID: 155 RVA: 0x000049A5 File Offset: 0x00002BA5
	public void LoadLevel(Level newLevel)
	{
		GameManager.prevLevel = GameManager.level;
		GameManager.level = newLevel;
		base.StartCoroutine(this.LoadNewLevel());
	}

	// Token: 0x0600009C RID: 156 RVA: 0x000049C4 File Offset: 0x00002BC4
	public void ReloadLevel()
	{
		base.StartCoroutine(this.LoadNewLevel());
	}

	// Token: 0x0600009D RID: 157 RVA: 0x000049D3 File Offset: 0x00002BD3
	private IEnumerator LoadNewLevel()
	{
		AudioManager.Get().MusicFadeOut(AudioManager.defaultMusicFadeDuration);
		FadeManager.Get().PlayFadeIn(FadeManager.defaultSpeed);
		while (FadeManager.Get().IsPlaying())
		{
			yield return null;
		}
		SceneManager.LoadScene(GameManager.levelToSceneTable[GameManager.level]);
		yield break;
	}

	// Token: 0x040000A3 RID: 163
	public static GameManager instance;

	// Token: 0x040000A4 RID: 164
	public static Dictionary<Level, string> levelToSceneTable = new Dictionary<Level, string>
	{
		{
			Level.BIKINIBOTTOM,
			"BikiniBottom"
		},
		{
			Level.SPONGEBOBSHOUSE,
			"SpongeBobsHouse"
		},
		{
			Level.SQUIDWARDSHOUSE,
			"SquidwardsHouse"
		},
		{
			Level.SHADYSHOALS,
			"ShadyShoals"
		},
		{
			Level.BANK,
			"Bank"
		},
		{
			Level.BOSSFIGHT,
			"MrKrabsBossFight"
		}
	};

	// Token: 0x040000A5 RID: 165
	public static Level prevLevel;

	// Token: 0x040000A6 RID: 166
	public static Level level;

	// Token: 0x040000A7 RID: 167
	public int shadyShoalsKills;

	// Token: 0x040000A8 RID: 168
	public int downtownKills;

	// Token: 0x040000A9 RID: 169
	public int bankKills;
}
