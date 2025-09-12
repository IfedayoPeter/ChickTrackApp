namespace Base.Services
{
    public class MapperConfig : Profile
    {
        private readonly List<Type> mappings = new List<Type>();
        //private List<Type> mappings;
        public MapperConfig()
        {
            ConfigureCustomMappings();
        }

        public virtual void AddMappingType(Type type)
        {
            mappings.Add(type);
        }

        protected virtual void ConfigureCustomMappings()
        {
            // Custom mappings with member configurations           
        }

    }
}
