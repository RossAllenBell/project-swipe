using UnityEngine;

public class ShortSword : Weapon
{

	public override string Name {get{return "Short Sword";}}
	public override float Damage {get{return 60f;}}
	public override float LengthMitigation {get{return 0.2f;}}
	public override int Cost {get{return 10;}}

}
