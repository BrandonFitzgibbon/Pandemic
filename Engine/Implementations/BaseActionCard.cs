namespace Engine.Implementations
{
    public class BaseActionCard : Card
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public BaseActionCard(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
