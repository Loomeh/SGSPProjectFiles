using System;
using UnityEngine;

namespace UnityStandardAssets.Effects
{
	// Token: 0x0200006D RID: 109
	[RequireComponent(typeof(SphereCollider))]
	public class AfterburnerPhysicsForce : MonoBehaviour
	{
		// Token: 0x060001B9 RID: 441 RVA: 0x0000942C File Offset: 0x0000762C
		private void OnEnable()
		{
			this.m_Sphere = (base.GetComponent<Collider>() as SphereCollider);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00009440 File Offset: 0x00007640
		private void FixedUpdate()
		{
			this.m_Cols = Physics.OverlapSphere(base.transform.position + this.m_Sphere.center, this.m_Sphere.radius);
			for (int i = 0; i < this.m_Cols.Length; i++)
			{
				if (this.m_Cols[i].attachedRigidbody != null)
				{
					Vector3 vector = base.transform.InverseTransformPoint(this.m_Cols[i].transform.position);
					vector = Vector3.MoveTowards(vector, new Vector3(0f, 0f, vector.z), this.effectWidth * 0.5f);
					float value = Mathf.Abs(Mathf.Atan2(vector.x, vector.z) * 57.29578f);
					float num = Mathf.InverseLerp(this.effectDistance, 0f, vector.magnitude);
					num *= Mathf.InverseLerp(this.effectAngle, 0f, value);
					Vector3 vector2 = this.m_Cols[i].transform.position - base.transform.position;
					this.m_Cols[i].attachedRigidbody.AddForceAtPosition(vector2.normalized * this.force * num, Vector3.Lerp(this.m_Cols[i].transform.position, base.transform.TransformPoint(0f, 0f, vector.z), 0.1f));
				}
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000095C4 File Offset: 0x000077C4
		private void OnDrawGizmosSelected()
		{
			if (this.m_Sphere == null)
			{
				this.m_Sphere = (base.GetComponent<Collider>() as SphereCollider);
			}
			this.m_Sphere.radius = this.effectDistance * 0.5f;
			this.m_Sphere.center = new Vector3(0f, 0f, this.effectDistance * 0.5f);
			Vector3[] array = new Vector3[]
			{
				Vector3.up,
				-Vector3.up,
				Vector3.right,
				-Vector3.right
			};
			Vector3[] array2 = new Vector3[]
			{
				-Vector3.right,
				Vector3.right,
				Vector3.up,
				-Vector3.up
			};
			Gizmos.color = new Color(0f, 1f, 0f, 0.5f);
			for (int i = 0; i < 4; i++)
			{
				Vector3 vector = base.transform.position + base.transform.rotation * array[i] * this.effectWidth * 0.5f;
				Vector3 a = base.transform.TransformDirection(Quaternion.AngleAxis(this.effectAngle, array2[i]) * Vector3.forward);
				Gizmos.DrawLine(vector, vector + a * this.m_Sphere.radius * 2f);
			}
		}

		// Token: 0x040001F0 RID: 496
		public float effectAngle = 15f;

		// Token: 0x040001F1 RID: 497
		public float effectWidth = 1f;

		// Token: 0x040001F2 RID: 498
		public float effectDistance = 10f;

		// Token: 0x040001F3 RID: 499
		public float force = 10f;

		// Token: 0x040001F4 RID: 500
		private Collider[] m_Cols;

		// Token: 0x040001F5 RID: 501
		private SphereCollider m_Sphere;
	}
}
