﻿using SocialMediaApp.Application.Common.Interfaces;
using SocialMediaApp.Domain.Entities;
using SocialMediaApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Infrastructure.Repositories
{
    public class PostRepository(AppDbContext db) : Repository<Post>(db),
        IPostRepository
    {
    }
}