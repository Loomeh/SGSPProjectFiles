using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000041 RID: 65
public class PlayerMovement : MonoBehaviour
{
	// Token: 0x060000F9 RID: 249 RVA: 0x00005DE0 File Offset: 0x00003FE0
	private void Awake()
	{
		PlayerMovement.instance = this;
	}

	// Token: 0x060000FA RID: 250 RVA: 0x00005DE8 File Offset: 0x00003FE8
	private void Start()
	{
		PlayerMovement.health = 100f;
		PlayerMovement.dead = false;
		PlayerMovement.canControl = true;
		Transform playerInitialSpawn = WarpManager.Get().GetPlayerInitialSpawn(GameManager.prevLevel);
		if (playerInitialSpawn == null)
		{
			return;
		}
		base.transform.position = playerInitialSpawn.position;
		base.transform.rotation = playerInitialSpawn.rotation;
	}

	// Token: 0x060000FB RID: 251 RVA: 0x00005E48 File Offset: 0x00004048
	private void Update()
	{
		if (!PauseManager.Get().paused)
		{
			if (PlayerMovement.autoSprint)
			{
				this.walking = !this.walking;
			}
			if (this.aiming && this.muzzleFlash.transform.position != this.muzzleFlashAimTransform.transform.position)
			{
				this.muzzleFlash.transform.position = this.muzzleFlashAimTransform.transform.position;
			}
			if (this.unaiming)
			{
				this.muzzleFlash.transform.position = this.muzzleFlashUnaimTransform.transform.position;
			}
			this.animator.SetBool("inAir", !PlayerMovement.isGrounded);
			if (PlayerMovement.health < 100f)
			{
				this.healTimer -= Time.deltaTime;
				if (this.healTimer <= 0f)
				{
					PlayerMovement.UpdateHealth(10f);
					this.healTimer = 2f;
				}
			}
			if ((Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f) && !this.walking)
			{
				this.animator.SetBool("walkBool", true);
				this.animator.SetBool("runBool", false);
				this.walking = true;
				if (Input.GetKey(KeyCode.LeftShift))
				{
					this.animator.SetBool("runBool", true);
				}
			}
			else if (Input.GetAxis("Horizontal") == 0f && Input.GetAxis("Vertical") == 0f && this.walking)
			{
				this.animator.SetBool("walkBool", false);
				this.animator.SetBool("runBool", false);
				this.walking = false;
			}
			if (this.fireTimer >= 0f)
			{
				this.fireTimer -= Time.deltaTime;
			}
			if (PlayerMovement.canControl)
			{
				if (Input.GetKey(KeyCode.LeftShift))
				{
					if (this.walking && !PlayerMovement.sprint)
					{
						this.animator.SetBool("runBool", true);
					}
					PlayerMovement.sprint = true;
				}
				else
				{
					if (PlayerMovement.sprint)
					{
						if (this.walking)
						{
							this.animator.SetBool("runBool", false);
						}
						else if (this.velocity.x != 0f && this.velocity.z != 0f)
						{
							this.animator.SetBool("runBool", true);
						}
					}
					PlayerMovement.sprint = false;
				}
				if (Input.GetMouseButton(1))
				{
					this.animator.SetBool("aiming", true);
				}
				else
				{
					this.animator.SetBool("aiming", false);
				}
				if (Input.GetMouseButtonDown(1) && !this.aimingIn)
				{
					this.animator.SetTrigger("aim");
					this.aiming = true;
					this.unaiming = false;
					this.aimingIn = true;
				}
				if ((this.aiming && Input.GetMouseButtonUp(1) && !this.unaiming) || this.reloading)
				{
					this.animator.SetTrigger("unaim");
					this.aiming = false;
					this.aimingIn = false;
					this.unaiming = true;
					HUDManager.Get().crosshair.SetActive(true);
				}
				if (this.aimingIn)
				{
					this.gunTransform.position = Vector3.MoveTowards(this.gunTransform.position, this.aimTransform.position, 50f * Time.deltaTime);
					this.gunTransform.rotation = Quaternion.RotateTowards(this.gunTransform.rotation, this.aimTransform.rotation, 50f * Time.deltaTime);
					if (this.gunTransform.position == this.aimTransform.position && this.gunTransform.rotation == this.aimTransform.rotation)
					{
						this.aimingIn = false;
						HUDManager.Get().crosshair.SetActive(false);
					}
				}
				else if (this.unaiming || this.reloading)
				{
					this.gunTransform.position = Vector3.MoveTowards(this.gunTransform.position, this.unaimedTransform.position, 50f * Time.deltaTime);
					this.gunTransform.rotation = Quaternion.RotateTowards(this.gunTransform.rotation, this.unaimedTransform.rotation, 50f * Time.deltaTime);
					if (this.gunTransform.position == this.unaimedTransform.position && this.gunTransform.rotation == this.unaimedTransform.rotation)
					{
						this.unaiming = false;
					}
				}
				if (Input.GetMouseButtonDown(0) && this.fireTimer <= 0f && !this.reloading)
				{
					if (PlayerMovement.ammo > 0)
					{
						PlayerMovement.ammo--;
						AudioManager.Get().Play("GlockFire");
						RaycastHit raycastHit;
						if (Physics.Raycast(this.fpsCam.transform.position, this.fpsCam.transform.forward, out raycastHit, 500f, LayerMask.GetMask(new string[]
						{
							"Shootable"
						})))
						{
							HUDManager.Get().hitMarkers.SetActive(false);
							HUDManager.Get().hitMarkers.SetActive(true);
							string tag = raycastHit.collider.tag;
							uint num = <PrivateImplementationDetails>.ComputeStringHash(tag);
							if (num <= 1452685169U)
							{
								if (num <= 938470621U)
								{
									if (num != 429623513U)
									{
										if (num != 802805123U)
										{
											if (num == 938470621U)
											{
												if (tag == "FishLeg")
												{
													raycastHit.collider.GetComponentInParent<Fish>().leg = true;
													raycastHit.collider.GetComponentInParent<Fish>().SendMessage("Shot");
												}
											}
										}
										else if (tag == "Fish")
										{
											raycastHit.collider.GetComponentInParent<Fish>().SendMessage("Shot");
										}
									}
									else if (tag == "MrKrabs")
									{
										raycastHit.collider.GetComponentInParent<MrKrabs>().SendMessage("Shot");
									}
								}
								else if (num != 1223284393U)
								{
									if (num != 1311244785U)
									{
										if (num == 1452685169U)
										{
											if (tag == "Patrick")
											{
												raycastHit.collider.GetComponentInParent<Patrick>().SendMessage("Shot");
											}
										}
									}
									else if (tag == "Thug")
									{
										raycastHit.collider.GetComponentInParent<Thug>().SendMessage("Shot");
									}
								}
								else if (tag == "JellyFish")
								{
									raycastHit.collider.GetComponentInParent<JellyFish>().SendMessage("Shot");
								}
							}
							else if (num <= 3580974891U)
							{
								if (num != 2188757914U)
								{
									if (num != 2929485700U)
									{
										if (num == 3580974891U)
										{
											if (tag == "MrKrabsTrueForm")
											{
												raycastHit.collider.GetComponent<MrKrabsTrueForm>().Shot();
											}
										}
									}
									else if (tag == "Gary")
									{
										raycastHit.collider.GetComponent<Gary>().SendMessage("Shot");
									}
								}
								else if (tag == "Generic")
								{
									raycastHit.collider.GetComponent<ShootableRigidbody>().SendMessage("Shot");
								}
							}
							else if (num != 3626534731U)
							{
								if (num != 3652900801U)
								{
									if (num == 4194582670U)
									{
										if (tag == "Sandy")
										{
											raycastHit.collider.GetComponentInParent<Sandy>().SendMessage("Shot");
										}
									}
								}
								else if (tag == "Larry")
								{
									raycastHit.collider.GetComponentInParent<Larry>().SendMessage("Shot");
								}
							}
							else if (tag == "VendingMachine")
							{
								raycastHit.collider.GetComponent<VendingMachine>().SendMessage("Shot");
							}
						}
						if (!this.aiming)
						{
							this.animator.SetTrigger("fire");
						}
						else
						{
							this.animator.SetTrigger("aimfire");
						}
						this.fireTimer = 0.2f;
						this.muzzleFlash.Play();
					}
					else
					{
						AudioManager.Get().Play("GlockFireNoAmmo");
						int num2 = this.reloadAttempts;
						this.reloadAttempts = num2 + 1;
						if (num2 == 5)
						{
							HUDManager.Get().ineractText.gameObject.SetActive(true);
							HUDManager.Get().ineractText.text = "Press R to reload";
							this.reloadAttempts = 0;
						}
					}
				}
				if (Input.GetKeyDown(KeyCode.R) && PlayerMovement.ammo <= 0 && !this.reloading)
				{
					this.aiming = false;
					this.aimingIn = false;
					this.unaiming = true;
					if (HUDManager.Get().ineractText.text == "Press R to reload")
					{
						HUDManager.Get().ineractText.gameObject.SetActive(false);
					}
					base.StartCoroutine(this.Reload());
					this.animator.SetTrigger("reload");
				}
				this.prevIsGrounded = PlayerMovement.isGrounded;
				PlayerMovement.isGrounded = Physics.CheckSphere(this.groundCheck.position, this.groundDistance);
				if (!PlayerMovement.isGrounded && this.prevIsGrounded && this.velocity.y <= 0f)
				{
					this.velocity.y = -2f;
				}
				RaycastHit raycastHit2;
				if (!this.ceilingHit && !PlayerMovement.isGrounded && Physics.Raycast(this.controller.transform.position, Vector3.up, out raycastHit2, this.controller.height / 2f + this.ceilingHitLength))
				{
					this.velocity.y = 0f;
					this.ceilingHit = true;
				}
				if (PlayerMovement.isGrounded && this.ceilingHit)
				{
					this.ceilingHit = false;
				}
				float axis = Input.GetAxis("Horizontal");
				float axis2 = Input.GetAxis("Vertical");
				Vector3 vector = base.transform.right * axis + base.transform.forward * axis2;
				if (PlayerMovement.autoSprint)
				{
					PlayerMovement.sprint = !PlayerMovement.sprint;
				}
				this.controller.Move(vector * (PlayerMovement.sprint ? this.runSpeed : this.walkSpeed) * Time.deltaTime);
				this.velocity.x = vector.x * (PlayerMovement.sprint ? this.runSpeed : this.walkSpeed) * Time.deltaTime;
				this.velocity.z = vector.z * (PlayerMovement.sprint ? this.runSpeed : this.walkSpeed) * Time.deltaTime;
				if (Input.GetButtonDown("Jump") && PlayerMovement.isGrounded)
				{
					this.velocity.y = Mathf.Sqrt(this.jumpHeight * -2f * this.gravity);
				}
			}
			this.velocity.y = this.velocity.y + this.gravity * Time.deltaTime;
			this.controller.Move(this.velocity * Time.deltaTime);
		}
	}

