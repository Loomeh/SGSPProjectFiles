using System;
using UnityEngine;

// Token: 0x02000037 RID: 55
public class MouseLook : MonoBehaviour
{
	// Token: 0x060000BD RID: 189 RVA: 0x00004EA4 File Offset: 0x000030A4
	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	// Token: 0x060000BE RID: 190 RVA: 0x00004EB4 File Offset: 0x000030B4
	private void Update()
	{
		if (PlayerMovement.canControl)
		{
			float d = Input.GetAxis("Mouse X") * MouseLook.mouseSensitivity;
			float num = Input.GetAxis("Mouse Y") * MouseLook.mouseSensitivity;
			this.xRot -= num;
			this.xRot = Mathf.Clamp(this.xRot, -90f, 90f);
			base.transform.localRotation = Quaternion.Euler(this.xRot, 0f, 0f);
			this.playerBody.Rotate(Vector3.up * d);
		}
	}

	// Token: 0x040000C7 RID: 199
	public Transform playerBody;

	// Token: 0x040000C8 RID: 200
	public static float mouseSensitivity = 5f;

	// Token: 0x040000C9 RID: 201
	private float xRot;
}
