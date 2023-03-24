using System.Windows.Shapes;

namespace Antsimulation
{
    public class Ant
    {
        public Ellipse ant { get; set; }
        public int health { get; set; }
        public double nutrition { get; set; }
        public int strength { get; set; }
        public int speed { get; set; }
        public int gen { get; set; }
        public double duration { get; set; }

        public Ant(Ellipse ant, int health, double nutrition, int strength, int speed, int gen, double duration)
        {
            this.ant = ant;
            this.health = health;
            this.nutrition = nutrition;
            this.strength = strength;
            this.speed = speed;
            this.gen = gen;
            this.duration = duration;
        }
    }

    public class Food
    {
        public Ellipse food { get; set; }
        public double size { get; set; }
        public Food(Ellipse food, int size)
        {
            this.food = food;
            this.size = size;
        }
    }
}