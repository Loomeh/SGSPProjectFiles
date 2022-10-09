using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200003B RID: 59
public class MrKrabsTrueForm : MonoBehaviour
{
	// Token: 0x060000CD RID: 205 RVA: 0x000050F8 File Offset: 0x000032F8
	private void Start()
	{
		this.rb = base.GetComponent<Rigidbody>();
		this.animator = base.GetComponent<Animator>();
		this.player = GameObject.FindGameObjectWithTag("Player");
		this.HEALTH_STAGE1_COLOR = new Color(this.Remap(0f, 0f, 255f, 0f, 1f), this.Remap(255f, 0f, 255f, 0f, 1f), this.Remap(7f, 0f, 255f, 0f, 1f), this.Remap(109f, 0f, 255f, 0f, 1f));
		this.HEALTH_STAGE2_COLOR = new Color(this.Remap(255f, 0f, 255f, 0f, 1f), this.Remap(255f, 0f, 255f, 0f, 1f), this.Remap(7f, 0f, 255f, 0f, 1f), this.Remap(109f, 0f, 255f, 0f, 1f));
		this.HEALTH_STAGE3_COLOR = new Color(this.Remap(255f, 0f, 255f, 0f, 1f), this.Remap(0f, 0f, 255f, 0f, 1f), this.Remap(7f, 0f, 255f, 0f, 1f), this.Remap(109f, 0f, 255f, 0f, 1f));
		FadeManager.Get().PlayFadeOut(FadeManager.defaultSpeed);
		AudioManager.Get().MusicFadeIn(AudioManager.defaultMusicFadeDuration);
		ObjectiveManager.Get().UpdateObjective();
		this.trackTimer = 8f;
		this.trackWaitTimer = 3f;
	}

	// Token: 0x060000CE RID: 206 RVA: 0x00005300 File Offset: 0x00003500
	private void Update()
	{
		Debug.Log(this.state + " - " + this.rb.velocity);
		switch (this.state)
		{
		case MrKrabsTrueFormState.TRACK:
			if (this.trackTimer > 0f)
			{
				base.transform.rotation = Quaternion.Euler(base.transform.rotation.eulerAngles.x, Quaternion.Lerp(base.transform.rotation, Quaternion.LookRotation(this.player.transform.position - base.transform.position), 2f * Time.deltaTime).eulerAngles.y, base.transform.rotation.eulerAngles.z);
				this.trackTimer -= Time.deltaTime;
				return;
			}
			this.trackWaitTimer -= Time.deltaTime;
			if (this.trackWaitTimer <= 0f)
			{
				this.animator.SetTrigger("run");
				this.state = MrKrabsTrueFormState.CHARGE;
				return;
			}
			break;
		case MrKrabsTrueFormState.CHARGE:
			this.rb.AddForce(base.transform.forward * 10f, ForceMode.VelocityChange);
			return;
		case MrKrabsTrueFormState.WAIT:
			this.waitTimer -= Time.deltaTime;
			if (this.waitTimer <= 0f)
			{
				if (this.bombHit)
				{
					this.bombHit = false;
				}
				else if (this.wallHit)
				{
					this.wallHit = false;
				}
				this.state = MrKrabsTrueFormState.TRACK;
				this.trackTimer = 6f;
				this.trackWaitTimer = 2f;
			}
			break;
		default:
			return;
		}
	}

