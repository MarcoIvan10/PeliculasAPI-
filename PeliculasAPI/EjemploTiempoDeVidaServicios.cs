namespace PeliculasAPI
{
    public class ServiciosTransient
    {
        private readonly Guid _id;
        public ServiciosTransient()
        {
            _id = Guid.NewGuid();
        }
        public Guid ObntenerId => _id;
    }

    public class ServiciosScoped
    {
        private readonly Guid _id;
        public ServiciosScoped()
        {
            _id = Guid.NewGuid();
        }
        public Guid ObntenerId => _id;
    }

    public class ServiciosSingleton
    {
        private readonly Guid _id;
        public ServiciosSingleton()
        {
            _id = Guid.NewGuid();
        }
        public Guid ObntenerId => _id;
    }
}
