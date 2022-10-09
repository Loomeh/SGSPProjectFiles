using System;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	// Token: 0x02000068 RID: 104
	public class SmoothFollow : MonoBehaviour
	{
		// Token: 0x06000197 RID: 407 RVA: 0x0000255D File Offset: 0x0000075D
		private void Start()
		{
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000088E4 File Offset: 0x00006AE4
		private void LateUpdate()
		{
			if (!this.target)
			{
				return;
			}
			float y = this.target.eulerAngles.y;
			float b = this.target.position.y + this.height;
			float num = base.transform.eulerAngles.y;
			float num2 = base.transform.position.y;
			num = Mathf.LerpAngle(num, y, this.rotationDamping * Time.deltaTime);
			num2 = Mathf.Lerp(num2, b, this.heightDamping * Time.deltaTime);
			Quaternion rotation = Quaternion.Euler(0f, num, 0f);
			base.transform.position = this.target.position;
			base.transform.position -= rotation * Vector3.forward * this.distance;
			base.transform.position = new Vector3(base.transform.position.x, num2, base.transform.position.z);
			base.transform.LookAt(this.target);
		}

		// Token: 0x040001C9 RID: 457
		[SerializeField]
		private Transform target;

		// Token: 0x040001CA RID: 458
		[SerializeField]
		private float distance = 10f;

		// Token: 0x040001CB RID: 459
		[SerializeField]
		private float height = 5f;

		// Token: 0x040001CC RID: 460
		[SerializeField]
		private float rotationDamping;

		// Token: 0x040001CD RID: 461
		[SerializeField]
		private float heightDamping;
	}
}
