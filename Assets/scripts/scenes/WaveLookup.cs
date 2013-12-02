public class WaveLookup {
    
    public static Scene GetWave(int waveNumber)
    {
        switch (waveNumber)
        {
        case 1: return new EnemyWave1();
        case 2: return new EnemyWave2();
        default: return null;
        }
    }
    
}
