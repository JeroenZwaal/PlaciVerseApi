﻿using PlaciVerseApi.Models;
using System.Threading.Tasks;

namespace PlaciVerseApi.Repositories
{
    public interface IObjectRepository
    {
        Task<bool> CreateObject(Object2D obj, int envId);
        Task<List<Object2D?>> GetObjectsByEnvironmentId(int envId);
        //Task<List<Object2D>> GetAllObjects();
        //Task<bool> UpdateObject(Object2D obj);
        //Task<bool> DeleteObject(int id);
    }
}
