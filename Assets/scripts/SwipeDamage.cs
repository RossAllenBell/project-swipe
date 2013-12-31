public struct SwipeDamage
{
	public float StartTime, X, Y;
	public string Damage;

	public SwipeDamage(float startTime, float x, float y, float damage) 
	{
		this.StartTime = startTime;
		this.X = x;
		this.Y = y;
		this.Damage = ((int) damage).ToString();
	}
}