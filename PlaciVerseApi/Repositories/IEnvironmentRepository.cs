﻿using PlaciVerseApi.Models;

namespace PlaciVerseApi.Repositories
{
    public interface IEnvironmentRepository
    {
        Task<bool> CreateEnvironment(Environment2D env, string userId);
        Task<IEnumerable<Environment2D?>> GetEnvironmentByUserId(string userId);
        //Task<bool> UpdateEnvironment(Environment2D env);
        Task<bool> DeleteEnvironment(int id, string userId);
    }
}
