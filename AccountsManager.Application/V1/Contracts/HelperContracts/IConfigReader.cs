namespace AccountsManager.Application.V1.Contracts.HelperContracts
{
    public interface IConfigReader
    {
        T GetSectionValue<T>(string section);
    }
}
