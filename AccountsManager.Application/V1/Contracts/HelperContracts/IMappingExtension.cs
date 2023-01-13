namespace AccountsManager.Application.V1.Contracts.HelperContracts
{
    public interface IMappingExtension
    {
        TDestination MapEntity<TSource, TDestination>(TSource entity);
        TDestination MapEntiyInto<TSource, TDestination>(TSource source, TDestination destination);
    }
}