	// Token: 0x060000FC RID: 252 RVA: 0x000069E8 File Offset: 0x00004BE8
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("MrKrabsTrueForm"))
		{
			PlayerMovement.UpdateHealth(-80f);
		}
	}

	// Token: 0x060000FD RID: 253 RVA: 0x00006A04 File Offset: 0x00004C04
	private bool OnSlope()
	{
		RaycastHit raycastHit;
		return PlayerMovement.isGrounded && (Physics.Raycast(base.transform.position, Vector3.down, out raycastHit, this.controller.height / 2f * this.slopeForceRayLength) && raycastHit.normal != Vector3.up);
	}

	// Token: 0x060000FE RID: 254 RVA: 0x00006A61 File Offset: 0x00004C61
	public static void UpdateMoney(float value)
	{
		PlayerMovement.money += value;
	}

	// Token: 0x060000FF RID: 255 RVA: 0x00006A70 File Offset: 0x00004C70
	public static void SetHealth(float value)
	{
		PlayerMovement.health = value;
		HUDManager.Get().bloodOverlay.color = new Color(HUDManager.Get().bloodOverlay.color.r, HUDManager.Get().bloodOverlay.color.g, HUDManager.Get().bloodOverlay.color.b, (PlayerMovement.health - 0f) * -1f / 100f + 1f);
		if (PlayerMovement.health <= 0f)
		{
			GameManager.Get().ReloadLevel();
		}
	}

	// Token: 0x06000100 RID: 256 RVA: 0x00006B08 File Offset: 0x00004D08
	public static void UpdateHealth(float value)
	{
		if (PlayerMovement.dead)
		{
			return;
		}
		if (value < 0f)
		{
			PlayerMovement.instance.hurtSounds[Random.Range(0, 3)].Play();
		}
		PlayerMovement.health += value;
		HUDManager.Get().bloodOverlay.color = new Color(HUDManager.Get().bloodOverlay.color.r, HUDManager.Get().bloodOverlay.color.g, HUDManager.Get().bloodOverlay.color.b, (PlayerMovement.health - 0f) * -1f / 100f + 1f);
		if (PlayerMovement.health <= 0f)
		{
			PlayerMovement.dead = true;
			GameManager.Get().ReloadLevel();
		}
	}

	// Token: 0x06000101 RID: 257 RVA: 0x00006BD1 File Offset: 0x00004DD1
	private IEnumerator Reload()
	{
		this.reloading = true;
		yield return new WaitForSeconds(1f);
		AudioManager.Get().Play("GlockReload");
		yield return new WaitForSeconds(1f);
		PlayerMovement.ammo = 6;
		this.reloading = false;
		yield break;
	}

	// Token: 0x06000102 RID: 258 RVA: 0x00006BE0 File Offset: 0x00004DE0
	private IEnumerator Aim()
	{
		if (this.unaiming)
		{
			yield return null;
		}
		while (this.gunTransform.position != this.aimTransform.position || this.gunTransform.rotation != this.aimTransform.rotation)
		{
			this.gunTransform.position = Vector3.MoveTowards(this.gunTransform.position, this.aimTransform.position, 50f * Time.deltaTime);
			this.gunTransform.rotation = Quaternion.RotateTowards(this.gunTransform.rotation, this.aimTransform.rotation, 50f * Time.deltaTime);
			yield return null;
		}
		this.aimingIn = false;
		yield break;
	}

	// Token: 0x06000103 RID: 259 RVA: 0x00006BEF File Offset: 0x00004DEF
	private IEnumerator Unaim()
	{
		if (this.aimingIn)
		{
			yield return null;
		}
		while (this.gunTransform.position != this.unaimedTransform.position || this.gunTransform.rotation != this.unaimedTransform.rotation)
		{
			Debug.Log("Lerping rot");
			this.gunTransform.position = Vector3.MoveTowards(this.gunTransform.position, this.unaimedTransform.position, 50f * Time.deltaTime);
			this.gunTransform.rotation = Quaternion.RotateTowards(this.gunTransform.rotation, this.unaimedTransform.rotation, 50f * Time.deltaTime);
			yield return null;
		}
		this.unaiming = false;
		yield break;
	}

	// Token: 0x0400010B RID: 267
	public static float health = 100f;

	// Token: 0x0400010C RID: 268
	public static int ammo = 6;

	// Token: 0x0400010D RID: 269
	public int reloadAttempts;

	// Token: 0x0400010E RID: 270
	public static float money;

	// Token: 0x0400010F RID: 271
	public static bool autoSprint;

	// Token: 0x04000110 RID: 272
	public static bool sprint;

	// Token: 0x04000111 RID: 273
	public Transform gunTransform;

	// Token: 0x04000112 RID: 274
	public Transform aimTransform;

	// Token: 0x04000113 RID: 275
	public Transform unaimedTransform;

	// Token: 0x04000114 RID: 276
	public Camera fpsCam;

	// Token: 0x04000115 RID: 277
	public Animator animator;

	// Token: 0x04000116 RID: 278
	public CharacterController controller;

	// Token: 0x04000117 RID: 279
	public Transform groundCheck;

	// Token: 0x04000118 RID: 280
	public LayerMask groundMask;

	// Token: 0x04000119 RID: 281
	public ParticleSystem muzzleFlash;

	// Token: 0x0400011A RID: 282
	public Transform muzzleFlashAimTransform;

	// Token: 0x0400011B RID: 283
	public Transform muzzleFlashUnaimTransform;

	// Token: 0x0400011C RID: 284
	public AudioSource[] hurtSounds;

	// Token: 0x0400011D RID: 285
	public float walkSpeed = 12f;

	// Token: 0x0400011E RID: 286
	public float runSpeed = 30f;

	// Token: 0x0400011F RID: 287
	public Vector3 velocity;

	// Token: 0x04000120 RID: 288
	public float gravity = -9.81f;

	// Token: 0x04000121 RID: 289
	public float groundDistance = 0.4f;

	// Token: 0x04000122 RID: 290
	public static bool isGrounded = false;

	// Token: 0x04000123 RID: 291
	private bool prevIsGrounded;

	// Token: 0x04000124 RID: 292
	public float jumpHeight = 3f;

	// Token: 0x04000125 RID: 293
	public static bool canControl;

	// Token: 0x04000126 RID: 294
	private float fireTimer;

	// Token: 0x04000127 RID: 295
	private bool reloading;

	// Token: 0x04000128 RID: 296
	private bool aiming;

	// Token: 0x04000129 RID: 297
	private bool aimingIn;

	// Token: 0x0400012A RID: 298
	private bool unaiming;

	// Token: 0x0400012B RID: 299
	public float slopeForce;

	// Token: 0x0400012C RID: 300
	public float slopeForceRayLength;

	// Token: 0x0400012D RID: 301
	public float ceilingHitLength;

	// Token: 0x0400012E RID: 302
	public bool ceilingHit;

	// Token: 0x0400012F RID: 303
	private float healTimer;

	// Token: 0x04000130 RID: 304
	private bool walking;

	// Token: 0x04000131 RID: 305
	public static PlayerMovement instance;

	// Token: 0x04000132 RID: 306
	public static bool dead;
}
