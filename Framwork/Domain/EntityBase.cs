namespace Framwork.Domain
{
    public class EntityBase<TKEY>
    {
        public EntityBase()
        {
            CreationDate = DateTime.Now;
        }

        public long Id { get; private set; }
        public DateTime CreationDate { get; private set; }

    }
}
