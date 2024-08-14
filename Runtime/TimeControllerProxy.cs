using System;
using UnityEngine;

namespace Lvl3Mage.TimeScaleControl
{
	/// <summary>
	/// A proxy class for the <see cref="TimeController"/> class that allows for the control of timescale from the Unity Editor (i.e. using UI events).
	/// </summary>
	public class TimeControllerProxy : MonoBehaviour
	{
		/// <summary>
		/// Proxy for the <see cref="TimeController.SetTimeScale(float)"/> method.
		/// </summary>
		public void SetTimeScale(float speed){
			TimeController.SetTimeScale(speed);
		}
		/// <summary>
		/// Proxy for the <see cref="TimeController.ResetTimeScale()"/> method.
		/// </summary>
		public void ResetTimeScale(){
			TimeController.ResetTimeScale();
		
		}
		/// <summary>
		/// Proxy for the <see cref="TimeController.SetTimeScale(float,float,Action,Action,Action)"/> method.
		/// </summary>
		public void SetTimeScale(float speed, float duration){
			TimeController.SetTimeScale(speed, duration);
		}
		/// <summary>
		/// Proxy for the <see cref="TimeController.ResetTimeScale(float,Action,Action,Action)"/> method.
		/// </summary>
		public void ResetTimeScale(float duration){
			TimeController.ResetTimeScale(duration);
		}
	}
}