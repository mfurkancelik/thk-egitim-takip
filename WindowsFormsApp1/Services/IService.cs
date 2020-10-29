﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Services
{
    public interface IService<T> where T : BaseEntity
    {
        void Add(T item);
        void Update(T item);
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> exp);
        T GetById(Guid id);
        T GetByDefault(Expression<Func<T, bool>> exp);
        void Delete(Guid id);
        int Save();

    }
}
