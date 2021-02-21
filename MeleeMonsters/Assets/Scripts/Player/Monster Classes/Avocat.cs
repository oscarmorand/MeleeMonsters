
public class Avocat : MonsterClass
{
    static public int speed_static = 15;

    public int Speed_public_instance = 15;

    private AvocadoSettings avocadoSettings;

    //constructor
    public Avocat(MonsterScriptableObject _monsterSetting) : base(_monsterSetting) => avocadoSettings = (AvocadoSettings)_monsterSetting;

    public void ComputeShoot()
    {
        float shoot = avocadoSettings.StoneShootStrength;
    }
}