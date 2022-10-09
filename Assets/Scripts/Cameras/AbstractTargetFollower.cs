using System;
using UnityEngine;

namespace UnityStandardAssets.Cameras
{
	// Token: 0x020000A2 RID: 162
	public abstract class AbstractTargetFollower : MonoBehaviour
	{
		// Token: 0x06000322 RID: 802 RVA: 0x0000FCCA File Offset: 0x0000DECA
		protected virtual void Start()
		{
			if (this.m_AutoTargetPlayer)
			{
				this.FindAndTargetPlayer();
			}
			if (this.m_Target == null)
			{
				return;
			}
			this.targetRigidbody = this.m_Target.GetComponent<Rigidbody>();
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000FCFC File Offset: 0x0000DEFC
		private void FixedUpdate()
		{
			if (this.m_AutoTargetPlayer && (this.m_Target == null || !this.m_Target.gameObject.activeSelf))
			{
				this.FindAndTargetPlayer();
			}
			if (this.m_UpdateType == AbstractTargetFollower.UpdateType.FixedUpdate)
			{
				this.FollowTarget(Time.deltaTime);
			}
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000FD4C File Offset: 0x0000DF4C
		private void LateUpdate()
		{
			if (this.m_AutoTargetPlayer && (this.m_Target == null || !this.m_Target.gameObject.activeSelf))
			{
				this.FindAndTargetPlayer();
			}
			if (this.m_UpdateType == AbstractTargetFollower.UpdateType.LateUpdate)
			{
				this.FollowTarget(Time.deltaTime);
			}
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000FD9C File Offset: 0x0000DF9C
		public void ManualUpdate()
		{
			if (this.m_AutoTargetPlayer && (this.m_Target == null || !this.m_Target.gameObject.activeSelf))
			{
				this.FindAndTargetPlayer();
			}
			if (this.m_UpdateType == AbstractTargetFollower.UpdateType.ManualUpdate)
			{
				this.FollowTarget(Time.deltaTime);
			}
		}

		// Token: 0x06000326 RID: 806
		protected abstract void FollowTarget(float deltaTime);

		// Token: 0x06000327 RID: 807 RVA: 0x0000FDEC File Offset: 0x0000DFEC
		public void FindAndTargetPlayer()
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
			if (gameObject)
			{
				this.SetTarget(gameObject.transform);
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000FE18 File Offset: 0x0000E018
		public virtual void SetTarget(Transform newTransform)
		{
			this.m_Target = newTransform;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000FE21 File Offset: 0x0000E021
		public Transform Target
		{
			get
			{
				return this.m_Target;
			}
		}

		// Token: 0x04000374 RID: 884
		[SerializeField]
		protected Transform m_Target;

		// Token: 0x04000375 RID: 885
		[SerializeField]
		private bool m_AutoTargetPlayer = true;

		// Token: 0x04000376 RID: 886
		[SerializeField]
		private AbstractTargetFollower.UpdateType m_UpdateType;

		// Token: 0x04000377 RID: 887
		protected Rigidbody targetRigidbody;

		// Token: 0x020000F1 RID: 241
		public enum UpdateType
		{
			// Token: 0x040004C3 RID: 1219
			FixedUpdate,
			// Token: 0x040004C4 RID: 1220
			LateUpdate,
			// Token: 0x040004C5 RID: 1221
			ManualUpdate
		}
	}
}
