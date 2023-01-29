namespace AccountsManager.ApplicationModels.V1.DTOs.PaginatedResponse
{
    public sealed class PaginatedResponse<T>
    {
        public List<T> Data { get; set; } = new List<T>();
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalEntities { get; set; }
    }
}
