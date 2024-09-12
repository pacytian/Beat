// BeatTable Class
public class BeatDatabase : TableDatabase
{
    public string ingredient;
    public string place;
    public int order;
    public int earliest;
}
//角色表
public class BeatTable : ConfigTable<BeatDatabase, BeatTable>
{
    void Awake()
    {
        load("Config/BeatTable1.csv");
    }
}