using System;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class FPSDemoGUI : MonoBehaviour
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	private void Start()
	{
		if (Screen.dpi < 1f)
		{
			this.dpiScale = 1f;
		}
		if (Screen.dpi < 200f)
		{
			this.dpiScale = 1f;
		}
		else
		{
			this.dpiScale = Screen.dpi / 200f;
		}
		this.guiStyleHeader.fontSize = (int)(15f * this.dpiScale);
		this.guiStyleHeader.normal.textColor = new Color(0.15f, 0.15f, 0.15f);
		this.currentInstance = Object.Instantiate<GameObject>(this.Prefabs[this.currentNomber], base.transform.position, base.transform.rotation);
		this.currentInstance.AddComponent<FPSDemoReactivator>().TimeDelayToReactivate = this.reactivateTime;
		this.sunIntensity = this.Sun.intensity;
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00002130 File Offset: 0x00000330
	private void OnGUI()
	{
		if (GUI.Button(new Rect(10f * this.dpiScale, 15f * this.dpiScale, 135f * this.dpiScale, 37f * this.dpiScale), "PREVIOUS EFFECT"))
		{
			this.ChangeCurrent(-1);
		}
		if (GUI.Button(new Rect(160f * this.dpiScale, 15f * this.dpiScale, 135f * this.dpiScale, 37f * this.dpiScale), "NEXT EFFECT"))
		{
			this.ChangeCurrent(1);
		}
		this.sunIntensity = GUI.HorizontalSlider(new Rect(10f * this.dpiScale, 70f * this.dpiScale, 285f * this.dpiScale, 15f * this.dpiScale), this.sunIntensity, 0f, 0.6f);
		this.Sun.intensity = this.sunIntensity;
		GUI.Label(new Rect(300f * this.dpiScale, 70f * this.dpiScale, 30f * this.dpiScale, 30f * this.dpiScale), "SUN INTENSITY", this.guiStyleHeader);
		GUI.Label(new Rect(400f * this.dpiScale, 15f * this.dpiScale, 100f * this.dpiScale, 20f * this.dpiScale), "Prefab name is \"" + this.Prefabs[this.currentNomber].name + "\"  \r\nHold any mouse button that would move the camera", this.guiStyleHeader);
	}

	// Token: 0x06000003 RID: 3 RVA: 0x000022D4 File Offset: 0x000004D4
	private void ChangeCurrent(int delta)
	{
		this.currentNomber += delta;
		if (this.currentNomber > this.Prefabs.Length - 1)
		{
			this.currentNomber = 0;
		}
		else if (this.currentNomber < 0)
		{
			this.currentNomber = this.Prefabs.Length - 1;
		}
		if (this.currentInstance != null)
		{
			Object.Destroy(this.currentInstance);
		}
		if (this.currentNomber < 10)
		{
			this.currentInstance = Object.Instantiate<GameObject>(this.Prefabs[this.currentNomber], base.transform.position, base.transform.rotation);
			this.Gun.SetActive(false);
		}
		else
		{
			this.currentInstance = Object.Instantiate<GameObject>(this.Prefabs[this.currentNomber], this.muzzleFlashPoint.position, this.muzzleFlashPoint.rotation);
			this.Gun.SetActive(true);
		}
		this.currentInstance.AddComponent<FPSDemoReactivator>().TimeDelayToReactivate = this.reactivateTime;
	}

	// Token: 0x04000001 RID: 1
	public GameObject[] Prefabs;

	// Token: 0x04000002 RID: 2
	public Transform muzzleFlashPoint;

	// Token: 0x04000003 RID: 3
	public GameObject Gun;

	// Token: 0x04000004 RID: 4
	public float reactivateTime = 4f;

	// Token: 0x04000005 RID: 5
	public Light Sun;

	// Token: 0x04000006 RID: 6
	private int currentNomber;

	// Token: 0x04000007 RID: 7
	private GameObject currentInstance;

	// Token: 0x04000008 RID: 8
	private GUIStyle guiStyleHeader = new GUIStyle();

	// Token: 0x04000009 RID: 9
	private float sunIntensity;

	// Token: 0x0400000A RID: 10
	private float dpiScale;
}
