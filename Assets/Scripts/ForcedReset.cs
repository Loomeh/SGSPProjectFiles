using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

// Token: 0x02000018 RID: 24
[RequireComponent(typeof(Image))]
public class ForcedReset : MonoBehaviour
{
	// Token: 0x0600004D RID: 77 RVA: 0x000039F0 File Offset: 0x00001BF0
	private void Update()
	{
		if (CrossPlatformInputManager.GetButtonDown("ResetObject"))
		{
			SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
		}
	}
}
