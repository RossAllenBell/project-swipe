using UnityEngine;
using System.Collections.Generic;

public abstract class Weapon
{
	public static List<Weapon> Weapons = new List<Weapon>(new Weapon[]{new Dagger(), new ShortSword()});

	public abstract string Name { get; }
	public abstract Texture2D Texture { get; }
	public abstract float Damage { get; }
	public abstract float LengthMitigation { get; }
	public abstract int Cost { get; }
}
