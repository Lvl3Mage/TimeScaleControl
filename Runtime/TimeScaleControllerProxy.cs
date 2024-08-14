using System;
using UnityEngine;

namespace Lvl3Mage.TimeScaleControl
{
	/// <summary>
	/// A proxy class for the <see cref="TimeScaleController"/> class that allows for the control of timescale from the Unity Editor (i.e. using UI events).
	/// </summary>
	public class TimeScaleControllerProxy : MonoBehaviour
	{
		/// <summary>
		/// Proxy for the <see cref="TimeScaleController.SetTimeScale(float)"/> method.
		/// </summary>
		public void SetTimeScale(float speed){
			TimeScaleController.SetTimeScale(speed);
		}
		/// <summary>
		/// Proxy for the <see cref="TimeScaleController.ResetTimeScale()"/> method.
		/// </summary>
		public void ResetTimeScale(){
			TimeScaleController.ResetTimeScale();
		
		}
		/// <summary>
		/// Proxy for the <see cref="TimeScaleController.SetTimeScale(float,float,Action,Action,Action)"/> method.
		/// </summary>
		public void SetTimeScale(float speed, float duration){
			TimeScaleController.SetTimeScale(speed, duration);
		}
		/// <summary>
		/// Proxy for the <see cref="TimeScaleController.ResetTimeScale(float,Action,Action,Action)"/> method.
		/// </summary>
		public void ResetTimeScale(float duration){
			TimeScaleController.ResetTimeScale(duration);
		}
	}
}