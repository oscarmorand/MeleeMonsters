
public class Fantome : MonsterClass
{
    static public int speed_static = 15;

    public int Speed_public_instance = 15;

    private FantomeSettings fantomeSettings;

    //constructor
    public Fantome(MonsterScriptableObject _monsterSetting) : base(_monsterSetting) => fantomeSettings = (FantomeSettings)_monsterSetting;
}
