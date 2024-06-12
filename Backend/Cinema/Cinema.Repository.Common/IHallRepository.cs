﻿using Cinema.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Repository.Common
{
    public interface IHallRepository
    {
        Task AddHallAsync(Hall hall);
        Task<IEnumerable<Hall>> GetAllHallsAsync();
        Task<Hall?> GetHallByIdAsync(Guid id);
        Task UpdateHallAsync(Hall hall);
        Task DeleteHallAsync(Guid id);
    }
}
