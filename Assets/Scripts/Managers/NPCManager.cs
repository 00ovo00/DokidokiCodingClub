using System.Collections.Generic;

public class NPCManager : SingletonBase<NPCManager>
{
    public List<NPCData> NpcList = new List<NPCData>();

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
    
    private NPCData GetNPC(string name)
    {
        return NpcList.Find(npc => npc.NpcName == name);
    }

    public void UpdateFriendship(string name, int result)
    {
        NPCData npc = GetNPC(name);
        if (npc != null)
        {
            npc.FriendshipLv += result;
        }
    }
}
