﻿using NZWalks.API.Model.Domain;

namespace NZWalks.API.Repositories
{
    public interface ITokenHandler
    {
        Task<string> CreateToken(User user);
    }
}
