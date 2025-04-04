using u_navigator_backend.Domain.Models;

namespace u_navigator_backend.Infrastructure.Repositories
{
    public interface IConsultantRepository
    {
        Task AddConsultantAsync(Consultant consultant);
        Task<Consultant?> GetConsultantByUserIdAsync(string userId);

    }
}
