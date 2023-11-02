namespace dotnet_rpg.Models
{
    public class Character
    {
        public int Id { get; set; }

        // setting the default value of Name to "Rex"
        public string Name { get; set; } = "Rex";

        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;

        // setting the default value of Archetype to "Hero"
        public RpgClass Archetype { get; set; } = RpgClass.Hero;
    }
}
