using System;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	// Token: 0x0200006C RID: 108
	public class WaypointProgressTracker : MonoBehaviour
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00009049 File Offset: 0x00007249
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00009051 File Offset: 0x00007251
		public WaypointCircuit.RoutePoint targetPoint { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000905A File Offset: 0x0000725A
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x00009062 File Offset: 0x00007262
		public WaypointCircuit.RoutePoint speedPoint { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000906B File Offset: 0x0000726B
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x00009073 File Offset: 0x00007273
		public WaypointCircuit.RoutePoint progressPoint { get; private set; }

		// Token: 0x060001B4 RID: 436 RVA: 0x0000907C File Offset: 0x0000727C
		private void Start()
		{
			if (this.target == null)
			{
				this.target = new GameObject(base.name + " Waypoint Target").transform;
			}
			this.Reset();
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000090B4 File Offset: 0x000072B4
		public void Reset()
		{
			this.progressDistance = 0f;
			this.progressNum = 0;
			if (this.progressStyle == WaypointProgressTracker.ProgressStyle.PointToPoint)
			{
				this.target.position = this.circuit.Waypoints[this.progressNum].position;
				this.target.rotation = this.circuit.Waypoints[this.progressNum].rotation;
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00009120 File Offset: 0x00007320
		private void Update()
		{
			if (this.progressStyle == WaypointProgressTracker.ProgressStyle.SmoothAlongRoute)
			{
				if (Time.deltaTime > 0f)
				{
					this.speed = Mathf.Lerp(this.speed, (this.lastPosition - base.transform.position).magnitude / Time.deltaTime, Time.deltaTime);
				}
				this.target.position = this.circuit.GetRoutePoint(this.progressDistance + this.lookAheadForTargetOffset + this.lookAheadForTargetFactor * this.speed).position;
				this.target.rotation = Quaternion.LookRotation(this.circuit.GetRoutePoint(this.progressDistance + this.lookAheadForSpeedOffset + this.lookAheadForSpeedFactor * this.speed).direction);
				this.progressPoint = this.circuit.GetRoutePoint(this.progressDistance);
				Vector3 lhs = this.progressPoint.position - base.transform.position;
				if (Vector3.Dot(lhs, this.progressPoint.direction) < 0f)
				{
					this.progressDistance += lhs.magnitude * 0.5f;
				}
				this.lastPosition = base.transform.position;
				return;
			}
			if ((this.target.position - base.transform.position).magnitude < this.pointToPointThreshold)
			{
				this.progressNum = (this.progressNum + 1) % this.circuit.Waypoints.Length;
			}
			this.target.position = this.circuit.Waypoints[this.progressNum].position;
			this.target.rotation = this.circuit.Waypoints[this.progressNum].rotation;
			this.progressPoint = this.circuit.GetRoutePoint(this.progressDistance);
			Vector3 lhs2 = this.progressPoint.position - base.transform.position;
			if (Vector3.Dot(lhs2, this.progressPoint.direction) < 0f)
			{
				this.progressDistance += lhs2.magnitude;
			}
			this.lastPosition = base.transform.position;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00009364 File Offset: 0x00007564
		private void OnDrawGizmos()
		{
			if (Application.isPlaying)
			{
				Gizmos.color = Color.green;
				Gizmos.DrawLine(base.transform.position, this.target.position);
				Gizmos.DrawWireSphere(this.circuit.GetRoutePosition(this.progressDistance), 1f);
				Gizmos.color = Color.yellow;
				Gizmos.DrawLine(this.target.position, this.target.position + this.target.forward);
			}
		}

		// Token: 0x040001E1 RID: 481
		[SerializeField]
		private WaypointCircuit circuit;

		// Token: 0x040001E2 RID: 482
		[SerializeField]
		private float lookAheadForTargetOffset = 5f;

		// Token: 0x040001E3 RID: 483
		[SerializeField]
		private float lookAheadForTargetFactor = 0.1f;

		// Token: 0x040001E4 RID: 484
		[SerializeField]
		private float lookAheadForSpeedOffset = 10f;

		// Token: 0x040001E5 RID: 485
		[SerializeField]
		private float lookAheadForSpeedFactor = 0.2f;

		// Token: 0x040001E6 RID: 486
		[SerializeField]
		private WaypointProgressTracker.ProgressStyle progressStyle;

		// Token: 0x040001E7 RID: 487
		[SerializeField]
		private float pointToPointThreshold = 4f;

		// Token: 0x040001EB RID: 491
		public Transform target;

		// Token: 0x040001EC RID: 492
		private float progressDistance;

		// Token: 0x040001ED RID: 493
		private int progressNum;

		// Token: 0x040001EE RID: 494
		private Vector3 lastPosition;

		// Token: 0x040001EF RID: 495
		private float speed;

		// Token: 0x020000DB RID: 219
		public enum ProgressStyle
		{
			// Token: 0x0400046B RID: 1131
			SmoothAlongRoute,
			// Token: 0x0400046C RID: 1132
			PointToPoint
		}
	}
}
