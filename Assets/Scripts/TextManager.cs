using System;
using TMPro;
using UnityEngine;

// Token: 0x02000050 RID: 80
public class TextManager : MonoBehaviour
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x0600013D RID: 317 RVA: 0x000074CE File Offset: 0x000056CE
	// (set) Token: 0x0600013E RID: 318 RVA: 0x000074D6 File Offset: 0x000056D6
	public bool talking { get; private set; }

	// Token: 0x0600013F RID: 319 RVA: 0x000074DF File Offset: 0x000056DF
	private void Awake()
	{
		TextManager.instance = this;
	}

	// Token: 0x06000140 RID: 320 RVA: 0x000074E7 File Offset: 0x000056E7
	public static TextManager Get()
	{
		return TextManager.instance;
	}

	// Token: 0x06000141 RID: 321 RVA: 0x000074F0 File Offset: 0x000056F0
	public void Talk(string text)
	{
		PlayerMovement.instance.velocity = Vector3.zero;
		PauseManager.Get().canPause = false;
		this.talking = true;
		PlayerMovement.canControl = false;
		this.textBox.SetActive(true);
		this.spaceKeyImage.SetActive(true);
		HUDManager.Get().ineractText.gameObject.SetActive(false);
		this.textLines = text.Split(new char[]
		{
			';'
		});
		this.textIndex = 0;
		this.textUI.text = this.textLines[this.textIndex];
	}

	// Token: 0x06000142 RID: 322 RVA: 0x00007588 File Offset: 0x00005788
	private void LateUpdate()
	{
		if (this.talking && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
		{
			int num = this.textIndex + 1;
			this.textIndex = num;
			if (num == this.textLines.Length)
			{
				this.talking = false;
				PlayerMovement.canControl = true;
				PauseManager.Get().canPause = true;
				this.textBox.SetActive(false);
				return;
			}
			this.textUI.text = this.textLines[this.textIndex];
			if (this.textIndex == this.textLines.Length - 1)
			{
				this.spaceKeyImage.SetActive(false);
			}
		}
	}

	// Token: 0x04000157 RID: 343
	private static TextManager instance;

	// Token: 0x04000158 RID: 344
	public GameObject textBox;

	// Token: 0x04000159 RID: 345
	public TextMeshProUGUI textUI;

	// Token: 0x0400015A RID: 346
	public string[] textLines;

	// Token: 0x0400015B RID: 347
	public int textIndex;

	// Token: 0x0400015C RID: 348
	public GameObject spaceKeyImage;
}
