using UnityEngine;

public class Dagger : Weapon
{

	public override string Name {get{return "Dagger";}}
	public override float Damage {get{return 50f;}}
	public override float LengthMitigation {get{return 0f;}}
	public override int Cost {get{return 0;}}

}
