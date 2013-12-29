public struct SwipeDamage
{
	public float startTime, x, y;
	public string damage;

	public SwipeDamage(float startTime, float x, float y, float damage) 
	{
		this.startTime = startTime;
		this.x = x;
		this.y = y;
		this.damage = ((int) damage).ToString();
	}
}