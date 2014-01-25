using UnityEngine;

public class Dagger : Weapon
{

	public override string Name {get{return "Dagger";}}
	public override Texture2D Texture {get{return Resources.Load("media/ui/dagger") as Texture2D;}}
	public override float Damage {get{return 100f;}}
	public override float LengthMitigation {get{return 0f;}}
	public override int Cost {get{return 0;}}

}
