using System;
using System.Collections;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	// Token: 0x0200005D RID: 93
	public class DragRigidbody : MonoBehaviour
	{
		// Token: 0x06000171 RID: 369 RVA: 0x00007F14 File Offset: 0x00006114
		private void Update()
		{
			if (!Input.GetMouseButtonDown(0))
			{
				return;
			}
			Camera camera = this.FindCamera();
			RaycastHit raycastHit = default(RaycastHit);
			if (!Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition).origin, camera.ScreenPointToRay(Input.mousePosition).direction, out raycastHit, 100f, -5))
			{
				return;
			}
			if (!raycastHit.rigidbody || raycastHit.rigidbody.isKinematic)
			{
				return;
			}
			if (!this.m_SpringJoint)
			{
				GameObject gameObject = new GameObject("Rigidbody dragger");
				Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
				this.m_SpringJoint = gameObject.AddComponent<SpringJoint>();
				rigidbody.isKinematic = true;
			}
			this.m_SpringJoint.transform.position = raycastHit.point;
			this.m_SpringJoint.anchor = Vector3.zero;
			this.m_SpringJoint.spring = 50f;
			this.m_SpringJoint.damper = 5f;
			this.m_SpringJoint.maxDistance = 0.2f;
			this.m_SpringJoint.connectedBody = raycastHit.rigidbody;
			base.StartCoroutine("DragObject", raycastHit.distance);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000803E File Offset: 0x0000623E
		private IEnumerator DragObject(float distance)
		{
			float oldDrag = this.m_SpringJoint.connectedBody.drag;
			float oldAngularDrag = this.m_SpringJoint.connectedBody.angularDrag;
			this.m_SpringJoint.connectedBody.drag = 10f;
			this.m_SpringJoint.connectedBody.angularDrag = 5f;
			Camera mainCamera = this.FindCamera();
			while (Input.GetMouseButton(0))
			{
				Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
				this.m_SpringJoint.transform.position = ray.GetPoint(distance);
				yield return null;
			}
			if (this.m_SpringJoint.connectedBody)
			{
				this.m_SpringJoint.connectedBody.drag = oldDrag;
				this.m_SpringJoint.connectedBody.angularDrag = oldAngularDrag;
				this.m_SpringJoint.connectedBody = null;
			}
			yield break;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00008054 File Offset: 0x00006254
		private Camera FindCamera()
		{
			if (base.GetComponent<Camera>())
			{
				return base.GetComponent<Camera>();
			}
			return Camera.main;
		}

		// Token: 0x0400018D RID: 397
		private const float k_Spring = 50f;

		// Token: 0x0400018E RID: 398
		private const float k_Damper = 5f;

		// Token: 0x0400018F RID: 399
		private const float k_Drag = 10f;

		// Token: 0x04000190 RID: 400
		private const float k_AngularDrag = 5f;

		// Token: 0x04000191 RID: 401
		private const float k_Distance = 0.2f;

		// Token: 0x04000192 RID: 402
		private const bool k_AttachToCenterOfMass = false;

		// Token: 0x04000193 RID: 403
		private SpringJoint m_SpringJoint;
	}
}
