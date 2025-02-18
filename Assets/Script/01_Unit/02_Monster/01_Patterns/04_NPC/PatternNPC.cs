public class PatternNPC : Pattern
{
    protected NPC npc;

    public virtual void Initialize(NPC npc)
    {
        this.npc = npc;

        Recognize?.Initialize(npc);
        MoveBasic?.Initialize(npc);
        MoveChase?.Initialize(npc);
    }
}
