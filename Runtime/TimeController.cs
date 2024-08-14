using System;
using System.Collections;
using System.Threading.Tasks;
using Lvl3Mage.CoroutineToolkit;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lvl3Mage.TimeScaleControl
{
	/// <summary>
	/// Manages the timescale of the game, automatically adjusting the fixedDeltaTime as needed.
	/// Unless otherwise specified, resets the timescale to the default value when a new scene is loaded. Use <see cref="ToggleSceneTimeReset"/> to change this behavior.
	/// </summary>
	public static class TimeController
	{
		
		static readonly float RealPhysicsTime = Time.fixedDeltaTime;
		static readonly float RealTime = Time.timeScale;
		static bool resetOnSceneLoad = true;
		/// <summary>
		/// Set whether the timescale should be reset to the default value when a new scene is loaded. By default, this is true.
		/// </summary>
		/// <param name="value">
		/// When true, the timescale will be reset to the default value when a new scene is loaded.
		/// </param>
		public static void ToggleSceneTimeReset(bool value)
		{
			resetOnSceneLoad = value;
		}
		static TimeController()
		{
			SceneManager.sceneLoaded += (_, _) => {
				if (!resetOnSceneLoad){
					return;
				}
				Debug.Log("Timescale reset on scene load");
				ResetTimeScale();
			};
		}
		/// <summary>
		/// Set the timescale to a new value
		/// </summary>
		/// <param name="newTimeScale">
		/// The new timescale value
		/// </param>
		public static void SetTimeScale(float newTimeScale)
		{
			StopInterpolation();
			AssignTimeScale(newTimeScale);
		}


		/// <summary>
		/// Reset the timescale to the default value
		/// </summary>
		public static void ResetTimeScale()
		{
			StopInterpolation();
			AssignTimeScale(RealTime);
		}

		///  <summary>
		///  Interpolate the timescale to a new value over a duration
		///  </summary>
		///  <param name="newTimeScale">
		/// 	The new timescale value
		///  </param>
		///  <param name="duration">
		///  The duration of the interpolation
		///  </param>
		///  <param name="onEnd">
		///	 A callback to be called when the interpolation is over. Gets called either after completion or if the interpolation is interrupted.
		///  </param>
		///  <param name="onComplete">
		///  A callback to be called when the interpolation is over. Gets called only after completion.
		///  </param>
		///  <param name="onStop">
		///  A callback to be called when the interpolation is stopped. Gets called only if the interpolation is interrupted.
		///  </param>
		public static void SetTimeScale(float newTimeScale, float duration, Action onEnd = null, Action onComplete = null, Action onStop = null)
		{
			StartInterpolation(newTimeScale, duration, ()=> {
				onComplete?.Invoke();
				onEnd?.Invoke();
			}, ()=> {
				onStop?.Invoke();
				onEnd?.Invoke();
			});
		}
		/// <summary>
		/// Interpolates the timescale to the default value over a duration
		/// </summary>
		/// <param name="duration">
		/// The duration of the interpolation
		/// </param>
		///  <param name="onEnd">
		///	 A callback to be called when the interpolation is over. Gets called either after completion or if the interpolation is interrupted.
		///  </param>
		///  <param name="onComplete">
		///  A callback to be called when the interpolation is over. Gets called only after completion.
		///  </param>
		///  <param name="onStop">
		///  A callback to be called when the interpolation is stopped. Gets called only if the interpolation is interrupted.
		///  </param>
		public static void ResetTimeScale(float duration, Action onEnd = null, Action onComplete = null, Action onStop = null)
		{
			StartInterpolation(RealTime, duration, () => {
				onComplete?.Invoke();
				onEnd?.Invoke();
			}, ()=> {
				onStop?.Invoke();
				onEnd?.Invoke();
			});
		}
		
		static Coroutine interpolationCoroutine;
		static Action onInterpolationStopped;
		static void StartInterpolation(float finalTime, float duration, Action onComplete, Action onStop)
		{
			StopInterpolation();
			onInterpolationStopped = onStop;
			interpolationCoroutine = CoroutineRunner.StartCoroutine(InterpolateTimeScale(finalTime, duration, onComplete));
		}

		static void StopInterpolation()
		{
			if (interpolationCoroutine != null){
				CoroutineRunner.StopCoroutine(interpolationCoroutine);
				interpolationCoroutine = null;
				onInterpolationStopped?.Invoke();
				onInterpolationStopped = null;
			}
		}

		static IEnumerator InterpolateTimeScale(float finalTime, float duration, Action onComplete)
		{
			float originalTimeScale = Time.timeScale;
			for (float elapsed = 0; elapsed <= duration; elapsed += Time.unscaledDeltaTime){
				float newTime = Mathf.Lerp(originalTimeScale, finalTime, elapsed / duration);
				AssignTimeScale(newTime);
				yield return null;
			}

			AssignTimeScale(finalTime);

			interpolationCoroutine = null;
			onComplete?.Invoke();
		}

		static void AssignTimeScale(float newTimeScale)
		{
			Time.timeScale = newTimeScale;
			Time.fixedDeltaTime = newTimeScale * (RealPhysicsTime / RealTime);
		}
	}
}