namespace Overture.Domain.Data.Aggregate
{
    public record Address
    {
        public required Id Id { get; init; }
        public TenantId TenantId { get; set; } = TenantId.Default;
    }

    
}
