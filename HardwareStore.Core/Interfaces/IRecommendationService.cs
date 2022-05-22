using HardwareStore.Core.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HardwareStore.Core.Interfaces
{
    public interface IRecommendationService
    {
        void Train();
        void AddFeedback(ICollection<Tuple<int, int>> feedback);
        Task<List<ProductShortDto>> GetUserRecommendations(int userId);
        Task<List<ProductShortDto>> GetProductRecommendations(int productId);
    }
}
