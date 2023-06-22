namespace Inheritance
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Creature creature1 = new Creature();
            creature1.Breath();

            NPC npc1 = new NPC();
            npc1.Breath();
            npc1.Interaction();

            // 공변성 : 하위타입을 기반타입으로 참조할 수 있는 성질
            Creature creature2 = new NPC();
            //NPC npc2 = new Creature(); 
            // 인스턴스 멤버함수 호출시 caller(this) 에 인스턴스 참조를 넘겨주는데, 기반 타입 객체를 넘겨주면 할당되지 않은 멤버에 접근하게되므로 불가능함
            creature2.Breath();
            // creature2.Interaction(); // 객체가 어떤 멤버들을 가지고있던지, 참조변수타입에 따라서만 멤버접근이 가능함.

            //npc2.Interaction(); 
        }
    }
}