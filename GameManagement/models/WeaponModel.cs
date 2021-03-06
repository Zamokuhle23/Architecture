using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponModel : BaseModel, IWeaponModel
{

	[Header("Shooting")]
	public float shotCooldown;
	public bool isSingleShot;

	public bool isTriggerDown { get; set; }
	public float NextShotAvailable { get; set; }

	[Header("Physics")]
	public float recoil = 0f;

}
