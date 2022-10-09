using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200002B RID: 43
public class FadeManager : MonoBehaviour
{
	// Token: 0x0600008A RID: 138 RVA: 0x000040B0 File Offset: 0x000022B0
	private void Awake()
	{
		FadeManager.instance = this;
	}

	// Token: 0x0600008B RID: 139 RVA: 0x000040B8 File Offset: 0x000022B8
	public static FadeManager Get()
	{
		return FadeManager.instance;
	}

	// Token: 0x0600008C RID: 140 RVA: 0x000040C0 File Offset: 0x000022C0
	private void Update()
	{
		if (this.playing)
		{
			if (this.fadeIn)
			{
				this.fadeScreenBlack.color = Color.Lerp(this.fadeScreenBlack.color, new Color(this.fadeScreenBlack.color.r, this.fadeScreenBlack.color.g, this.fadeScreenBlack.color.b, 1f), this.speed * Time.deltaTime);
				if (this.fadeScreenBlack.color == new Color(this.fadeScreenBlack.color.r, this.fadeScreenBlack.color.g, this.fadeScreenBlack.color.b, 1f))
				{
					this.playing = false;
					return;
				}
			}
			else
			{
				this.fadeScreenBlack.color = Color.Lerp(this.fadeScreenBlack.color, new Color(this.fadeScreenBlack.color.r, this.fadeScreenBlack.color.g, this.fadeScreenBlack.color.b, 0f), this.speed * Time.deltaTime);
				if (this.fadeScreenBlack.color == new Color(this.fadeScreenBlack.color.r, this.fadeScreenBlack.color.g, this.fadeScreenBlack.color.b, 0f))
				{
					this.playing = false;
					this.fadeScreenBlack.gameObject.SetActive(false);
				}
			}
		}
	}

	// Token: 0x0600008D RID: 141 RVA: 0x00004260 File Offset: 0x00002460
	public void PlayFadeIn(float playSpeed)
	{
		if (PauseManager.Get() != null)
		{
			PauseManager.Get().canPause = false;
		}
		this.speed = playSpeed;
		this.fadeScreenBlack.gameObject.SetActive(true);
		this.fadeScreenBlack.color = new Color(this.fadeScreenBlack.color.r, this.fadeScreenBlack.color.g, this.fadeScreenBlack.color.b, 0f);
		this.playing = true;
		this.fadeIn = true;
	}

	// Token: 0x0600008E RID: 142 RVA: 0x000042F0 File Offset: 0x000024F0
	public void PlayFadeOut(float playSpeed)
	{
		this.speed = playSpeed;
		this.fadeScreenBlack.gameObject.SetActive(true);
		this.fadeScreenBlack.color = new Color(this.fadeScreenBlack.color.r, this.fadeScreenBlack.color.g, this.fadeScreenBlack.color.b, 1f);
		this.playing = true;
		this.fadeIn = false;
	}

	// Token: 0x0600008F RID: 143 RVA: 0x00004368 File Offset: 0x00002568
	public bool IsPlaying()
	{
		return this.playing;
	}

	// Token: 0x0400007E RID: 126
	private static FadeManager instance;

	// Token: 0x0400007F RID: 127
	public Image fadeScreenBlack;

	// Token: 0x04000080 RID: 128
	private bool playing;

	// Token: 0x04000081 RID: 129
	private bool fadeIn;

	// Token: 0x04000082 RID: 130
	private float speed;

	// Token: 0x04000083 RID: 131
	public static float defaultSpeed = 5f;
}
