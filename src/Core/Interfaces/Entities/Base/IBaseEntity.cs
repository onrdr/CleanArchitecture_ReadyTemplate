namespace Core.Interfaces.Entities.Base;

public interface IBaseEntity<T>
{
    public T Id { get; set; }
}

public interface IBaseEntity : IBaseEntity<Guid>
{

}
