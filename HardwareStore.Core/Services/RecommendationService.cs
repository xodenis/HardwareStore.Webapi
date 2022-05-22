using AutoMapper;
using HardwareStore.Core.Dto;
using HardwareStore.Core.Interfaces;
using HardwareStore.Db;
using HardwareStore.Db.Models;
using IronPython.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Scripting.Hosting;
using MyMediaLite.Data;
using MyMediaLite.IO;
using MyMediaLite.ItemRecommendation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HardwareStore.Core.Services
{
    public class RecommendationService : IRecommendationService
    {
        private UserKNN Recommender = new();
        private readonly string QueryString = "SELECT UserId, ProductId FROM dbo.UserActions;";
        private readonly string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=HardwareStoreDB;Integrated Security=True";

        private readonly IMapping user_mapping = new Mapping();
        private readonly IMapping item_mapping = new Mapping();

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public RecommendationService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Train()
        {
            using SqlConnection connection = new(ConnectionString);
            SqlCommand command = new(QueryString, connection);
            connection.Open();

            Recommender.UpdateItems = true;

            SqlDataReader reader = command.ExecuteReader();

            var training_data = ItemData.Read(reader, user_mapping, item_mapping);
            Recommender.Feedback = training_data;
            Recommender.Train();
            Recommender.SaveModel("RecommendationsModel");

            connection.Close();
        }

        public void AddFeedback(ICollection<Tuple<int, int>> feedback)
        {
            Recommender.LoadModel("RecommendationsModel");

            Recommender.AddFeedback(feedback);

            Recommender.SaveModel("RecommendationsModel");
        }

        public async Task<List<ProductShortDto>> GetUserRecommendations(int userId)
        {
            Recommender.LoadModel("RecommendationsModel");

            using SqlConnection connection = new(ConnectionString);
            SqlCommand command = new(QueryString, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            var data = ItemData.Read(reader, user_mapping, item_mapping);
            Recommender.Feedback = data;


            var recommendations = Recommender.Recommend(user_mapping.ToInternalID(userId.ToString()), 5);

            List<Product> recommendedProducts = new();

            foreach (var item in recommendations)
            {
                int productId = int.Parse(item_mapping.ToOriginalID(item.Item1));
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id.Equals(productId));

                recommendedProducts.Add(product);
            }

            return _mapper.Map<List<ProductShortDto>>(recommendedProducts);
        }

        public async Task<List<ProductShortDto>> GetProductRecommendations(int productId)
        {
            System.Diagnostics.ProcessStartInfo start = new System.Diagnostics.ProcessStartInfo
            {
                FileName = @"C:\Python310\python.exe",
                Arguments = string.Format("{0} {1}", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ItemBasedRecommender.py"), productId),
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            System.Diagnostics.Process process = System.Diagnostics.Process.Start(start);
            using StreamReader reader = process.StandardOutput;
            string stderr = process.StandardError.ReadToEnd();
            string scriptResult = reader.ReadToEnd();

            scriptResult = scriptResult.Replace("Name: Id, dtype: int64", "");

            var temp = scriptResult.Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            List<Product> productsList = new();

            for (int i = 1; i < temp.Length; i += 2)
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id.Equals(int.Parse(temp[i])));
                productsList.Add(product);
            }

            return _mapper.Map<List<ProductShortDto>>(productsList);
        }
    }
}
