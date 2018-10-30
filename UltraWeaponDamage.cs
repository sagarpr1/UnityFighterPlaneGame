using UnityEngine;
using System.Collections;
using UltraReal.Utilities;
using UltraReal.WeaponSystem;

public class UltraWeaponDamage : UltraRealDamageBase
{



		/// <summary> 
		/// Reference to a damage sound effect
		/// </summary>
		[SerializeField]
		protected AudioClip _damagedSound = null;

		/// <summary> 
		/// Reference to a death sound effect
		/// </summary>
		[SerializeField]
		protected AudioClip _deathSound = null;

		/// <summary> 
		/// Reference to Audio Source. If none is provide, it will try to get the one on the game object.
		/// </summary>
		[SerializeField]
		protected AudioSource _audioSource = null;

		/// <summary> 
		/// Overriden OnStart method from UltraRealDamageBase.
		/// </summary>
		protected override void OnStart()
		{
			base.OnStart();

			_audioSource = GetComponent<AudioSource>();
		}

		/// <summary> 
		/// Overriden OnDeath method from UltraRealDamageBase.
		/// </summary>
		protected override void OnDeath()
		{
			base.OnDeath();

			if (_audioSource != null && _damagedSound != null)
				_audioSource.PlayOneShot(_deathSound);

			Destroy(gameObject);
		}

		/// <summary> 
		/// Overriden OnDamaged method from UltraRealDamageBase.
		/// </summary>
		protected override void OnDamaged(float damage)
		{
		//dont care about UltraReal health & damage: so make damage as 0
		//only updating player health
			base.OnDamaged(0);
		transform.GetComponent<PlayerHealth>().Hurt(damage);

			if (_audioSource != null && _damagedSound != null)
				_audioSource.PlayOneShot(_damagedSound);
		}
}