	// Token: 0x060000CF RID: 207 RVA: 0x000054BC File Offset: 0x000036BC
	private void OnTriggerEnter(Collider other)
	{
		if (this.state != MrKrabsTrueFormState.CHARGE)
		{
			return;
		}
		if (other.CompareTag("Bomb") && !this.wallHit && !this.bombHit)
		{
			Debug.Log("bomb hit");
			this.animator.SetTrigger("hit");
			this.waitTimer = 8f;
			this.bombHit = true;
			this.rb.velocity = Vector3.zero;
			AudioManager.Get().Play("Explosion");
			this.state = MrKrabsTrueFormState.WAIT;
			return;
		}
		if (other.CompareTag("Boundary") && !this.bombHit && !this.wallHit)
		{
			Debug.Log("Boundary hit");
			this.animator.SetTrigger("hit");
			this.waitTimer = 2f;
			this.wallHit = true;
			this.rb.velocity = Vector3.zero;
			this.state = MrKrabsTrueFormState.WAIT;
		}
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x000055A4 File Offset: 0x000037A4
	public void Shot()
	{
		if (this.dead)
		{
			return;
		}
		if (this.state == MrKrabsTrueFormState.WAIT && this.bombHit)
		{
			this.health--;
			this.healthBar.transform.localScale = new Vector3(this.Remap((float)this.health, 0f, (float)this.MAX_HEALTH, 0f, 1f), 1f, 1f);
			if (this.health == 0)
			{
				this.healthStage++;
				if (this.healthStage != 4)
				{
					this.health = 10;
					this.healthBar.transform.localScale = new Vector3(this.Remap((float)this.health, 0f, (float)this.MAX_HEALTH, 0f, 1f), 1f, 1f);
				}
				if (this.healthStage == 2)
				{
					this.healthBar.color = this.HEALTH_STAGE2_COLOR;
				}
				else if (this.healthStage == 3)
				{
					this.healthBar.color = this.HEALTH_STAGE3_COLOR;
				}
			}
			if (this.healthStage == 4)
			{
				this.dead = true;
				base.StartCoroutine(this.FinishFight());
			}
		}
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x000056E0 File Offset: 0x000038E0
	private float Remap(float value, float from1, float to1, float from2, float to2)
	{
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x000056F2 File Offset: 0x000038F2
	private IEnumerator FinishFight()
	{
		ObjectiveManager.currentObjective = 14;
		SaveManager.Save();
		FadeManager.Get().PlayFadeIn(2f);
		AudioManager.Get().MusicFadeOut(AudioManager.defaultMusicFadeDuration);
		while (FadeManager.Get().IsPlaying())
		{
			yield return null;
		}
		this.healthContainer.SetActive(false);
		this.bloodySpongeBob.SetActive(true);
		CutsceneManager.Get().PlayCutscene(Cutscene.END);
		this.deadKrab.SetActive(true);
		this.bombSpawner1.SetActive(false);
		this.bombSpawner2.SetActive(false);
		this.bombSpawner3.SetActive(false);
		GameObject[] array = GameObject.FindGameObjectsWithTag("Bomb");
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(false);
		}
		yield break;
	}

	// Token: 0x040000D5 RID: 213
	public Animator animator;

	// Token: 0x040000D6 RID: 214
	public Rigidbody rb;

	// Token: 0x040000D7 RID: 215
	public GameObject healthContainer;

	// Token: 0x040000D8 RID: 216
	public Image healthBar;

	// Token: 0x040000D9 RID: 217
	private Color HEALTH_STAGE1_COLOR;

	// Token: 0x040000DA RID: 218
	private Color HEALTH_STAGE2_COLOR;

	// Token: 0x040000DB RID: 219
	private Color HEALTH_STAGE3_COLOR;

	// Token: 0x040000DC RID: 220
	private int MAX_HEALTH = 10;

	// Token: 0x040000DD RID: 221
	public int healthStage = 1;

	// Token: 0x040000DE RID: 222
	public int health = 10;

	// Token: 0x040000DF RID: 223
	private MrKrabsTrueFormState state;

	// Token: 0x040000E0 RID: 224
	private float trackTimer = 50f;

	// Token: 0x040000E1 RID: 225
	private float trackWaitTimer;

	// Token: 0x040000E2 RID: 226
	public float bombTimer;

	// Token: 0x040000E3 RID: 227
	public GameObject player;

	// Token: 0x040000E4 RID: 228
	public bool wallHit;

	// Token: 0x040000E5 RID: 229
	public bool bombHit;

	// Token: 0x040000E6 RID: 230
	public float waitTimer;

	// Token: 0x040000E7 RID: 231
	private bool dead;

	// Token: 0x040000E8 RID: 232
	public GameObject bloodySpongeBob;

	// Token: 0x040000E9 RID: 233
	public GameObject deadKrab;

	// Token: 0x040000EA RID: 234
	public GameObject bombSpawner1;

	// Token: 0x040000EB RID: 235
	public GameObject bombSpawner2;

	// Token: 0x040000EC RID: 236
	public GameObject bombSpawner3;
}
