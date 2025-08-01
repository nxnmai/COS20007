namespace DrawingBattleArena
{
    public class CombatStrategy
    {
        public void ApplyCombat(Shape attacker, Shape target, object attackerOwner, object targetOwner, Canvas canvas, ref int damage, ref bool blocked)
        {
            Console.WriteLine($"Combat: {attacker.GetType().Name} vs {target.GetType().Name}, initial damage: {damage}");

            if (attacker is MyCircle && target is MyRectangle rect)
            {
                rect.Health -= 20;
                if (rect.Health <= 0)
                {
                    canvas.RemoveShape(rect);
                }

                blocked = true;
                damage = 0;
            }
            else if (attacker is MyCircle && target is MyLine line)
            {
                line.Health -= 10;

                blocked = true;
                damage = 0;
                
                if (attackerOwner is Player)
                {
                    ((Player)attackerOwner).BaseHealth -= 10;
                    Console.WriteLine("Player health reduced by 10 due to reflection, new health: " + ((Player)attackerOwner).BaseHealth);
                }
                else if (attackerOwner is Bot)
                {
                    ((Bot)attackerOwner).BaseHealth -= 10;
                    Console.WriteLine("Bot health reduced by 10 due to reflection, new health: " + ((Bot)attackerOwner).BaseHealth);
                }
                if (line.Health <= 0)
                {
                    canvas.RemoveShape(line);
                }
            }
            else if (attacker is MyCircle && target is MyCircle)
            {
                if (attackerOwner is Player)
                {
                    ((Bot)targetOwner).BaseHealth -= damage;
                    Console.WriteLine("Bot health reduced to " + ((Bot)targetOwner).BaseHealth);
                }
                else if (attackerOwner is Bot)
                {
                    ((Player)targetOwner).BaseHealth -= damage;
                    Console.WriteLine("Player health reduced to " + ((Player)targetOwner).BaseHealth);
                }
                blocked = true;
            }
        }
    }
}
