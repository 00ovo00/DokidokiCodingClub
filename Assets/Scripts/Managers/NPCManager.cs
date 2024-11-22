using System.Collections.Generic;

public class NPCManager : SingletonBase<NPCManager>
{
    public List<NPCData> NpcList = new List<NPCData>();

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
